using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wuphf.Shared;
using Wuphf.Shared.Configuration;

namespace Wuphf.Shared.Repository.Login
{
    public class Login
    {
        IAppSettings appSettings;
        public Login(IAppSettings appSettings)
        {
            this.appSettings = appSettings;
        }
        public async Task<Guid> Process(LoginRequest login)
        {
            HttpClient client = new HttpClient();
            var loginJson = Newtonsoft.Json.JsonConvert.SerializeObject(login);
            string url = appSettings.WuphfURL;
            var result = await client.PostAsync($"{url}/Account/{login.UserName}", new StringContent(loginJson, Encoding.UTF8, "application/json"));
            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException(result.ReasonPhrase);
            }
            var content = await result.Content.ReadAsStringAsync();
            Guid token = Guid.Empty;
            if (!string.IsNullOrEmpty(content) && !Guid.TryParse(content, out token))
            {
                throw new HttpRequestException(content);
            }
            return token;
        }
    }
}
