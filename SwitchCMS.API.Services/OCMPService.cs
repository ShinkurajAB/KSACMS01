using SwitchCMS.API.Services.Interface;
using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using SwitchCMS.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SwitchCMS.API.Services
{
    public class OCMPService : IOCMPService
    {

        private readonly IOCMPRepository CompanyRepository;

        public OCMPService(IOCMPRepository CompanyRepository)
        {
            this.CompanyRepository = CompanyRepository;
        }

        public async Task<bool> DeleteCompany(int companyId)
        {
            bool isDelete = await CompanyRepository.DeleteCompany(companyId);
            return isDelete;
        }

        public async Task<List<OCMP>> GetAllCompany()
        {
            var data= await CompanyRepository.GetAllCompanys();
            return data;
        }


        public async Task<CompanyPagination> GetCompanyByPagination(CompanyPagination pagination)
        {
            if (pagination.PageIndex > 0)
                pagination.PageIndex--;
            pagination.CompanyList = await CompanyRepository.GetCompanyByPageIndex(pagination);
            pagination.TotalCount  = await CompanyRepository.GetTotalCompanyCount(pagination);
            pagination.PageIndex++;

            if (pagination.TotalCount == pagination.RowCount)
                pagination.TotalPage = 1;
            else
                pagination.TotalPage = (int)Math.Ceiling(((decimal)pagination.TotalCount / (decimal)pagination.RowCount));
            return pagination;
        }

      
        public async Task<bool> SignUpCompany(OCMP company)
        {
            bool isSuccess = await CompanyRepository.SignUpCompany(company);
            return isSuccess;
        }

        public async Task<bool> UpdateCompany(OCMP company)
        {
            bool isSuccess = await CompanyRepository.UpdateCompany(company);
            return isSuccess;
        }

        public async Task<bool> UpdateCompanyStatus(int companyId, string companyStatus)
        {
            bool isSuccess = await CompanyRepository.UpdateCompanyStatus(companyId,companyStatus);
            return isSuccess;
        }


        public async Task<bool> UpdateInActiveCustomerbyExpireDate(DateTime Date)
        {
             var Success= await CompanyRepository.UpdateInActiveCustomerbyExpireDate(Date);
            return Success;
        }
        public async Task<OCMP> GetCustomerByCustID(int ID)
        {
            var Data= await CompanyRepository.GetCustomerByCustID(ID);
            return Data;
            
        }

    }
}
