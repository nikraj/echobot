using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace ATL_CMS
{
  public class Startup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors();
      services.AddMvc();

     
      JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

      services.AddAuthentication(options =>
      {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "oidc";
      })
          .AddCookie("Cookies")
          .AddOpenIdConnect("oidc", options =>
          {
            options.SignInScheme = "Cookies";

                  //Need to read urls from config
                  //local
                  options.Authority = "http://localhost:54530";

                  // For Dev env
                  //options.Authority = "https://atl-dev-identityappservice.azurewebsites.net";
                  options.RequireHttpsMetadata = false;

            options.ClientId = "CMS";
            options.ClientSecret = "secret";
            options.ResponseType = "code id_token";

            options.SaveTokens = true;
            options.GetClaimsFromUserInfoEndpoint = true;

            options.Scope.Add("api1");
            options.Scope.Add("lenderapi");
            options.Scope.Add("trusteeapi");
            options.Scope.Add("offline_access");
          });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
        {
          HotModuleReplacement = true
        });
      }
      
      //DefaultFilesOptions options = new DefaultFilesOptions();
      //options.DefaultFileNames.Clear();
      //options.DefaultFileNames.Add("index.html");
      //app.UseDefaultFiles(options);
      app.UseStaticFiles();

      app.UseAuthentication();
      app.UseCors(
        options => options.WithOrigins("http://localhost:8080").AllowAnyMethod()
    );
      app.UseMvc(routes =>
      {
        routes.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id?}");

        routes.MapSpaFallbackRoute(
            name: "spa-fallback",
            defaults: new { controller = "Home", action = "Index" });
      });

    }
  }

}
