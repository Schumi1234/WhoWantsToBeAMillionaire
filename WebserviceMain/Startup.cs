using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebserviceMain.Database;
using WebserviceMain.Database.Helper;
using WebserviceMain.WhoWantsToBeAMillionaire;
using WebserviceMain.Joker;
using WebserviceMain.Login;

namespace WebserviceMain
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
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.AddTransient<DatabaseHandler>();
			services.AddTransient<DatabaseController>();
			services.AddTransient<WhoWantsToBeAMillionaireHandler>();
			services.AddTransient<JokerHandler>();
			// for localhost of db
			var host = System.Environment.MachineName;
			var connection = $"Data Source={host};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;initial catalog=LB151WhoWantsToBeAMillionaire";
			services
				.AddDbContext<DataContext>(options => options.UseSqlServer(connection));
			services.AddTransient<LoginHandler>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
