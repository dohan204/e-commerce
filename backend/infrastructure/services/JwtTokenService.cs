using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using infrastructure.identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace infrastructure.services
{
    public class JwtToken
    {
        private readonly IConfiguration _config;
        public JwtToken(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateToken(AppUser user)
        {
            // tạo danh sách claim
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("FullName", user.FullName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            

            // lấy key từ cấu hình
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? string.Empty));

            // đăng ký chứng chỉ xác thực, với thuật toán mã hóa 
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // tạo cấu trúc token 
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );


            // viết token ra chuỗi string 
            return new JwtSecurityTokenHandler().WriteToken(token);
            
        }
    }
}