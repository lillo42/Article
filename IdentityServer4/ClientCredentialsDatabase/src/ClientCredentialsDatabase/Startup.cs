using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientCredentialsDatabase.IdentityServer;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using NHibernate;

namespace ClientCredentialsDatabase
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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            ISessionFactory sessionFactory = Fluently.Configure()
                                                .Database(MsSqlConfiguration.MsSql7.ConnectionString(Configuration.GetConnectionString("default")))
                                                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>())
                                                .BuildSessionFactory();

            services.AddScoped(service => sessionFactory.OpenSession());
            services.AddIdentityServer()
               .AddDeveloperSigningCredential()
               .AddClientStore<ClientStore>()
               .AddResourceStore<ResourceStore>();
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
            app.UseIdentityServer();
            app.UseMvc();
        }
    }
}
