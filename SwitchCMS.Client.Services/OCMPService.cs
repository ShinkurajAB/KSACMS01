using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class OCMPService : IOCMPService
    {
        private readonly IConfiguration _configuration;
        private HttpClient _httpClient = new HttpClient();


        public OCMPService(IConfiguration configuration) 
        {
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration.GetSection(APIServiceURLs.BaseAddressSection).Value!);
        }

        public async Task<bool> DeleteCompany(int companyId, string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await _httpClient.PutAsJsonAsync(APIServiceURLs.OCMPUrls.DeleteCompany(companyId), "");
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

        public async Task<List<OCMP>> GetAllCompany(string token)
        {
            try
            {
                string apiResponse = String.Empty;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.GetAsync(APIServiceURLs.OCMPUrls.GetAllCompany());
                apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<List<OCMP>>(apiResponse)!;
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

        public async Task<CompanyPagination> GetCompanyByPagination(CompanyPagination pagination, string token)
        {
            try
            {
                string apiResponse = String.Empty;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIServiceURLs.AuthScheme, token);
                var result = await _httpClient.PostAsJsonAsync<CompanyPagination>(APIServiceURLs.OCMPUrls.GetCompanyByPagination(), pagination);
                apiResponse = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<CompanyPagination>(apiResponse)!;
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

        public async Task<ModificationStatus> SignUpCompany(OCMP company)
        {
            try
            {
                
                var result = await _httpClient.PostAsJsonAsync<OCMP>(APIServiceURLs.OCMPUrls.SignUpCompany(), company);
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

        public async Task<bool> UpdateCompany(OCMP company, string token)
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync<OCMP>(APIServiceURLs.OCMPUrls.UpdateCompany(), company);
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
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }

        public async Task<bool> UpdateCompanyStatus(int companyId, string companyStatus, string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await _httpClient.PutAsJsonAsync(APIServiceURLs.OCMPUrls.UpdateCompanyStatus(companyId,companyStatus), "");
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
    }
}
