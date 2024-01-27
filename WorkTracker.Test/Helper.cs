using Microsoft.Extensions.Configuration;
using System.IO;
using WorkTracker.Models;

namespace WorkTracker.Test
{
    public static class Helper
    {
        public static AppSettings GetAppSettings()
        {
            var appSettings = new AppSettings();
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();
            configuration.GetSection("AppSettings").Bind(appSettings);

            return appSettings;
        }
    }
}
