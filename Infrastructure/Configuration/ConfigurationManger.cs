using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Infrastructure.Configuration
{
    public class ConfigurationManger
    {
        private readonly string _sqlConnection;
        public ConfigurationManger()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();

            _sqlConnection = root.GetConnectionString("DefaultConnection");


        }
        public string SqlDataConnection
        {
            get => _sqlConnection;
        }
    }
}
