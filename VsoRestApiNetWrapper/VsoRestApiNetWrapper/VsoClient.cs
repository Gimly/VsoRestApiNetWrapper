using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace VsoRestApiNetWrapper
{
    public class VsoClient
    {
        private const string BASE_URL_FORMAT = "https://{0}.visualstudio.com/DefaultCollection/_apis/";

        private readonly string accountName;

        private readonly string baseUrl;

        private AuthenticationType authenticationType;

        private string altUsername;

        private string altPassword;

        public VsoClient(string accountName)
        {
            this.accountName = accountName;
            this.authenticationType = AuthenticationType.NotSet;

            this.baseUrl = string.Format(BASE_URL_FORMAT, accountName);
        }

        public VsoClient UseBasicAuth(string altUsername, string altPassword)
        {
            this.authenticationType = AuthenticationType.Basic;
            this.altPassword = altPassword;
            this.altUsername = altUsername;

            return this;
        }

        public VsoClient UseOAuth(Guid applicationId, string state, Uri redirectUrl, params OAuthScope[] scopes)
        {
            this.authenticationType = AuthenticationType.OAuth;

            return this;
        }

        public string AccountName { get { return this.accountName; } }

        public async Task<Profile> GetConnectedUserProfileAsync()
        {
            using (var client = CreateHttpClient())
            {
                var response = await client.GetAsync("/profile/profiles/me?api-version=1.0");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<Profile>();
            }
        }

        public async Task<IEnumerable<Account>> GetAccountsByOwner(Guid ownerId)
        {
            using (var client = CreateHttpClient())
            {
                var response = await client.GetAsync(string.Format("Accounts/ownerId={0}", ownerId));

                response.EnsureSuccessStatusCode();

                var dto = await response.Content.ReadAsAsync<List<AccountDto>>();

                return dto.Select(
                    x => new Account
                    {
                        Id = x.AccountId,
                        Uri = x.AccountUri,
                        Name = x.AccountName,
                        OwnerId = x.AccountOwner,
                        Status = x.AccountStatus,
                        CreatorId = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        LastUpdaterId = x.LastUpdatedBy,
                        LastUpdatedDate = x.LastUpdatedDate,
                        OrganizationName = x.OrganizationName
                    });
            }
        }

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
            if (authenticationType == AuthenticationType.NotSet)
            {
                throw new InvalidOperationException("The authentication must be set before calling the web service.");
            }

            var client = new HttpClient()
            {
                BaseAddress = new Uri(this.baseUrl)
            };

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            if (this.authenticationType == AuthenticationType.Basic)
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(
                        "Basic",
                        Convert.ToBase64String(
                            StringToAscii(string.Format("{0}:{1}", altUsername, altPassword))));
            }

            return client;
        }

        private static byte[] StringToAscii(string s)
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
