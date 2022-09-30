using EM.InsurePlus.Common;
using EM.InsurePlus.Helpers;
using EM.InsurePlus.Models;
using EM.InsurePlus.Models.ServiceModels;
using EM.InsurePlus.Services.Contacts;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EM.InsurePlus.Services.ApiServices
{
    public class PolicyService : IPolicyService
    {
        public PolicyService()
        {

        }

        public async Task<VehiclePolicy> CreatePolicy(VehiclePolicy model)
        {
            UriBuilder builder = new UriBuilder(AppContants.Url);
            builder.AppendToPath($"api/vehiclePolicy/Create");
            string uri = builder.ToString();
            var client = new RestSharp.RestClient(uri);
            var request = new RestSharp.RestRequest(RestSharp.Method.POST);
            request.AddHeader("Content-type", "application/json");
            request.AddHeader("Authorization", $"Bearer {await SecureStorage.GetAsync(AppContants.AccessToken)}");
            request.RequestFormat = DataFormat.Json;
            string serialized = await Task.Run(() => JsonConvert.SerializeObject(model));

            request.AddJsonBody(serialized);
            var response = await client.ExecuteTaskAsync(request);
            if (response.StatusCode != HttpStatusCode.OK) return null;

            var result = JsonConvert.DeserializeObject<VehiclePolicy>(response.Content);
            if (result == null) return null;
            return result;

        }
    }
}
