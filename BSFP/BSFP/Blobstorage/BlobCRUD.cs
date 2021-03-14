using BSFP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BSFP.Blobstorage
{
    public class BlobCRUD
    {
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public IFormFile File { get; set; }
        public static async Task<BlobCRUD> CreateBlobFile(string container, IFormFile file, IConfiguration _configuration)
        {
            string blobstorageconnection = _configuration.GetValue<string>("blobstorage");

            byte[] dataFiles;
            // Retrieve storage account from connection string.
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
            // Create the blob client.
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            // Retrieve a reference to a container.
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(container);

            BlobContainerPermissions permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };

            BlobCRUD blob = new BlobCRUD();
            
            if (file != null)
            {
                bool fileExist = await FileExists(file.FileName, cloudBlobContainer);
                string systemFileName;
                if (fileExist)
                {
                    int getal = 0;
                    do
                    {
                        getal += 1;

                        systemFileName = file.FileName + "_" + getal + "";
                        fileExist = await FileExists(systemFileName, cloudBlobContainer);
                    } while (fileExist);
                }
                else
                {
                    systemFileName = file.FileName;
                }

                blob.ImageName = systemFileName;
                blob.ImagePath = "https://bfsp.blob.core.windows.net/" + container + "/" + systemFileName;
                blob.File = file;
                await cloudBlobContainer.SetPermissionsAsync(permissions);
                await using (var target = new MemoryStream())
                {
                    blob.File.CopyTo(target);
                    dataFiles = target.ToArray();
                }
                // This also does not make a service call; it only creates a local object.
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(systemFileName);
                await cloudBlockBlob.UploadFromByteArrayAsync(dataFiles, 0, dataFiles.Length);
            }
            else
            {
                blob.ImagePath = "../Images/logo_footer.png";
            }
            return blob;
        }

        public static async Task<BlobCRUD> EditBlobFile(string imageNameOld, IFormFile fileNEW, string container,IConfiguration _configuration)
        {
            await DeleteBlobFile(container,imageNameOld, _configuration);

            BlobCRUD blob = await CreateBlobFile(container, fileNEW, _configuration);
            return blob;
        }


        public static async Task DeleteBlobFile(string container,string imageName, IConfiguration _configuration)
        {
            if (imageName != null)
            {
                string blobstorageconnection = _configuration.GetValue<string>("blobstorage");
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(container);
                var blob = cloudBlobContainer.GetBlobReference(imageName);
                await blob.DeleteIfExistsAsync();
            }

        }


        public static async Task<bool> FileExists(string fileName, CloudBlobContainer directory)
        {
            return await directory.GetBlockBlobReference(fileName).ExistsAsync();
        }
    }
}
