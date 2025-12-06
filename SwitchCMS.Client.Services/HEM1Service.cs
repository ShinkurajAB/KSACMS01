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
    public class HEM1Service:IHEM1Service
    {
        private readonly IConfiguration _configuration;
        private HttpClient _httpClient = new HttpClient();
        public HEM1Service(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration.GetSection(APIServiceURLs.BaseAddressSection).Value!);
        }

        public async Task<bool> DeleteResignation(int id, string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await _httpClient.PutAsJsonAsync(APIServiceURLs.HEM1Url.DeleteResignation(id), "");
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

        public async Task<HEM1> GetResignationById(int id, string token)
        {
            try
            {
                string apiResponse = String.Empty;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.GetAsync(APIServiceURLs.HEM1Url.GetResignationById(id));
                apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<HEM1>(apiResponse)!;
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

        public async Task<EmployeeResignationPagination> GetResignationsByCompanyId(EmployeeResignationPagination pagination, string token)
        {
            try
            {
                string apiResponse = String.Empty;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.PostAsJsonAsync<EmployeeResignationPagination>(APIServiceURLs.HEM1Url.GetResignationsByPagination(), pagination);
                apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<EmployeeResignationPagination>(apiResponse)!;
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

        public async Task<ModificationStatus> InsertResignation(HEM1 modal, string token)
        {
            try
            {

                var result = await _httpClient.PostAsJsonAsync<HEM1>(APIServiceURLs.HEM1Url.InsertResignation(), modal);
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

        public async Task<ModificationStatus> UpdateResignation(HEM1 modal, string token)
        {
            try
            {

                var result = await _httpClient.PostAsJsonAsync<HEM1>(APIServiceURLs.HEM1Url.UpdateResignation(), modal);
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
