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
    public class HEM5Service:IHEM5Service
    {
        private readonly IConfiguration _configuration;
        private HttpClient _httpClient = new HttpClient();
        public HEM5Service(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration.GetSection(APIServiceURLs.BaseAddressSection).Value!);
        }

        public async Task<ModificationStatus> CreateGeneralWarning(HEM5 Model, string token)
        {
            try
            {

                var result = await _httpClient.PostAsJsonAsync<HEM5>(APIServiceURLs.HEM5Url.CreateGeneralWarning(), Model);
                string apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<ModificationStatus>(apiResponse)!;
                }
                else if (result.StatusCode == HttpStatusCode.BadRequest)
                {
                    Console.WriteLine("Error:" + apiResponse);
                    return new ModificationStatus();
                }
                else
                {
                    Console.WriteLine("Unknown Error");
                    return new ModificationStatus();

                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return new ModificationStatus(); }
        }

        public async Task<ModificationStatus> DeleteGeneralWarning(int ID, string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await _httpClient.PutAsJsonAsync(APIServiceURLs.HEM5Url.DeleteGeneralWarningID(ID), "");
                string apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<ModificationStatus>(apiResponse)!;
                }
                else if (result.StatusCode == HttpStatusCode.BadRequest)
                {
                    Console.WriteLine("Error:" + apiResponse);
                    return new ModificationStatus();
                }
                else
                {
                    Console.WriteLine("Unknown Error");
                    return new ModificationStatus();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ModificationStatus();
            }
        }

        public async Task<HEM5> GetGeneralWarningByID(int ID, string token)
        {
            try
            {
                string apiResponse = String.Empty;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.GetAsync(APIServiceURLs.HEM5Url.GetGeneralWarningByID(ID));
                apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<HEM5>(apiResponse)!;
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

        public async Task<GeneralWarningPagination> GetGeneralWarningPagination(GeneralWarningPagination Pagination, string token)
        {
            try
            {
                string apiResponse = String.Empty;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.PostAsJsonAsync<GeneralWarningPagination>(APIServiceURLs.HEM5Url.GetGeneralWarningByPageIndex(), Pagination);
                apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<GeneralWarningPagination>(apiResponse)!;
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

        public async Task<ModificationStatus> UpdateGeneralWarning(HEM5 Model, string token)
        {
            try
            {

                var result = await _httpClient.PostAsJsonAsync<HEM5>(APIServiceURLs.HEM5Url.UpdateGeneralWarning(), Model);
                string apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<ModificationStatus>(apiResponse)!;
                }
                else if (result.StatusCode == HttpStatusCode.BadRequest)
                {
                    Console.WriteLine("Error:" + apiResponse);
                    return new ModificationStatus();
                }
                else
                {
                    Console.WriteLine("Unknown Error");
                    return new ModificationStatus();

                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return new ModificationStatus(); }
        }
    }
}
