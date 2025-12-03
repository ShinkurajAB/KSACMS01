using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SwitchCMS.Client.Services.Interface;
using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Client.Services
{
    public class OADVService : IOADVService
    {

        private readonly IConfiguration _configuration;
        private HttpClient _httpClient = new HttpClient();

        public OADVService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration.GetSection(APIServiceURLs.BaseAddressSection).Value!);
        }


        public async Task<ModificationStatus> CreateAdvatisement(OADV model, string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.PostAsJsonAsync<OADV>(APIServiceURLs.OADVUrls.CreateAdvatisement(), model);
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

       

        public async Task<AdvatisementPagination> GetAdvatisementPagination(AdvatisementPagination Pagination, string token)
        {
            try
            {
                string apiResponse = String.Empty;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.PostAsJsonAsync<AdvatisementPagination>(APIServiceURLs.OADVUrls.GetAdvatisementByPageIndex(), Pagination);
                apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<AdvatisementPagination>(apiResponse)!;
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

        public async Task<OADV> GetAdvatisementByID(int ID, string token)
        {
            try
            {
                string apiResponse = String.Empty;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.GetAsync(APIServiceURLs.OADVUrls.GetAdvatisementByID(ID));
                apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<OADV>(apiResponse)!;
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

        public async Task<ModificationStatus> UpdateAdvatisement(OADV model, string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.PostAsJsonAsync<OADV>(APIServiceURLs.OADVUrls.UpdateAdvatisement(), model);
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

        public async Task<ModificationStatus> DeleteAdvatisement(int ID, string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.DeleteAsync(APIServiceURLs.OADVUrls.DeleteAdvatisementID(ID));
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

        public async Task<List<OADV>> GetAdvatisementByCustID(int CustID, string token)
        {
            try
            {
                string apiResponse = String.Empty;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.GetAsync(APIServiceURLs.OADVUrls.GetAdvatisementByCustID(CustID));
                apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<List<OADV>>(apiResponse)!;
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
    }
}
