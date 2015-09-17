using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Data.Entity;
using MovieAngularJSApp.Models;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.Data.Entity.SqlServer;
using System;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.Runtime;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity.Relational;

namespace MovieAngularJSApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            // Setup configuration sources.

            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }


        public void ConfigureServices(IServiceCollection services)
        {
            // add Entity Framework
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<MoviesAppContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            // add ASP.NET Identity
            services.AddIdentity<ApplicationUser, IdentityRole>() //Configuration)
                .AddEntityFrameworkStores<MoviesAppContext>()
                .AddDefaultTokenProviders();


            // add ASP.NET MVC
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseIdentity();


            CreateSampleData(app.ApplicationServices).Wait();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                // Uncomment the following line to add a route for porting Web API 2 controllers.
                // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            });
        }


        private static async Task CreateSampleData(IServiceProvider applicationServices)
        {
            using (var dbContext = applicationServices.GetService<MoviesAppContext>())
            {
                var sqlServerDatabase = dbContext.Database as RelationalDatabase;
                if (sqlServerDatabase != null)
                {
                    // Create database in user root (c:\users\your name)
                    if (sqlServerDatabase != null)
                    {
                        sqlServerDatabase.EnsureCreatedAsync().Wait();
                    }
                    // add some movies
                    var movies = new List<Movie>
                    {
                        new Movie {Title="Star Wars", Director="Lucas"},
                        new Movie {Title="King Kong", Director="Jackson"},
                        new Movie {Title="Memento", Director="Nolan"}
                    };
                    movies.ForEach(m => dbContext.Movies.Add(m));

                    // add some users
                    var userManager = applicationServices.GetService<UserManager<ApplicationUser>>();

                    // add editor user
                    var stephen = new ApplicationUser
                    {
                        UserName = "Stephen"
                    };
                    var result = await userManager.CreateAsync(stephen, "P@ssw0rd");
                    await userManager.AddClaimAsync(stephen, new Claim(ClaimTypes.Role, "CanEdit"));

                    // add normal user
                    var bob = new ApplicationUser
                    {
                        UserName = "Bob"
                    };
                    await userManager.CreateAsync(bob, "P@ssw0rd");


                }
            }
        }



    }
}


