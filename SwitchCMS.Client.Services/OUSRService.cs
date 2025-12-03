using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SwitchCMS.Client.Services.Interface;
using SwitchCMS.Model;
using SwitchCMS.Model.Authentication;
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
    public class OUSRService: IOUSRService
    {
        private readonly IConfiguration _configuration;
        private HttpClient _httpClient = new HttpClient();

        public OUSRService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration.GetSection(APIServiceURLs.BaseAddressSection).Value!);
        }

      

        public async Task<AuthenticationResponseModel> LoginUser(OUSR model)
        {
            try
            {
                string apiResponse = String.Empty;
                var Response = new AuthenticationResponseModel();
                var result = await _httpClient.PostAsJsonAsync<OUSR>(APIServiceURLs.OUSRUrls.LoginUser(), model);
                apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<AuthenticationResponseModel>(apiResponse)!;
                }
                else if (result.StatusCode == HttpStatusCode.BadRequest)
                {
                    Response.message = "Invalid UserName and Password";
                    Response.Success = false;
                    return Response;
                }
                else
                {
                    Console.WriteLine("Unknown Error" + apiResponse);
                    return new AuthenticationResponseModel();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new AuthenticationResponseModel();
            }
        }

        public async Task<OUSR> GetLoginUserDetails(string token)
        {
            try
            {
                string apiResponse = String.Empty;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.GetAsync(APIServiceURLs.OUSRUrls.GetLoginDetails());
                apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<OUSR>(apiResponse)!;
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
            catch(Exception ex)
            {
                Console.WriteLine("Unknown Error" + ex.Message);
                return new();
            }
 
        }

        public async Task<UsersPagination> GetUserByPagination(UsersPagination pagination, string token)
        {
            try
            {
                string apiResponse = String.Empty;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.PostAsJsonAsync<UsersPagination>(APIServiceURLs.OUSRUrls.GetUserByPageIndex(),pagination);
                apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<UsersPagination>(apiResponse)!;
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

        public async Task<ModificationStatus> CreeateUpdateUser(OUSR model, string token)
        {
            try
            {
                
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.PostAsJsonAsync<OUSR>(APIServiceURLs.OUSRUrls.CreateUser(), model);
                string apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<ModificationStatus>(apiResponse)!;
                }
                else if (result.StatusCode == HttpStatusCode.BadRequest)
                {
                    Console.WriteLine("Error:" + apiResponse);
                    return new ModificationStatus { Success=false, Message= "Error:" + apiResponse };
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

        public async Task<ModificationStatus> DeleteUserByUserID(int UserID, string token)
        {
            try
            {

                var result = await _httpClient.DeleteAsync(APIServiceURLs.OUSRUrls.DeleteUserByUserID(UserID));
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
