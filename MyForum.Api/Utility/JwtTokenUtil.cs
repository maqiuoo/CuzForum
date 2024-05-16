using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyForum.Api.Utility
{
	public class JwtTokenUtil
	{
		private string secKey;
		private int expireSeconds;

		public JwtTokenUtil(string secKey, int expireSeconds)
		{
			this.secKey = secKey;
			this.expireSeconds = expireSeconds;
		}
		public bool IsTokenExpired(string token, string secretKey)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(secretKey);
			try
			{
				tokenHandler.ValidateToken(token, new TokenValidationParameters {
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
			} catch (SecurityTokenExpiredException)
			{
				// token过期
				return true;
			} catch (Exception)
			{
				// 其他错误表示token无效
				return true;
			}
		}
		public string GetUsernameFromToken(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

			if (jwtToken == null)
			{
				return null;
			}
			// 这里假设用户名存储在 "username" 声明中。
			// 你可能需要根据你的JWT结构来调整这个声明的名称。
			var usernameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserName");

			if (usernameClaim != null)
			{
				return usernameClaim.Value;
			}

			return null;
		}

		private DateTime? GetCreatedDateFromToken(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			// 注意：这里不验证令牌的真实性和完整性。只解析结构读取数据。
			var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

			if (jwtToken == null)
			{
				return null;
			}

			var issuedAtClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "iat");
			if (issuedAtClaim != null)
			{
				if (long.TryParse(issuedAtClaim.Value, out var seconds))
				{
					var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
					return epoch.AddSeconds(seconds);
				}
			}
			return null;
		}

		private DateTime? GetExpirationDateFromToken(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken; // 不验证签名
			if (jwtToken == null)
			{
				return null;
			}
			var expirationClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "exp");
			if (expirationClaim != null)
			{
				// 尝试将过期声明的值转换为秒
				if (Int64.TryParse(expirationClaim.Value, out var seconds))
				{
					var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
					return epoch.AddSeconds(seconds);
				}
			}
			return null; // 没有找到过期声明或转换失败
		}

		private Claim GetClaimsFromToken(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(secKey); // 这里需要使用和生成令牌时相同的密钥
			try
			{
				tokenHandler.ValidateToken(token, new TokenValidationParameters {
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					// 根据需要设置其他参数
				}, out SecurityToken validatedToken);

				var jwtToken = (JwtSecurityToken)validatedToken;

				// 此处仅返回令牌中的第一个声明。如果有多个声明，你可能需要修改它以满足你的需求。
				return jwtToken.Claims.FirstOrDefault();
			} catch
			{
				// 令牌验证失败或其他错误的处理
				return null;
			}
		}

		private DateTime GenerateExpirationDate()
		{
			return DateTime.UtcNow.AddSeconds(expireSeconds);
		}

		public bool IsTokenExpired(string token)
		{
			var expiration = GetExpirationDateFromToken(token);
			return expiration != null && expiration.Value < DateTime.UtcNow;
		}

		public bool IsCreatedBeforeLastPasswordReset(DateTime? created, DateTime? lastPasswordReset)
		{
			return lastPasswordReset != null && created < lastPasswordReset;
		}

		public string GenerateToken(string username)
		{
			var claims = new Dictionary<string, object>
			{
			{"Claim_Key_Username", username},
			{"Claim_Key_Created", DateTime.UtcNow}
		};
			return GenerateToken(claims);
		}

		private string GenerateToken(IDictionary<string, object> claims)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(secKey);
			var tokenDescriptor = new SecurityTokenDescriptor {
				Subject = new ClaimsIdentity(new Claim[]
				{
				new Claim(JwtRegisteredClaimNames.Sub, (string)claims["Claim_Key_Username"]),
				new Claim("Claim_Key_Created", ((DateTime)claims["Claim_Key_Created"]).Ticks.ToString())
				}),
				Expires = GenerateExpirationDate(),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

		public bool CanTokenBeRefreshed(string token, DateTime? lastPasswordReset)
		{
			var created = GetCreatedDateFromToken(token);
			return !IsCreatedBeforeLastPasswordReset(created, lastPasswordReset) && !IsTokenExpired(token);
		}

		public string RefreshToken(string token)
		{
			try
			{
				var claimsPrincipal = GetClaimsFromToken(token);
				var claims = new Dictionary<string, object>
				{
				{"Claim_Key_Created", DateTime.UtcNow}
			};
				return GenerateToken(claims);
			} catch
			{
				return null;
			}
		}

		public bool ValidateToken(string token)
		{
			var username = GetUsernameFromToken(token);
			var created = GetCreatedDateFromToken(token);
			var expiration = GetExpirationDateFromToken(token);
			return username != null && !IsTokenExpired(token) && IsCreatedBeforeLastPasswordReset(created, expiration.Value);
		}
	}
}
