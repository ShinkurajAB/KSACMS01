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
    public class HEM4Service:IHEM4Service
    {
        private readonly IConfiguration _configuration;
        private HttpClient _httpClient = new HttpClient();
        public HEM4Service(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration.GetSection(APIServiceURLs.BaseAddressSection).Value!);
        }

        public async Task<bool> DeleteOfferLetter(int id, string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await _httpClient.PutAsJsonAsync(APIServiceURLs.HEM4Url.DeleteOfferLetter(id), "");
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

        public async Task<OfferLetterPagination> GetOfferLetterByCompanyId(OfferLetterPagination pagination, string token)
        {
            try
            {
                string apiResponse = String.Empty;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.PostAsJsonAsync<OfferLetterPagination>(APIServiceURLs.HEM4Url.GetOfferLetterByPagination(), pagination);
                apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<OfferLetterPagination>(apiResponse)!;
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

        public async Task<HEM4> GetOfferLetterById(int id, string token)
        {
            try
            {
                string apiResponse = String.Empty;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.GetAsync(APIServiceURLs.HEM4Url.GetOfferLetterById(id));
                apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<HEM4>(apiResponse)!;
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

        public async Task<ModificationStatus> InsertOfferLetter(HEM4 modal, string token)
        {
            try
            {

                var result = await _httpClient.PostAsJsonAsync<HEM4>(APIServiceURLs.HEM4Url.InsertOfferLetter(), modal);
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

        public async Task<ModificationStatus> UpdateOfferLetter(HEM4 modal, string token)
        {
            try
            {

                var result = await _httpClient.PostAsJsonAsync<HEM4>(APIServiceURLs.HEM4Url.UpdateOfferLetter(), modal);
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
