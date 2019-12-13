using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IB150218.Util
{
    class WebAPIHelper
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

        public HttpResponseMessage DeleteResponse(int id)
        {

            return client.DeleteAsync(route + "/" + id).Result;

        }

        public HttpResponseMessage GetResponse(string parametar)
        {

            return client.GetAsync(route + "/" + parametar).Result;
        }
        public HttpResponseMessage PostResponse(Object newObject)
        {

            return client.PostAsJsonAsync(route, newObject).Result;

        }
        public HttpResponseMessage GetActionResponse(string action)
        {
            return client.GetAsync(route + "/" + action).Result;
        }
        public HttpResponseMessage GetActionResponse(string action, int parameter )
        {
            return client.GetAsync(route + "/" + action + "/" + parameter).Result;
        }
        public HttpResponseMessage GetActionResponse(string action, string parameter = "")
        {
            return client.GetAsync(route + "/" + action + "/" + parameter).Result;
        }
        public HttpResponseMessage PutResponse(int id, Object existingObject)
        {
            return client.PutAsJsonAsync(route + "/" + id, existingObject).Result;
        }
        public HttpResponseMessage GetActionResponseResponse2(string action, string parameter = "", string parameter2 = "")
        {
            return client.GetAsync(route + "/" + action + "/" + parameter + "/" + parameter2).Result;
        }

        public HttpResponseMessage GetActionResponseResponse3(string action, string parameter = "", string parameter2 = "", string parameter3 = "")
        {
            return client.GetAsync(route + "/" + action + "/" + parameter + "/" + parameter2 + "/" + parameter3).Result;
        }

        public HttpResponseMessage PostActionResponse(string action, Object newObject)
        {

            return client.PostAsJsonAsync(route + "/" + action, newObject).Result;

        }
    }
}
