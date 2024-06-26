
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyForum.Api.Py;
using MyForum.BLL;
using MyForum.BLL.Interface;
using MyForum.DAL.Interface;
using MyForum.DAL.Repository;
using MyForum.DAL.ReptileData;
using MyForum.DAL.ReptileDataInterface;
using MyForum.DAL.ReptileDataRepository;
using MyForum.JWT;
using MyForum.Model;
using MyForum.Model.DTO;
using MyForum.Model.ReptlieModel;
using System.Text;

namespace MyForum.Api
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddAuthorization();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddControllers();
			builder.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<IUserService, UserService>();
			builder.Services.AddScoped<ITopicRepository, TopicRepository>();
			builder.Services.AddScoped<ITopicService, TopicService>();
			builder.Services.AddScoped<ICommentRepository, CommentRepository>();
			builder.Services.AddScoped<ICommentService, CommentService>();
			builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
			builder.Services.AddScoped<INotificationService, NotificationService>();
			builder.Services.AddScoped<ICollectRepository, CollectRepository>();
			builder.Services.AddScoped<ICollectService, CollectService>();
			builder.Services.AddScoped<IAccessTokenRepository, AccessTokenRepository>();
			builder.Services.AddScoped<IAccessTokenService, AccessTokenService>();
			builder.Services.AddScoped<ITableReadRepository, TableReadRepository>();
			builder.Services.AddHostedService<TimedHostedService>();
			builder.Services.AddScoped<IBooksRepository, BooksRepository>();
			builder.Services.AddScoped<ILectureRepository, LectureRepository>();
			builder.Services.AddScoped<ISchoolNotificationRepository, SchoolNotificationRepository>();
			builder.Services.AddAutoMapper(typeof(OrganizationProfile));

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
			builder.Services.AddDbContext<ModelDbContext>(options => {
				options.UseLazyLoadingProxies().UseSqlServer("Server=.;DataBase=MyForum;Trusted_Connection=True;TrustServerCertificate=true;");
			});
			builder.Services.AddDbContext<ReptileDbContext>(options => {
				options.UseSqlServer("Server=.;DataBase=MyForum;Trusted_Connection=True;TrustServerCertificate=true;");
			}, ServiceLifetime.Transient);
			string myPloicy = "MyPolicy";
			builder.Services.AddCors(options => {
				options.AddPolicy(name: myPloicy, policy => {
					policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
				});
			});
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseStaticFiles();
			app.UseRouting();
			app.UseCors(myPloicy);
			app.MapControllers();
			app.UseAuthentication();
			app.UseAuthorization();

			app.Run();
		}
	}
}
