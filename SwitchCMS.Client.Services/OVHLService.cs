using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SwitchCMS.Client.Services.Interface;
using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Client.Services
{
    public class OVHLService: IOVHLService
    {
        private readonly IConfiguration _configuration;
        private HttpClient _httpClient = new HttpClient();

        public OVHLService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration.GetSection(APIServiceURLs.BaseAddressSection).Value!);
        }

        public async Task<ModificationStatus> CreateVehicle(OVHL Model, string token)
        {
            try
            {

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.PostAsJsonAsync<OVHL>(APIServiceURLs.OVHLUrl.CreateVehicle(), Model);
                string apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<ModificationStatus>(apiResponse)!;
                }
                else if (result.StatusCode == HttpStatusCode.BadRequest)
                {
                    Console.WriteLine("Error:" + apiResponse);
                    return new ModificationStatus { Success = false, Message = "Error:" + apiResponse };
                }
                else
                {
                    Console.WriteLine("Unknown Error");
                    return new ModificationStatus { Success = false, Message = "Unknown Error" };

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ModificationStatus { Success = false, Message = ex.Message };
            }
        }

       

        public async Task<VehiclePagination> GetVehicleByPageIndex(VehiclePagination Pagination, string token)
        {
            try
            {
                string apiResponse = String.Empty;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.PostAsJsonAsync<VehiclePagination>(APIServiceURLs.OVHLUrl.GetVehicleByPageIndex(), Pagination);
                apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<VehiclePagination>(apiResponse)!;
                }
                else if (result.StatusCode == HttpStatusCode.BadRequest)
                {
                    return new();
                }
                else
                {
                    Console.WriteLine("Unknown Error" + apiResponse);
                    return new();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unknown Error" + ex.Message);
                return new();
            }
        }

        public async Task<OVHL> GetVehicleByVehcileID(int VehcleID, string token)
        {
            try
            {
                string apiResponse = String.Empty;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.GetAsync(APIServiceURLs.OVHLUrl.GetVehicleByID(VehcleID));
                apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<OVHL>(apiResponse)!;
                }
                else if (result.StatusCode == HttpStatusCode.BadRequest)
                {
                    return new();
                }
                else
                {
                    Console.WriteLine("Unknown Error" + apiResponse);
                    return new();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unknown Error" + ex.Message);
                return new();
            }
        }

        public async Task<ModificationStatus> UpdateVehicle(OVHL Model, string token)
        {
            try
            {

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.PostAsJsonAsync<OVHL>(APIServiceURLs.OVHLUrl.UpdateVehicle(), Model);
                string apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<ModificationStatus>(apiResponse)!;
                }
                else if (result.StatusCode == HttpStatusCode.BadRequest)
                {
                    Console.WriteLine("Error:" + apiResponse);
                    return new ModificationStatus { Success = false, Message = "Error:" + apiResponse };
                }
                else
                {
                    Console.WriteLine("Unknown Error");
                    return new ModificationStatus { Success = false, Message = "Unknown Error" };

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ModificationStatus { Success = false, Message = ex.Message };
            }
        }

        public async Task<ModificationStatus> DeleteVehicle(int VehicleID, string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.DeleteAsync(APIServiceURLs.OVHLUrl.DeleteVehicle(VehicleID));
                string apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<ModificationStatus>(apiResponse)!;
                }
                else if (result.StatusCode == HttpStatusCode.BadRequest)
                {
                    Console.WriteLine("Error:" + apiResponse);
                    return new ModificationStatus { Success = false, Message = "Error:" + apiResponse };
                }
                else
                {
                    Console.WriteLine("Unknown Error");
                    return new ModificationStatus { Success = false, Message = "Unknown Error" };

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ModificationStatus { Success = false, Message = ex.Message };
            }
        }
    }
}
