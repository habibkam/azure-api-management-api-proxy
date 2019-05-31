using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ApiManagementProxyService
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
            services.AddScoped(typeof(HttpClient), (serviceProvider) =>
            {
                return GetHttpClient().Result;
            });

            services.Configure<ApimSettings>(Configuration.GetSection("ApimSettings"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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

        private async Task<HttpClient> GetHttpClient()
        {
            var settings = Configuration.GetSection("ApimSettings").Get<ApimSettings>();
            var AuthContext = new AuthenticationContext(settings.Authority);
            var result = await AuthContext.AcquireTokenAsync(settings.Resource, new ClientCredential(settings.ClientId, settings.ClientSecret));
            var httpClient = new HttpClient();
            httpClient = new HttpClient { BaseAddress = new Uri(settings.Resource) };
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
            return httpClient;
        }
    }
}
