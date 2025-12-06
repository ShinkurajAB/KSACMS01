using SwitchCMS.API.Services.Interface;
using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using SwitchCMS.Repository;
using SwitchCMS.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.API.Services
{
    public class HEM4Service:IHEM4Service
    {
        private readonly IHEM4Repository hem4Repository;
        public HEM4Service(IHEM4Repository hem4Repository)
        {
            this.hem4Repository = hem4Repository;
        }

        public async Task<bool> DeleteOfferLetter(int id)
        {
            return await hem4Repository.DeleteOfferLetter(id);
        }

        public async Task<OfferLetterPagination> GetOfferLetterByCompanyId(OfferLetterPagination pagination)
        {
            if (pagination.PageIndex > 0)
                pagination.PageIndex--;
            pagination.OfferLetterList = await hem4Repository.GetOfferLetterByCompanyId(pagination);
            pagination.TotalCount = await hem4Repository.GetTotalOfferLetterCount(pagination.CompanyId);
            pagination.PageIndex++;

            if (pagination.TotalCount == pagination.RowCount)
                pagination.TotalPage = 1;
            else
                pagination.TotalPage = (int)Math.Ceiling(((decimal)pagination.TotalCount / (decimal)pagination.RowCount));
            return pagination;
        }

        public async Task<HEM4> GetOfferLetterById(int id)
        {
            return await hem4Repository.GetOfferLetterById(id);
        }

       

        public async Task<ModificationStatus> InsertOfferLetter(HEM4 modal)
        {
            bool Success = await hem4Repository.InsertOfferLetter(modal);
            if (Success)
            {
                return new ModificationStatus { Success = Success, Message = "Offer Letter Data Inserted Successfully" };
            }
            else
            {
                return new ModificationStatus { Success = Success, Message = "Offer Letter Data Insertion Failed" };
            }
        }

        public async Task<ModificationStatus> UpdateOfferLetter(HEM4 modal)
        {
            bool Success = await hem4Repository.UpdateOfferLetter(modal);
            if (Success)
            {
                return new ModificationStatus { Success = Success, Message = "Offer Letter Data Updated Successfully" };
            }
            else
            {
                return new ModificationStatus { Success = Success, Message = "Offer Letter Data Updation Failed" };
            }
        }
    }
}
