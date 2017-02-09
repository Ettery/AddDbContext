using DbContextInherit.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace DbContextInherit
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer();
            services.AddDbContext<QueryContext>(options => options.UseSqlServer("Data Source=QueryContext;Initial Catalog=YourData;Integrated Security=SSPI;"));
            services.AddDbContext<OtherContext>(options => options.UseSqlServer("Data Source=OtherContext;Initial Catalog=MyData;Integrated Security=SSPI;"));
            services.AddDbContext<SpecificCommandContext>(options => options.UseSqlServer("Data Source=SpecificCommandContext;Initial Catalog=MyData;Integrated Security=SSPI;"));
            services.AddDbContext<NonSpecificCommandContext>(options => options.UseSqlServer("Data Source=NonSpecificCommandContext;Initial Catalog=MyData;Integrated Security=SSPI;"));
            services.AddDbContext<CommandContext>(options => options.UseSqlServer("Data Source=CommandContext;Initial Catalog=MyData;Integrated Security=SSPI;"));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
