using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.API.Services.Interface
{
    public interface IHashPasswordService
    {
        public string HashPassword(string password);
        public bool Verify(string password, string passwordHash);
    }
}
