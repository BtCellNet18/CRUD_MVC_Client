using CrudApiClient.Interfaces;
using CrudApiClient.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CRUD_MVC_Client
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			string baseUrl = Configuration["AppSettings:BaseUrl"];		
			services.AddHttpClient<IAuthService, AuthService>(x => x.BaseAddress = new Uri(baseUrl));
			services.AddHttpClient<IUserService, UserService>(x => x.BaseAddress = new Uri(baseUrl));
			services.AddDistributedMemoryCache();
			services.AddControllersWithViews();

			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromSeconds(300);
			});

			services.AddMvc(option => option.EnableEndpointRouting = false);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSession();
			app.UseRouting();
			app.UseMvc();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
									name: "default",
									pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
