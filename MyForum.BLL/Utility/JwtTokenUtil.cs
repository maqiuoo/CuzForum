using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MyForum.BLL.Utility
{
    public static class JwtTokenUtil
    {
        public static bool IsTokenExpired(string token, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero // 调整时钟偏差容忍值
                }, out SecurityToken validatedToken);

                // 如果ValidateToken没有抛出异常，那么token有效
                var jwtToken = (JwtSecurityToken)validatedToken;
                // 可以进一步检查jwtToken对象的属性来获取更多信息
                return false;
            }
            catch (SecurityTokenExpiredException)
            {
                // token过期
                return true;
            }
            catch (Exception)
            {
                // 其他错误表示token无效
                return true;
            }
        }
    }
}
