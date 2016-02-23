using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Net;
using Newtonsoft.Json;

namespace DashboardClient
{
    class login
    {
        public const string ClientID = "86a18gufgng7etbyf55fhojpe913196";
        private const string ClientSecret = "eygblnn3n0yj17rxrz17sxoe2ik4uh1";
        private string state = new XRF().generateState();

        /// <summary>
        /// Authorizes app via twitch.tv. If successful redirect is generated and code is parsed
        /// </summary>
        /// <returns></returns>
        public Code authorization()
        {
            {
                var client = new RestClient(@"https://api.twitch.tv/kraken/oauth2/authorize");
                var request = new RestRequest(Method.GET);

                request.AddParameter("response_type", "code");
                request.AddParameter("client_id", ClientID);
                request.AddParameter("redirect_url", @"https://localhost");
                request.AddParameter("scope", "chat_login");
                request.AddParameter("state", state);

                var response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //somehow grab redirect and parse code...

                }
                else
                {
                    return null;
                }
            }

        }

        //TODO
        public AccessToken Login(string userName, string password, string state)
        {
            var client = new RestClient(@"https://api.twitch.tv/kraken/oauth2/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-type", "application/json");
            request.AddBody(
                new 
                { 
                    client_id = ClientID,
                    client_secret = ClientSecret,
                    grant_type = "authorization_code",
                    redirect = @"https://localhost",
                    code = /*code received from redirect*/,
                    state = state
                });

            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<AccessToken>(response.Content);
                return result;
            }
            return null;
        }

        [JsonObject("access token")]
        public class AccessToken
        {
            [JsonProperty("access_token")]
            public string access_token { get; set; }
            [JsonProperty("scope")]
            public string[] scope { get; set; }
        }


        public class Code
        {
            public string code { get; set; }
            public string state { get; set; }
        }
    }
}
