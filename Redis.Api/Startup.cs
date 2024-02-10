using Microsoft.Extensions.DependencyInjection.Extensions;
using Redis.Api.BackgroundTasks;
using Redis.Api.Services;
using StackExchange.Redis;

namespace Redis.Api
{
    public sealed class Startup
    {
        public IConfiguration Config { get; }
        public Startup(IConfiguration configuration)
        {
            this.Config = configuration;
        }

        // This method gets called by the runtime. Use this method to add serices to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddSingleton<IConnectionMultiplexer>(_ =>
            ConnectionMultiplexer.Connect(Config.GetValue<string>("RedisConnection"))
            );
            //services.TryAdd(ServiceDescriptor.Singleton<ICacheService, InMemoryCacheService>());
            services.TryAdd(ServiceDescriptor.Singleton<ICacheService, RedisCacheService>());
            //services.AddSingleton<ICacheService, InMemoryCacheService>();
            services.AddHostedService<RedisSubsrcriber>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}