using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using TaskManagementSystem.API.Core.Extension;
using TaskManagementSystem.API.Core.Helper;
using TaskManagementSystem.API.Core.Middleware;
using TaskManagementSystem.Repository.Context;

namespace TaskManagementSystem.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public static string _dbEnv;
        public static string _secret;
        public static string _containerImagePath;
        private const int RetryCount = 5;
        private const int RetryDelayTime = 1000;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddCors(options =>
           {
               options.AddDefaultPolicy(
                   builder =>
                   {
                       builder.AllowAnyOrigin()
                              .WithMethods("GET","POST", "DELETE")
                              .AllowAnyHeader();
                   });
           });

            services.Configure<AppSettings>(_configuration.GetSection("AppSettings"));

            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling
                                                                = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen();
            services.AddAutoMapper(typeof(Startup));
            ServiceCollectionDIExtension.ConfigureServicesDependency(services);
            AddHttpClient(services, "ax4api", "ax4apiBaseUrl");
            services.AddControllers();
            ConfigureDbContext(services);
        }

        private static void AddHttpClient(IServiceCollection services, string clientName, string configKey)
        {
            services.AddHttpClient(clientName, (serviceProvider, client) =>
            {
                var config = serviceProvider.GetService<IConfiguration>();
                var baseAddress = config["AppSettings:" + configKey];
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("userName", "gireesh1");
            }).AddPolicyHandler(GetRetyPolicy());
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetyPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == HttpStatusCode.BadGateway || msg.StatusCode == HttpStatusCode.InternalServerError
                || msg.StatusCode == HttpStatusCode.RequestTimeout || msg.StatusCode == HttpStatusCode.ServiceUnavailable
                || msg.StatusCode == HttpStatusCode.Unauthorized || msg.StatusCode == HttpStatusCode.NotFound
                || msg.StatusCode == HttpStatusCode.BadRequest)
                .WaitAndRetryAsync(RetryCount, retryAttempt => TimeSpan.FromMilliseconds(RetryDelayTime * Math.Pow(2, retryAttempt)));
        }

        private void ConfigureDbContext(IServiceCollection services)
        {
            //WBCrossCheckSchedulerService.IsCrossCheckScheduleEnabled = Convert.ToInt16(_configuration.GetSection("AppSettings").GetSection("IsCrossCheckSchedulerEnabled").Value);
            _dbEnv = _configuration.GetSection("AppSettings").GetSection("DbEnv").Value;
            _containerImagePath = _configuration.GetSection("AppSettings").GetSection("ContainerImagePath").Value;
            //AX4NotificationManager._isAX4IntegrationEnabled = Convert.ToInt32(_configuration.GetSection("AppSettings").GetSection("isAX4IntegrationEnabled").Value);

            services.AddDbContext<TaskContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DataSQLContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, TaskContext kGASContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //for session management
            app.UseSession();
            app.UseCookiePolicy();

            // migrate database changes on startup (includes initial db creation)
            if (_dbEnv == "SQLLITE")
                kGASContext.Database.Migrate();

            // generated swagger json and swagger ui middleware
            app.UseSwagger();
            app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "ASP.NET Core Sign-up and Verification API"));

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // custom jwt auth middleware
            app.UseMiddleware<JwtHandlerMiddleware>();

            loggerFactory.AddLog4Net();
            app.UseRouting();

            // authentication and authorization needs to be injected after routing.
            app.UseAuthorization();
            app.UseAuthorization();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
