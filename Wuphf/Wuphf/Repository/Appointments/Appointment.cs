using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Wuphf.Shared.Appointments;
namespace Wuphf.Repository.Appointments
{
    public class Appointment
    {
        public async Task<List<Wuphf.Shared.Appointments.Appointment>> List()
        {
            HttpClient client = new HttpClient();
            string url = Wuphf.Application.AppSettingsManager.Settings["WuphfUrl"];
            url = $"{url}Appointment";
            var result = await client.GetFromJsonAsync<Wuphf.Shared.Appointments.Appointment[]>("Appointment");        
            return result.ToList();
        }
    }
}
