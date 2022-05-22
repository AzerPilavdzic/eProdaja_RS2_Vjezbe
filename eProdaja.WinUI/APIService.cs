using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eProdaja.Model;

namespace eProdaja.WinUI
{
    public class APIService
    {
        public string _resource = null;
        public string _endpoint = "https://localhost:7151/";

        public static string _username=  null;
        public static string _password = null;
        public APIService(string resource)
        {
            _resource = resource;
        }
        public async Task<T> Get<T>(object search = null)
        {
            var query = "";
            if (search!=null)
            {
                //metoda ToQueryString konvertuje nas unos u onaj mahniti
                //query sa upitnicima
                query=await search.ToQueryString();
            }
            var list = await $"{_endpoint}{_resource}?{query}".WithBasicAuth(_username,_password).GetJsonAsync<T>();
            return list;
        }

        public async Task<T> GetById<T>(object id)
        {
            var result = await $"{_endpoint }{_resource}/{id}".WithBasicAuth(_username, _password).GetJsonAsync<T>(); 
            return result;
        }

        public async Task<T> Post<T>(object request)
        {
            try
            {
                var result = await $"{_endpoint }{_resource}".WithBasicAuth(_username, _password).PostJsonAsync(request).ReceiveJson<T>();
                return result;
            }
            catch (FlurlHttpException ex)
            {
                var errors = await ex.GetResponseJsonAsync<Dictionary<string, string[]>>();

                var stringBuilder = new StringBuilder();
                foreach (var error in errors)
                {
                    stringBuilder.AppendLine($"{error.Key}, ${string.Join(",", error.Value)}");
                }

                MessageBox.Show(stringBuilder.ToString(), "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return default(T);
            }

        }

        public async Task<T> Put<T>(object id, object request)
        {
            var result = await $"{_endpoint}{_resource}/{id}".WithBasicAuth(_username, _password).PutJsonAsync(request).ReceiveJson<T>();

            return result;
        }
    }
}
