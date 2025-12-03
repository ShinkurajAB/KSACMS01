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
    public class OATCService : IOATCService
    {
        private readonly IConfiguration _configuration;
        private HttpClient _httpClient = new HttpClient();

        public OATCService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration.GetSection(APIServiceURLs.BaseAddressSection).Value!);
        }


        public async Task<ModificationStatus> CreateDocument(OATC Model, string token)
        {
            try
            {
                string apiResponse = String.Empty;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.PostAsJsonAsync<OATC>(APIServiceURLs.OATCUrl.CreateDocument(), Model);
                apiResponse = await result.Content.ReadAsStringAsync();
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
            catch (Exception ex) { Console.WriteLine(ex.Message); return new ModificationStatus { Success = false, Message = ex.Message }; }
        }

      

        public async Task<DocumentPagination> GetDocumentByPageIndex(DocumentPagination Pagination, string token)
        {
            try
            {
                string apiResponse = String.Empty;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.PostAsJsonAsync<DocumentPagination>(APIServiceURLs.OATCUrl.GetDocumentPageIndex(), Pagination);
                apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<DocumentPagination>(apiResponse)!;
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

        public async Task<OATC> GetDocumentByID(int ID, string token)
        {
            try
            {
                string apiResponse = String.Empty;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.GetAsync(APIServiceURLs.OATCUrl.GetDocumentByID(ID));
                apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<OATC>(apiResponse)!;
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

        public async Task<ModificationStatus> DeleteDocumentByID(int ID, string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.DeleteAsync(APIServiceURLs.OATCUrl.DeleteDocumentByID(ID));
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

        public async Task<ModificationStatus> UpdateDocumentByID(OATC Model, string token)
        {
            try
            {
                string apiResponse = String.Empty;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.PostAsJsonAsync<OATC>(APIServiceURLs.OATCUrl.UpdateDocument(), Model);
                apiResponse = await result.Content.ReadAsStringAsync();
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
            catch (Exception ex) { Console.WriteLine(ex.Message); return new ModificationStatus { Success = false, Message = ex.Message }; }

        }
    }
}
