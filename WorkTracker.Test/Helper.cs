using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WorkTracker.Models;

namespace WorkTracker.Test
{
    public class Helper
    {
        public static AppSettings getAppSettings()
        {
            AppSettings appSettings = new AppSettings();
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();
            ConfigurationBinder.Bind(configuration.GetSection("AppSettings"), appSettings);

            return appSettings;
        }
    }
}
