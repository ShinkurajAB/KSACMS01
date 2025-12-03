using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using SwitchCMS.Client.Services.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Client.Services
{
    public class S3Service: IS3Service
    {
        private readonly string AccessKey;
        private readonly string secretKey;
        private readonly string bucketName;
        private readonly string ServiceURL;
        private readonly IAmazonS3 s3Client;
        private readonly string FilePath;


        private readonly IConfiguration config;

        public S3Service(IConfiguration _config)
        {
            this.config = _config;

            AccessKey = config.GetSection("S3:accessKey").Value!;
            secretKey = config.GetSection("S3:secretkey").Value!;
            bucketName = config.GetSection("S3:bucketname").Value!;
            ServiceURL = config.GetSection("S3:ServiceUrl").Value!;
            FilePath = config.GetSection("S3:FileSavePath").Value!;

            var credentials = new BasicAWSCredentials(AccessKey, secretKey);
            s3Client = new AmazonS3Client(credentials, new AmazonS3Config
            {
                ServiceURL = ServiceURL,
                ForcePathStyle = true

            });
        }


        public async Task<bool> UploadFile(Stream stream, string fileName, string contentType, string CompanyID)
        {
            var putRequest = new Amazon.S3.Model.PutObjectRequest
            {
                BucketName = bucketName,
                Key = $"{FilePath}{CompanyID}/{fileName}", // Folder inside bucket
                InputStream = stream,
                ContentType = contentType,
                DisablePayloadSigning = true,
                DisableDefaultChecksumValidation = true
            };

            try
            {
                var response = await s3Client.PutObjectAsync(putRequest);
                return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch(Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> DeleteFileAsync(string fileName, string CompanyID)
        {
            try
            {
                var deleteRequest = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = $"{FilePath}{CompanyID}/{fileName}" // Example: "uploads/myphoto.jpg"
                };

                var response = await s3Client.DeleteObjectAsync(deleteRequest);
                return response.HttpStatusCode == System.Net.HttpStatusCode.NoContent;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> FIleExistorNot(string CompanyID, string fileName)
        {
            try
            {
                var deleteRequest = new GetObjectMetadataRequest
                {
                    BucketName = bucketName,
                    Key = $"{FilePath}{CompanyID}/{fileName}" // Example: "uploads/myphoto.jpg"
                };

                var response = await s3Client.GetObjectMetadataAsync(deleteRequest);
                return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }
    }
}
