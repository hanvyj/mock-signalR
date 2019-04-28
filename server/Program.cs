using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace mock_signalR {
  public class Program {
    public static readonly Dictionary<string, string> _switchMappings =
      new Dictionary<string, string> { { "-origins", "origins" },
        { "-hub", "hub" }
      };

    public static void Main(string[] args) {
      var portArg = args.ToList().IndexOf("-port") + 1;
      var port = portArg < args.Length ? args[portArg] : "5000";
      CreateWebHostBuilder(args).UseUrls($"http://localhost:{port}").Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
      WebHost.CreateDefaultBuilder(args)
      .ConfigureAppConfiguration((hostingContext, config) => {
        config.AddCommandLine(args, _switchMappings);
      })
      .UseStartup<Startup>();
  }
}