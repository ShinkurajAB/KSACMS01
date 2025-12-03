using SwitchCMS.API.Services.Interface;

namespace SwitchCMS.API.AppService
{
    public class CompanyInActiveService : BackgroundService
    {

        private readonly IServiceProvider _serviceProvider;

        public CompanyInActiveService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                using (var scope = _serviceProvider.CreateScope())
                {
                    var companyService = scope.ServiceProvider.GetRequiredService<IOCMPService>();
                    await companyService.UpdateInActiveCustomerbyExpireDate(DateTime.Now);
                  
                }

                   
                // Perform your background task here

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken); // Delay for 5 seconds before the next iteration
            }
        }

    }
}
