using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Client.Services.Interface
{
    public interface IS3Service
    {
        Task<bool> UploadFile(Stream stream, string fileName, string contentType, string CompanyID);
        Task<bool> DeleteFileAsync(string filepath, string CompanyID);
        Task<bool> FIleExistorNot(string CompanyID, string fileName);

        Task<byte[]> DownloadFile(string filepath, string CompanyID);
    }
}
