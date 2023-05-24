using Dal;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RestSharp;
using static System.Net.Mime.MediaTypeNames;

namespace bigdata
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider serviceProvider;
        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            this.serviceProvider = serviceProvider;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}
            //var collection = "weather";
            //var database = "WebberDB";
            //var source = "MorSur";

            GetAndSaveData();


        }

        public async Task GetAndSaveData()
        {
            
            var client = new RestClient("https://eu-central-1.aws.data.mongodb-api.com/app/data-gnnzj/endpoint/data/v1/action/find");
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Access-Control-Request-Headers", "*");
            request.AddHeader("api-key", "IoXtS0TMnF7fzC6AoZyb3KQ3ECFuUCdDcDzZG6V9wrE8cFmeShAa6kp1W2bfAOvc");
            var body = "{\"collection\": \"weather\", \"database\": \"WebberDB\", \"dataSource\": \"MorSur\"}";

            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.PostAsync(request);
            Root? root = JsonConvert.DeserializeObject<Root>(response.Content);
            List<Dal.Entities.Feature> features = root.Documents.Select(x => new Dal.Entities.Feature
            {
                Type = x.Type, Id = x.Id,
                Properties = x.Features.Select(z => new Dal.Entities.Properties
                {
                    Value = z.Properties.Value,
                    Created = z.Properties.Created,
                    Observed = z.Properties.Observed,
                    ParameterId = z.Properties.ParameterId,
                    StationId = z.Properties.StationId
                }
            ).ToList()
            }).ToList();

            using (BigdataContext context = serviceProvider.CreateAsyncScope().ServiceProvider.GetService<BigdataContext>())
            {
               //context.Entry(features).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //Console.WriteLine(changy);
               await context.Feature.AddRangeAsync(features);
                var changy = context.ChangeTracker.Entries();
                await context.SaveChangesAsync();
            }
        }

    }
}