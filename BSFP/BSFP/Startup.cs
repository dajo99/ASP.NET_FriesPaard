using BSFP.Areas.Identity.Data;
using BSFP.Data;
using BSFP.Data.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSFP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BSFPContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("BSFPConnection")));
            services.AddDefaultIdentity<CustomUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BSFPContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            //CreateUserRoles(serviceProvider).Wait();
        }



        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            RoleManager<IdentityRole> RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            BSFPContext Context = serviceProvider.GetRequiredService<BSFPContext>();

            IdentityResult roleResult;

            UserManager<CustomUser> _userManager = serviceProvider.GetRequiredService<UserManager<CustomUser>>();
            // Adding Admin Role.
            bool roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                // create the roles and seed them to the database.
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));

            }

            //Main user aanmeken
            CustomUser exist = await _userManager.FindByEmailAsync("bsfp@gmail.com");
            if (exist == null)
            {
                var admin = new CustomUser { UserName = "Admin", Email = "bsfp@gmail.com", Voornaam = "Admin", Achternaam = "BSFP" };
                var result = await _userManager.CreateAsync(admin, "Campus99");
                if (result.Succeeded)
                {
                    // Assign Admin role to the main user.
                    IdentityUser user = Context.Users.FirstOrDefault(u => u.Email == "bsfp@gmail.com");
                    if (user != null)
                    {
                        DbSet<IdentityUserRole<string>> roles = Context.UserRoles;
                        IdentityRole adminRole = Context.Roles.FirstOrDefault(r => r.Name == "Admin");
                        if (adminRole != null)
                        {
                            if (!roles.Any(ur => ur.UserId == user.Id && ur.RoleId == adminRole.Id))
                            {
                                roles.Add(new IdentityUserRole<string>() { UserId = user.Id, RoleId = adminRole.Id });
                                Context.SaveChanges();
                            }
                        }
                    }

                }
            }





            // Adding Member Role.
            if (!await RoleManager.RoleExistsAsync("Member"))
            {
                // create the roles and seed them to the database.
                await RoleManager.CreateAsync(new IdentityRole("Member"));

            }



        }
    }
}
