using application.interfaces;
using domain.interfaces;
using infrastructure.identity;
using application.cases.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text;
using domain.entities;
using infrastructure.dependency;
using Microsoft.EntityFrameworkCore;
using application.exceptions;

namespace infrastructure.repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _ctx;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender emailSender;
        public UserRepository(UserManager<AppUser> userManager, IEmailSender emailSender, ApplicationDbContext ctx)
        {
            _ctx = ctx;
            _userManager = userManager;
            this.emailSender = emailSender;
        }
        public async Task<User?> GetByIdAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "Customer";

            return new User(
                user.Id,
                user.UserName!,
                user.FullName!,
                user.Email!,
                user.CreatedAt,
                user.UpdatedAt
            // role
            );
        }

        public async Task<Guid> CreatedAsync(User user)
        {

            if (await EmailExists(user.Email))
                throw new Exception("Email đã tồn tại kh thể đăng ký");
            string randomName = Guid.NewGuid().ToString("N").Substring(0, 10);
            // var userCreate = User.Create(userRequest.UserName, userRequest.Email, userRequest.Password);

            var userApp = new AppUser
            {
                Id = Guid.NewGuid(),
                UserName = user.Name,
                FullName = randomName,
                Email = user.Email,
                CreatedAt = DateTime.UtcNow
            };
            var result = await _userManager.CreateAsync(userApp, user.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join(",", result.Errors.Select(e => e.Description)));
            // await _userManager.AddToRoleAsync(userApp, "Guest");
            var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(userApp);

            var tokenBytes = Encoding.UTF8.GetBytes(emailToken);
            var base64Url = Convert.ToBase64String(tokenBytes)
                .Replace("+", "-")
                .Replace("/", "_")
                .TrimEnd('=');
            Console.WriteLine(base64Url);
            var confirmLink = $"http://localhost:5255/api/auth/email-confirm?userId={userApp.Id}&token={base64Url}";
            await emailSender.SendEmailAsync(user.Email,
                    "Xác nhận địa chỉ email của bạn",
                    $@"
                <h2>Chào mừng bạn đến với TestX!</h2>
                <p>Cảm ơn bạn đã đăng ký tài khoản. Vui lòng nhấn vào nút bên dưới để xác nhận địa chỉ email của bạn:</p>
                <p>
                    <a href='{confirmLink}' 
                    style='background-color: #4CAF50; color: white; padding: 14px 20px; text-decoration: none; border-radius: 4px; display: inline-block;'>
                        Xác nhận Email
                    </a>
                </p>
                <p>Hoặc copy link sau vào trình duyệt:</p>
                <p>{confirmLink}</p>
                <p>Link này sẽ hết hạn sau 24 giờ.</p>
                <p>Nếu bạn không đăng ký tài khoản này, vui lòng bỏ qua email này.</p>
            "
                , true);

            return userApp.Id;
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _ctx.Users.AnyAsync(e => e.Email == email);
        }
        public async Task<IReadOnlyList<User>> GetAllUserAsync()
        {
            var users = await _ctx.Users.AsNoTracking().ToListAsync();
            return users.Select(e => new User(e.Id, e.UserName!, e.FullName!, e.Email!, e.CreatedAt, e.UpdatedAt)).ToList();
        }


        public async Task RemoveUser(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if(user is not null)
            {
                var result = await _userManager.DeleteAsync(user);

                if(!result.Succeeded) 
                    throw new Exception($"Failed to remove user {userId}");
            }
        }
    }
}