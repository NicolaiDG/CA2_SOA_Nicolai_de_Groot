using CA2_Nicolai_de_Groot.Data;
using CA2_Nicolai_de_Groot.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CA2_Nicolai_de_Groot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Frontend
            builder.Services.AddRazorPages();
            builder.Services.AddHttpClient();



            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();


            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/api/Auth/Login";
            });


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();   
            app.UseAuthorization();

            app.MapControllers();

            // Frontend
            app.MapRazorPages();
            app.UseStaticFiles();


            app.Run();
        }
    }
}
