using Microsoft.AspNetCore.Components.Authorization;
using SwitchCMS.Client.Services;
using SwitchCMS.Client.Services.Interface;
using SwitchCMS.Component.Auth;

namespace SwitchCMS.Client.AppService
{
    public class DIService
    {
        public static void AddService(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddScoped<IOUSRService,OUSRService>();

            builder.Services.AddScoped<IOCMPService, OCMPService>();
            builder.Services.AddScoped<IOCRYService, OCRYService>();

            builder.Services.AddScoped<IOMENService, OMENService>();
            builder.Services.AddScoped<IOACAService, OACAService>();

            builder.Services.AddScoped<IOHEMService, OHEMService>();

            builder.Services.AddScoped<IOADVService, OADVService>();
            builder.Services.AddScoped<IS3Service, S3Service>();

            builder.Services.AddScoped<IOVHLService, OVHLService>();

            builder.Services.AddScoped<IOATCService, OATCService>();

            builder.Services.AddScoped<IVHL1Service, VHL1Service>();
        }
    }
}
