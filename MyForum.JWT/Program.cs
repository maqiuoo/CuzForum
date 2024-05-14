
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyForum.BLL;
using MyForum.BLL.Interface;
using MyForum.DAL;
using MyForum.DAL.Interface;
using MyForum.Model;
using System.Data;
using System.Text;

namespace MyForum.JWT
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddAuthorization();
			string myPloicy = "MyPolicy";
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<ModelDbContext>(options => {
				options.UseSqlServer("Server=.;DataBase=MyForum;Trusted_Connection=True;TrustServerCertificate=true;");
			});
			builder.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<IUserService, UserService>();
			builder.Services.AddControllers();
			#region JWT
			builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));
			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(opt => {
					var jwtSettings = builder.Configuration.GetSection("JWT").Get<JWTSettings>();
					byte[] keyBytes = Encoding.UTF8.GetBytes(jwtSettings.SecKey);
					var secKey = new SymmetricSecurityKey(keyBytes);
					opt.TokenValidationParameters = new() {
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = secKey
					};
				});
			#endregion
			builder.Services.AddCors(options => {
				options.AddPolicy(name: myPloicy, policy => {
					policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyHeader();
				});
			});
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.MapControllers();
			app.UseAuthentication();
			app.UseAuthorization();

			app.Run();
		}
	}
}
