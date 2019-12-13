using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IB150218_PCL.Util
{
   public class WebAPIHelper
    {
        private HttpClient client { get; set; }
        private string route { get; set; }
        public WebAPIHelper(string uri, string route)
        {
            client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(10);
            client.BaseAddress = new Uri(uri);
            this.route = route;
        }

        public HttpResponseMessage GetResponse()
        {
            return client.GetAsync(route).Result;
        }

        public HttpResponseMessage GetResponse(int id)
        {
            return client.GetAsync(route + "/" + id).Result;
        }

        public HttpResponseMessage GetActionResponse(string v)
        {
            return client.GetAsync(route + "/" + v).Result;
        }

        public HttpResponseMessage GetActionResponse(string action, string param)
        {
            return client.GetAsync(route + "/" + action + "/" + param).Result;

        }
        public HttpResponseMessage GetActionResponse(string action, int param)
        {
            return client.GetAsync(route + "/" + action + "/" + param).Result;

        }
        public HttpResponseMessage GetActionResponse(string action, string param, string parm1)
        {
            return client.GetAsync(route + "/" + action + "/" + param + "/" + parm1).Result;
        }
        public HttpResponseMessage GetActionResponse(string action, string param, string parm1, int treningID)
        {
            return client.GetAsync(route + "/" + action + "/" + param + "/" + parm1 + "/" + treningID).Result;
        }
        public HttpResponseMessage GetActionResponse(string action, string param, string parm4, string parm1, int treningID)
        {
            return client.GetAsync(route + "/" + action + "/" + param + "/" + parm4 + "/" + parm1 + "/" + treningID).Result;
        }
        public HttpResponseMessage PostResponse(object obj)
        {
            var jsonObject = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            return client.PostAsync(route, jsonObject).Result;
        }
        // api/Endpoint/{param}
        public async Task<HttpResponseMessage> PostResponse(string param, object obj)
        {
            var jsonObject = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            HttpResponseMessage result = await client.PostAsync(route + "/" + param + "/", jsonObject);
            return result;
        }
        public HttpResponseMessage PutResponse(int param, object obj)
        {
            var jsonObject = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            HttpResponseMessage result = client.PutAsync(route + "/" + param + "/", jsonObject).Result;
            return result;
        }
    }
}
