using StackExchange.Redis;

namespace Redis.Api.BackgroundTasks
{
    public class RedisSubsrcriber : BackgroundService
    {
        private readonly IConnectionMultiplexer connectionMultiplexer;

        public RedisSubsrcriber(IConnectionMultiplexer connectionMultiplexer)
        {
            this.connectionMultiplexer = connectionMultiplexer;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var subscriber = connectionMultiplexer.GetSubscriber();
            return subscriber.SubscribeAsync("messages",
                (channel, value) =>
            {
                Console.WriteLine($"The message from {value}");
            });
        }
    }
}
