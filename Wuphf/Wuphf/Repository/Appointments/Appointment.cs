using System;
using System.Collections.Generic;
using System.Net.Http;
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
            var result = await client.GetAsync(url);
            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException(result.ReasonPhrase);
            }
            var details = new List<Wuphf.Shared.Appointments.Appointment>();
            var content = await result.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentNullException();
            }
            details = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Wuphf.Shared.Appointments.Appointment>>(content);
            return details;
        }
    }
}
