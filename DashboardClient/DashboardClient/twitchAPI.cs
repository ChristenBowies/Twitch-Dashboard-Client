using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using RestSharp;

namespace DashboardClient
{
    class twitchAPI
    {
        public void getUser()
        {
            //API endpoint for Twitch
            string url = @"https://api.twitch.tv/kraken/user";


            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                
            }
            else
            {
               
            }
        }

        public class User
        {
            public string 
        } 
    }
}
