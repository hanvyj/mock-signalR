using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SignalRChat.Hubs;

namespace mock_signalR {
  public class Startup {
    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
      services.AddSignalR();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
      var originString = Configuration.GetValue<string>("origins");
      var hub = Configuration.GetValue<string>("hub");

      Console.WriteLine($"Origin: {originString}, Hub: {hub}");

      var origins = originString.Split(",").Select(origin => origin.Trim()).ToArray();

      app.UseDeveloperExceptionPage();
      app.UseCors(builder => {
        builder.WithOrigins(origins)
          .AllowAnyHeader()
          .WithMethods("GET", "POST", "OPTIONS")
          .AllowCredentials();
      });
      app.UseMvc();

      // Ensure our hub path starts with "/"
      if (!hub.StartsWith("/")) {
        hub = $"/{hub}";
      }
      app.UseSignalR(routes => {
        routes.MapHub<MockHub>(hub);
      });
    }
  }
}