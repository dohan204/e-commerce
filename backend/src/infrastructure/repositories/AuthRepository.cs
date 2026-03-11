using System.Linq.Expressions;
using System.Text;
using application.cases.Dtos;
using application.cases.Queries.Authentication;
using application.exceptions;
using application.interfaces;
using domain.entities;
using infrastructure.identity;
using infrastructure.services;
using Microsoft.AspNetCore.Identity;

namespace infrastructure.repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly JwtToken _token;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AuthRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, JwtToken token)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _token = token;
        }

        public async Task<ResponseLogin> LoginRequest(GetUserLoginCommand command)
        {
            var user = await _userManager.FindByNameAsync(command.username);
            if(user is null) 
                throw new NotFoundException("User not found");
            if(!user.EmailConfirmed)
                throw new UnauthorizeException("Email chưa được xác nhận");

            var result = await _userManager.CheckPasswordAsync(user, command.password);
            if(!result) 
                throw new UnauthorizeException("Tài khoản hoặc mật khẩu không đúng");
            var token = _token.GenerateToken(user);
            return new ResponseLogin(token);
        }
        public async Task<bool> ConfirmEmail(string userId, string token)
        {
            // laays ra user 
            var user = await _userManager.FindByIdAsync(userId);
            if(user is null) 
                return false;

            if(string.IsNullOrEmpty(token))
                return false;

            var base64 = token.Replace("-", "+").Replace("_", "/");
            var tokenBytes = Convert.FromBase64String(base64);
            var originalString = Encoding.UTF8.GetString(tokenBytes);

            var result = await _userManager.ConfirmEmailAsync(user, originalString);
            if(!result.Succeeded)
                return false; 
            return true;
        }
    }
}