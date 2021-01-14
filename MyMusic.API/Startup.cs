using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MyMusic.Core;
using MyMusic.Core.Repositories;
using MyMusic.Core.Services;
using MyMusic.Data;
using MyMusic.Data.MongoDB.Repository;
using MyMusic.Data.MongoDB.Setting;
using MyMusic.Services.Services;

namespace MyMusic.API
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
			services.AddControllers();
			//Configuration for SQL Server
			services.AddDbContext<MyMusicDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("MyMusic.Data")));
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			services.AddTransient<IMusicService, MusicService>();
			services.AddTransient<IArtistService, ArtistService>();
			//Configuration for MongoDB
			services.Configure<Settings>(
				options =>
				{
					options.ConnectionString = Configuration.GetValue<string>("MongoDB:ConnectionString");
					options.Database = Configuration.GetValue<string>("MongoDB:Database");
				}
			);
			services.AddSingleton<IMongoClient, MongoClient>(
				_ => new MongoClient(Configuration.GetValue<string>("MongoDB:ConnectionString"))
			);
			services.AddTransient<IDatabaseSettings, DatabaseSettings>();
			services.AddScoped<IComposerRepository, ComposerRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
