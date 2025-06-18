using Microsoft.EntityFrameworkCore;
using DiplomaProject.API.Extentions;
using System.Globalization;
using System.Text.Json.Serialization;

namespace DiplomaProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var cultureInfo = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            builder.Services.AddControllersWithViews();
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionsString")));
            builder.Services.RegisterBLLDependencies();
            builder.Services.RegisterDALDependencies();
            builder.Services.AddRazorPages();
            builder.Services.AddHttpClient();

            builder.Services.AddAuthentication("MyCookieAuth")
                .AddCookie("MyCookieAuth", options =>
                {
                    options.LoginPath = "/Auth"; 
                    options.ExpireTimeSpan = TimeSpan.FromHours(1);
                    options.SlidingExpiration = true;
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("StudentOnly", policy => policy.RequireRole("Студент"));
                options.AddPolicy("TeacherOnly", policy => policy.RequireRole("Преподаватель"));
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}"); //Index

            app.UseAuthentication(); 
            app.UseAuthorization();
            app.MapRazorPages();
            app.MapControllers();
            app.UseStaticFiles();
            app.MapFallbackToFile("Index.html");
            app.UseSwagger();
            app.UseSwaggerUI();
            app.Run();
        }
    }
}
