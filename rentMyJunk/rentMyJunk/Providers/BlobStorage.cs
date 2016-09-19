using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace rentMyJunk.Providers
{

    public class BlobStorage
    {
        private static CloudStorageAccount _storageAccount;
        private static CloudBlobClient _storageClient;

        public BlobStorage()
        {
            _storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["blobendpoint"]);
            _storageClient = _storageAccount.CreateCloudBlobClient();
        }

        public string SaveBlob(string id, Stream image)
        {
            string blobName = id + ".jpg";

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = _storageClient.GetContainerReference("images");

            //Retrieve a reference to the blob we're going to upload.
            CloudBlockBlob blob = container.GetBlockBlobReference(blobName);

            //upload the blob from the stream.
            blob.UploadFromStream(image);

            //return the blob's uri after creation.
            return blob.Uri.ToString();
        }
    }

    
}