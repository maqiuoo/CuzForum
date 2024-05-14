
using Microsoft.EntityFrameworkCore;
using MyForum.BLL;
using MyForum.BLL.Interface;
using MyForum.DAL;
using MyForum.DAL.Interface;
using MyForum.Model;

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
			//builder.Services.AddScoped<IBlogNewsService, BlogNewsService>();
			//builder.Services.AddScoped<ITypeInfoRepository, TypeInfoRepository>();
			//builder.Services.AddScoped<ITypeInfoService, TypeInfoService>();
			//builder.Services.AddScoped<IWriterInfoRepository, WriterInfoRepository>();
			//builder.Services.AddScoped<IWriterInfoService, WriterInfoService>();
			builder.Services.AddDbContext<ModelDbContext>(options => {
				options.UseSqlServer("Server=.;DataBase=MyForum;Trusted_Connection=True;TrustServerCertificate=true;");
			});
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
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseCors(myPloicy);
			app.MapControllers();
			app.UseAuthorization();


			app.Run();
		}
	}
}
