using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wuphf.Shared.Appointments;
namespace Wuphf.Repository.Appointments
{
    public class AppointmentDetails
    {
        public async Task<List<AppointmentDetail>> List()
        {
            HttpClient client = new HttpClient();
            string url = Wuphf.Application.AppSettingsManager.Settings["WuphfUrl"];
            url = $"{url}AppointmentDetail/{DateTime.Now.ToString("s")}";
            var result = await client.GetAsync(url);
            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException(result.ReasonPhrase);
            }
            var details = new List<AppointmentDetail>();
            var content = await result.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentNullException();
            }
            details = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AppointmentDetail>>(content);
            return details;
        }
        public async Task Complete(AppointmentDetail detail)
        {
            HttpClient client = new HttpClient();
            var detailJson = Newtonsoft.Json.JsonConvert.SerializeObject(detail);
            string url = Wuphf.Application.AppSettingsManager.Settings["WuphfUrl"];
            var result = await client.PostAsync($"{url}AppointmentDetail", new StringContent(detailJson, Encoding.UTF8, "application/json"));
            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException(result.ReasonPhrase);
            }
            return;
        }
    }
}
