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
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Client.Services
{
    public class HEM3Service:IHEM3Service
    {
        private readonly IConfiguration _configuration;
        private HttpClient _httpClient = new HttpClient();
        public HEM3Service(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration.GetSection(APIServiceURLs.BaseAddressSection).Value!);
        }

        public async Task<bool> DeleteAbsentee(int id, string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await _httpClient.PutAsJsonAsync(APIServiceURLs.HEM3Url.DeleteAbsentee(id), "");
                string apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return Convert.ToBoolean(apiResponse);
                }
                else if (result.StatusCode == HttpStatusCode.BadRequest)
                {
                    Console.WriteLine("Error:" + apiResponse);
                    return false;
                }
                else
                {
                    Console.WriteLine("Unknown Error");
                    return false;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<HEM3> GetAbsenteeById(int id, string token)
        {
            try
            {
                string apiResponse = String.Empty;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.GetAsync(APIServiceURLs.HEM3Url.GetAbsenteeById(id));
                apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<HEM3>(apiResponse)!;
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

        public async Task<EmployeeAbsencePagination> GetAbsenteesByCompanyId(EmployeeAbsencePagination pagination, string token)
        {
            try
            {
                string apiResponse = String.Empty;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.PostAsJsonAsync<EmployeeAbsencePagination>(APIServiceURLs.HEM3Url.GetAbsenteesByPagination(), pagination);
                apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<EmployeeAbsencePagination>(apiResponse)!;
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

        public async Task<ModificationStatus> InsertAbsentee(HEM3 modal, string token)
        {
            try
            {

                var result = await _httpClient.PostAsJsonAsync<HEM3>(APIServiceURLs.HEM3Url.InsertAbsentee(), modal);
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

        public async Task<ModificationStatus> UpdateAbsentee(HEM3 modal, string token)
        {
            try
            {

                var result = await _httpClient.PostAsJsonAsync<HEM3>(APIServiceURLs.HEM3Url.UpdateAbsentee(), modal);
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
