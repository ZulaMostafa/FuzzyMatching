// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FuzzyMatching.Definitions.Models;
using FuzzyMatching.Definitions.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.CogSLanguageUtilities.Core.Services.Storage
{
    /*
     * some notes:
     *      - we use file exists in all reading methods, in order to throw our custom exception in case file wan't found
     */
    public class BlobStorageService : IStorageService
    {
        private readonly BlobContainerClient _blobContainerClient;

        public BlobStorageService(string connectionString, string containerName)
        {
            try
            {
                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
                _blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
                if (!_blobContainerClient.Exists())
                {
                    throw new FileLoadException();
                   // throw new BlobContainerNotFoundException(containerName);
                }
            }
            catch (Exception e) when (e is RequestFailedException || e is FormatException || e is AggregateException)
            {
                throw new Exception() ;
            }
        }

        public async Task<string[]> ListFilesAsync()
        {
            var blobs = _blobContainerClient.GetBlobsAsync();
            List<string> blobNames = new List<string>();
            await foreach (BlobItem blob in blobs)
            {
                blobNames.Add(blob.Name);
            }
            return blobNames.ToArray();
        }

        public async Task<Stream> ReadFileAsync(string fileName)
        {
            if (await FileExists(fileName))
            {
                BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
                BlobDownloadInfo download = await blobClient.DownloadAsync();
                return download.Content;
            }
            else
            {
                throw new FileNotFoundException();
                //throw new Definitions.Exceptions.Storage.FileNotFoundException(fileName);
            }
        }

        public async Task<string> ReadFileAsStringAsync(string fileName)
        {
            if (await FileExists(fileName))
            {
                BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
                BlobDownloadInfo download = await blobClient.DownloadAsync();
                using (StreamReader sr = new StreamReader(download.Content))
                {
                    return sr.ReadToEnd();
                }
            }
            else
            {
                throw new FileNotFoundException();
                //throw new Definitions.Exceptions.Storage.FileNotFoundException(fileName);
            }
        }

        public async Task StoreDataAsync(Object data, string fileName)
        {
            BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
            using (MemoryStream stream = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(stream))
                {
                    sw.Write(data);
                    sw.Flush();
                    stream.Position = 0;
                    await blobClient.UploadAsync(stream, overwrite: true);
                }
            }
        }

        public async Task<Stream> ReadFromAbsolutePathAsync(string fileName , string location)
        {
            var relativePath = Path.Combine(fileName, location);
            return await ReadFileAsync(relativePath);
            
        }

        public async Task<bool> FileExists(string fileName)
        {
            return await Task.Run(() =>
            {
                return _blobContainerClient.GetBlobClient(fileName).Exists();
            });
        }

       

        public async Task StoreDataToDirectoryAsync(Object data, string location, string fileName)
        {
            var relativePath = Path.Combine(location, fileName);
            await StoreDataAsync(data, relativePath);
        }



         
    
        public async Task StoreObjectAsync(Object data, string fileName, string location)
        {
            await StoreDataToDirectoryAsync(data, location, fileName);
            return;
        }


        public async Task<string[]> ListPreprocessedDatasetsAsync(string location)
        {
            return await ListFilesAsync();
            
        }

        public async Task<Object> LoadObjectAsync(string name, string location)
        {
           return await ReadFromAbsolutePathAsync(name, location);
        
        }

       
        
    }
}