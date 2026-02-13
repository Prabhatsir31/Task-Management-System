using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace TaskManagementSystem.Repository.Context
{
    public class TaskContextFactory : ITaskContextFactory
    {
        private readonly IConfiguration _configuration;
        public static string TokenSecrete { get; set; }
        public static int RefreshTokenTTL { get; set; }
        public static int TokenTTL { get; set; }

        public TaskContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TaskContext CreateKGASContext()
        {
            var options = new DbContextOptionsBuilder<TaskContext>();
            options.UseSqlServer(_configuration.GetConnectionString("DataSQLContext"));
            TokenSecrete = _configuration.GetSection("AppSettings").GetSection("Secret").Value;
            RefreshTokenTTL = Convert.ToInt16(_configuration.GetSection("AppSettings").GetSection("RefreshTokenTTL").Value);
            TokenTTL = Convert.ToInt16(_configuration.GetSection("AppSettings").GetSection("TokenTTL").Value);

            return new TaskContext(options.Options);
        }
    }
}
