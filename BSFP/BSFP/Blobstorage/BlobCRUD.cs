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
        
        public static async Task<Nieuws> CreateBlobFile(string container, IFormFile file, IConfiguration _configuration)
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

            Nieuws nieuws = new Nieuws();
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

                nieuws.ImageName = systemFileName;
                nieuws.ImagePath = "https://bsfp.blob.core.windows.net/testcontainer/" + systemFileName;
                nieuws.File = file;
                await cloudBlobContainer.SetPermissionsAsync(permissions);
                await using (var target = new MemoryStream())
                {
                    nieuws.File.CopyTo(target);
                    dataFiles = target.ToArray();
                }
                // This also does not make a service call; it only creates a local object.
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(systemFileName);
                await cloudBlockBlob.UploadFromByteArrayAsync(dataFiles, 0, dataFiles.Length);
            }
            else
            {
                nieuws.ImagePath = "../Images/logo_footer.png";
            }
            return nieuws;
        }

        public static async Task<Nieuws> EditBlobFile(Nieuws nieuwsOLD, Nieuws nieuwsNEW, string container,IConfiguration _configuration)
        {
            await DeleteBlobFile(container, nieuwsOLD, _configuration);

            Nieuws nieuws = await CreateBlobFile(container, nieuwsNEW.File, _configuration);
            return nieuws;
        }


        public static async Task DeleteBlobFile(string container, Nieuws nieuws, IConfiguration _configuration)
        {
            if (nieuws.ImageName != null)
            {
                string blobstorageconnection = _configuration.GetValue<string>("blobstorage");
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(container);
                var blob = cloudBlobContainer.GetBlobReference(nieuws.ImageName);
                await blob.DeleteIfExistsAsync();
            }

        }


        public static async Task<bool> FileExists(string fileName, CloudBlobContainer directory)
        {
            return await directory.GetBlockBlobReference(fileName).ExistsAsync();
        }
    }
}
