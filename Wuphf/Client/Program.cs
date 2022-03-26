using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using MudBlazor.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wuphf.Shared.Session;
using Wuphf.Shared.Appointments;
using Wuphf.Shared.Configuration;
using Wuphf.Shared.Dialog;
using Wuphf.Client.Models.Dialog;

namespace Wuphf.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddSingleton<IAppSettings, AppSettings>(sp => new AppSettings { WuphfURL = builder.Configuration["serverUrl"] });
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["serverUrl"]) });
            builder.Services.AddMudServices();
            builder.Services.AddSingleton<Session>();
            builder.Services.AddTransient<IAppointmentsRepo, AppointmentsRepo>();
            builder.Services.AddSingleton<IMsgBox, MsgBox>();
            await builder.Build().RunAsync();
        }
    }
}
