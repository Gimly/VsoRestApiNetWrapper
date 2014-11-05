using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace VsoRestApiNetWrapper
{
    public class VsoAccount
    {
        private const string BASE_URL_FORMAT = "https://{0}.visualstudio.com/DefaultCollection/_apis/";

        private readonly string accountName;

        private readonly string baseUrl;

        private string altUsername;

        private string altPassword;

        public VsoAccount(string accountName, string altUsername, string altPassword)
        {
            this.accountName = accountName;
            this.altPassword = altPassword;
            this.altUsername = altUsername;

            this.baseUrl = string.Format(BASE_URL_FORMAT, accountName);
        }

        public string AccountName { get { return this.accountName; } }

        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            using (var client = CreateHttpClient())
            {
                var response = await client.GetAsync("projects");

                response.EnsureSuccessStatusCode();

                var projectsList = await response.Content.ReadAsAsync<ProjectsListDto>();

                return projectsList.Projects.Select(
                    p => new Project
                    {
                        Id = p.Id,
                        Name = p.Name,
                        State = p.State,
                        Url = new Uri(p.Url)
                    });
            }
        }

        private HttpClient CreateHttpClient()
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri(this.baseUrl)
            };

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(
                        StringToAscii(string.Format("{0}:{1}", altUsername, altPassword))));

            return client;
        }

        public static byte[] StringToAscii(string s)
        {
            byte[] retval = new byte[s.Length];
            for (int ix = 0; ix < s.Length; ++ix)
            {
                char ch = s[ix];
                if (ch <= 0x7f) retval[ix] = (byte)ch;
                else retval[ix] = (byte)'?';
            }
            return retval;
        }
    }
}
