using SwitchCMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Repository.Interface
{
    public interface IADV1Repository
    {
        Task<bool> InsertAdvCust(ADV1 Modal);
        Task<bool> deleteAdvbyCustID(int AdvatisementID);
    }
}
