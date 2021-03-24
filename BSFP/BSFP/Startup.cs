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
using System.Globalization;
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

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc()
                .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

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

                // ConfirmedEmail
                options.SignIn.RequireConfirmedEmail = true;
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

            var cultures = new List<CultureInfo> {
                new CultureInfo("nl-NL"),
                new CultureInfo("fr-FR"),
                new CultureInfo("de-DE"),
                new CultureInfo("en-US")
            };
            app.UseRequestLocalization(options => {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-US");
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });

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

            //Main user aanmaken
            CustomUser exist = await _userManager.FindByEmailAsync("bsfptest@gmail.com");
            if (exist == null)
            {
                var admin = new CustomUser { UserName = "Admintest", Email = "bsfptest@gmail.com", Voornaam = "Admintest", Achternaam = "BSFP",Lidnummer="BSFP123456789", PhoneNumber="0473576120", EmailConfirmed = true};
                var result = await _userManager.CreateAsync(admin, "Campus99");
                if (result.Succeeded)
                {
                    // Assign Admin role to the main user.
                    IdentityUser user = Context.Users.FirstOrDefault(u => u.Email == "bsfptest@gmail.com");
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
