using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Net;

namespace DashboardClient
{
    class login
    {
        public string ClientID = "86a18gufgng7etbyf55fhojpe913196";
        private string ClientSecret = "eygblnn3n0yj17rxrz17sxoe2ik4uh1";
        private string state = new XRF().generateState();

        public Code authorization()
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
                Code xrf = new Code();
                return xrf;
            }
            else
            {
                return null;
            }

        }

        public UserLogin Login(string userName, string password, Code xrf)
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
                    code = xrf.code,
                    state = state
                });
            return null;
        }

        public class UserLogin
        {
            public string Token { get; set; }
            public string[] Scope { get; set; }
        }

        public class Code
        {
            public string code { get; set; }
        }
    }
}
