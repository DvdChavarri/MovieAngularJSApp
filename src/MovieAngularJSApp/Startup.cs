using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using MovieAngularJSApp.Models;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.Runtime;
using Microsoft.Data.Entity;

namespace MovieAngularJSApp
{
    public class Startup
	{
		/*
        public Startup(IHostingEnvironment env)
		{
			// Setup configuration sources.
			Configuration = new Configuration()
				.AddJsonFile("config.json")
				.AddEnvironmentVariables();
		}
        */

        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            // Setup configuration sources.
            var configurationBuilder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);

            configurationBuilder.AddEnvironmentVariables();
            Configuration = configurationBuilder.Build();
        }


        public IConfiguration Configuration { get; set; }


		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();

			// Register Entity Framework
			services.AddEntityFramework() // (Configuration)
				.AddSqlServer()
                .AddDbContext<MoviesAppContext>(options =>
                {
                    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MoviesDatabase;Trusted_Connection=True;MultipleActiveResultSets=true");
                    //Configuration.Get("Data:DefaultConnection:ConnectionString"));
                });
        }

		public void Configure(IApplicationBuilder app)
		{
			app.UseMvc();
        }

	}
}
