using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using static System.Console;

namespace Client
{
    class Program
    {
        private const string VALUES = "api/values";
        private const string URL = "https://localhost:44315";
        private const string URL_AUTORIZATION = "https://localhost:44387";
        static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();
        static async Task MainAsync()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(URL)
            };

            try
            {
                await GetAllAsync(httpClient);
                WriteLine();
                await GetAsync(httpClient, 1);
                WriteLine();
                await AddWithoutAuthorizationAsync(httpClient, "test");
                WriteLine();
                await GetAuthorization(httpClient);
                WriteLine();
                await GetInvalidAuthorization(httpClient);
                WriteLine();
                await AddAuthorizationAsync(httpClient, "test");
                WriteLine();
                await GetAllAsync(httpClient);
            }
            catch (Exception e)
            {

                WriteLine("Unexpect error");
                WriteLine(e.ToString());
            }

            ReadLine();
        }

        static async Task GetAllAsync(HttpClient httpClient)
        {
            WriteLine($"get all from {VALUES}");

            HttpResponseMessage httpResponse = await httpClient.GetAsync(VALUES);
            WriteLine($"Http Status Code from {VALUES}: {httpResponse.StatusCode.ToString()} - expect {HttpStatusCode.OK}");
            WriteLine($"Content message from {VALUES}: {await httpResponse.Content.ReadAsStringAsync()}");
        }

        static async Task GetAsync(HttpClient httpClient, int id)
        {
            string url = $"{VALUES}/{id}";
            WriteLine($"get all from {url}");

            HttpResponseMessage httpResponse = await httpClient.GetAsync(url);
            WriteLine($"Http Status Code from {url}: {httpResponse.StatusCode.ToString()} - expect {HttpStatusCode.OK}");
            WriteLine($"Content message from {url}: {await httpResponse.Content.ReadAsStringAsync()}");
        }

        static async Task AddWithoutAuthorizationAsync(HttpClient httpClient, string value)
        {
            WriteLine($"add to {VALUES} Without Authorization");

            HttpResponseMessage httpResponse = await httpClient.PostAsync(VALUES, new StringContent(value));
            WriteLine($"Http Status Code from {VALUES}: {httpResponse.StatusCode.ToString()} - expect {HttpStatusCode.Unauthorized}");
            WriteLine($"Content message from {VALUES}: {await httpResponse.Content.ReadAsStringAsync()}");
        }

        static async Task GetAuthorization(HttpClient httpClient)
        {
            string urlAuthorization = "connect/token";
            WriteLine($"get authorization from {URL_AUTORIZATION}/{urlAuthorization}");

            var authHttpClient = new HttpClient
            {
                BaseAddress = new Uri(URL_AUTORIZATION)
            };

            var form = new FormUrlEncodedContent
            (
                new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string,string>("client_id","test"),
                    new KeyValuePair<string,string>("client_secret","123"),
                    new KeyValuePair<string,string>("grant_type","client_credentials")
                });

            HttpResponseMessage httpResponse = await authHttpClient.PostAsync(urlAuthorization, form);
            WriteLine($"Http Status Code from {VALUES}: {httpResponse.StatusCode.ToString()} - expect {HttpStatusCode.OK}");
            WriteLine($"Content message from {VALUES}: {await httpResponse.Content.ReadAsStringAsync()}");
        }

        static async Task GetInvalidAuthorization(HttpClient httpClient)
        {
            string urlAuthorization = "connect/token";
            WriteLine($"get authorization from {URL_AUTORIZATION}/{urlAuthorization}");
            var authHttpClient = new HttpClient
            {
                BaseAddress = new Uri(URL_AUTORIZATION)
            };

            var form = new FormUrlEncodedContent
            (
                new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string,string>("client_id","test"),
                    new KeyValuePair<string,string>("client_secret","1234"),
                    new KeyValuePair<string,string>("grant_type","client_credentials")
                });

            HttpResponseMessage httpResponse = await authHttpClient.PostAsync(urlAuthorization, form);
            WriteLine($"Http Status Code from {VALUES}: {httpResponse.StatusCode.ToString()} - expect {HttpStatusCode.BadRequest}");
            WriteLine($"Content message from {VALUES}: {await httpResponse.Content.ReadAsStringAsync()}");
        }

        static async Task AddAuthorizationAsync(HttpClient httpClient, string value)
        {
            WriteLine($"add to {VALUES} with authorization");

            var authHttpClient = new HttpClient
            {
                BaseAddress = new Uri(URL_AUTORIZATION)
            };

            var form = new FormUrlEncodedContent
            (
                new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string,string>("client_id","test"),
                    new KeyValuePair<string,string>("client_secret","123"),
                    new KeyValuePair<string,string>("grant_type","client_credentials")
                });

            HttpResponseMessage httpResponse = await authHttpClient.PostAsync("connect/token", form);
            AccessTokenViewModel accessToken = JsonConvert.DeserializeObject<AccessTokenViewModel>(await httpResponse.Content.ReadAsStringAsync());
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(accessToken.TokenType, accessToken.AccessToken);
            httpResponse = await httpClient.PostAsync(VALUES, new StringContent(value));
            WriteLine($"Http Status Code from {VALUES}: {httpResponse.StatusCode.ToString()} - expect {HttpStatusCode.OK}");
        }
    }

}
