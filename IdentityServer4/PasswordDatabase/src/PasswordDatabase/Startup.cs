using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using NHibernate.Tool.hbm2ddl;
using PasswordDatabase.IdentityServer;

namespace PasswordDatabase
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

            var sessionFactory = Fluently.Configure()
                                            .Database(MsSqlConfiguration.MsSql2012.ConnectionString(@"Server=192.168.223.128;Database=IdentityPassword;User Id=sa;Password=Hello123#;"))
                                            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>())
                                        //.ExposeConfiguration(config =>
                                        //{
                                        //    var export = new SchemaExport(config);
                                        //    export.Execute(true, true, false);
                                        //})
                                        .BuildSessionFactory();

            services.AddSingleton(service => sessionFactory.OpenSession());

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddClientStore<ClientStore>()
                .AddResourceStore<ResourceStore>()
                .AddProfileService<ProfileService>()
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();
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
