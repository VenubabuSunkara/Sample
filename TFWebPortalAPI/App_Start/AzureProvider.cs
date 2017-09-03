using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TransForce.API.App_Start
{
    public class AzureProvider
    {
        private static string connectionString { get; set; }
        private static string containerName { get; set; }
        static AzureProvider()
        {
            connectionString = ConfigurationManager.AppSettings["BlobStorageConnectionString"];
            containerName = ConfigurationManager.AppSettings["BlobStorageContainerName"];
        }
        public static async Task<bool> DeleteBlobAsync(string fileName)
        {
            try
            {
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
                CloudBlobClient _blobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer _cloudBlobContainer = _blobClient.GetContainerReference(containerName);
                CloudBlockBlob _blockBlob = _cloudBlobContainer.GetBlockBlobReference(fileName);
                //delete file from container    
                await _blockBlob.DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static async Task<string> UploadFilestoblobAsync(byte[] fileData, string type, string fileName)
        {
            #region create and configure
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(containerName.ToLower());

            //Create the container if it does not already exist.
            container.CreateIfNotExists();

            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            #endregion

            // Retrieve reference to a blob named "test".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
            blockBlob.Properties.ContentType = type;
            using (var ms = new MemoryStream(fileData, false))
            {
                await blockBlob.UploadFromStreamAsync(ms);
            }
            return String.Format("http://{0}{1}", blockBlob.Uri.DnsSafeHost, blockBlob.Uri.AbsolutePath);
        }
    }
}