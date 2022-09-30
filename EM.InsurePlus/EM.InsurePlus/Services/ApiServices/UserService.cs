using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EM.InsurePlus.Common;
using EM.InsurePlus.Helpers;
using EM.InsurePlus.Models;
using EM.InsurePlus.Services.Contacts;
using Xamarin.Essentials;

namespace EM.InsurePlus.Services.ApiServices
{
    public class UserService: IUserService
    {

        public async Task<bool> AuthToken(AuthorizeModel model)
        {
           
            UriBuilder builder = new UriBuilder(AppContants.Url);
            builder.AppendToPath($"api/Account/Token");
            string uri = builder.ToString();

            var client = new RestSharp.RestClient(uri);
            var request = new RestSharp.RestRequest(RestSharp.Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-type", "application/json");
            request.RequestFormat = RestSharp.DataFormat.Json;
            string serialized = await Task.Run(() => JsonConvert.SerializeObject(model));
            request.AddJsonBody(serialized);
            var response = await client.ExecuteTaskAsync(request);            

            if (!response.IsSuccessful)
            {
                await SecureStorage.SetAsync(AppContants.AccessToken, string.Empty);
                return false;
            }

            var result = JsonConvert.DeserializeObject<TokenModel>(response.Content);

            if (result == null) return false;          
             

            if (string.IsNullOrEmpty(result.Token)) return false;

            await SecureStorage.SetAsync(AppContants.AccessToken, result.Token);
             return true;
        }

        public async Task<bool> Register(User model)
        {           

            UriBuilder builder = new UriBuilder(AppContants.Url);
            builder.AppendToPath($"api/Account/Register");
            string uri = builder.ToString();
            var client = new RestSharp.RestClient(uri);
            var request = new RestSharp.RestRequest(RestSharp.Method.POST);
            request.AddHeader("Content-type", "application/json");
            request.AddHeader("Authorization", $"Bearer {await SecureStorage.GetAsync(AppContants.AccessToken)}");
            request.RequestFormat = DataFormat.Json;
            string serialized = await Task.Run(() => JsonConvert.SerializeObject(model));

            request.AddJsonBody(serialized);

            var response = await client.ExecuteTaskAsync(request);


            if (response.StatusCode != HttpStatusCode.OK) return false;

            return JsonConvert.DeserializeObject<bool>(response.Content);

        }
    }
}
