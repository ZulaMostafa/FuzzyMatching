// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
//using Microsoft.CogSLanguageUtilities.Definitions.APIs.Services;
//using Microsoft.CogSLanguageUtilities.Definitions.Exceptions.Storage;
//using Microsoft.CogSLanguageUtilities.Definitions.Models.Enums.Storage;
using FuzzyMatching.Definitions.Services;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Microsoft.CogSLanguageUtilities.Core.Services.Storage
{
    /*
    * some notes:
    *      - we use file exists in all reading methods, in order to throw our custom exception in case file wan't found
    */
    public class LocalStorageService : IStorageService
    {
        private readonly string _targetDirectory;

        public LocalStorageService(string targetDirectory)
        {
            if (!Directory.Exists(targetDirectory))
            {
                throw new FileNotFoundException();
                //throw new FolderNotFoundException(targetDirectory);
            }
            _targetDirectory = targetDirectory;
        }

        public async Task<string[]> ListFilesAsync(string fileName)
        {
            string filePath = Path.Combine(_targetDirectory, fileName);
            return await Task.FromResult(Directory.GetFiles(filePath).Select(i => Path.GetFileName(i)).ToArray());
        }

        public async Task<Object> ReadFileAsync(string fileName)
        {
            string filePath = Path.Combine(_targetDirectory, fileName);
            if (await FileExists(fileName))
            {
                try
                {
                   IFormatter formatter = new BinaryFormatter();
                   Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                   return  await Task.FromResult( formatter.Deserialize(stream));
                    
                   
                }
                catch (UnauthorizedAccessException)
                {
                    throw new UnauthorizedAccessException();
                    //throw new UnauthorizedFileAccessException(AccessType.Read.ToString(), Path.Combine(_targetDirectory, fileName));
                }
            }
            else
            {
                throw new FileNotFoundException();
                //throw new Definitions.Exceptions.Storage.FileNotFoundException(filePath);
            }
        }

        public async Task<string> ReadFileAsStringAsync(string fileName)
        {
            var filePath = Path.Combine(_targetDirectory, fileName);
            if (await FileExists(fileName))
            {
                return await File.ReadAllTextAsync(filePath);
            }
            else
            {
                throw new FileNotFoundException();
                //throw new Definitions.Exceptions.Storage.FileNotFoundException(filePath);
            }
        }

        public async Task StoreDataAsync(Object data, string fileName)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                string filePath = Path.Combine(_targetDirectory, fileName);
                Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                formatter.Serialize(stream, data);
                stream.Close();
                
               
            }
            catch (UnauthorizedAccessException)
            {
                throw new UnauthorizedAccessException();
                //throw new UnauthorizedFileAccessException(AccessType.Write.ToString(), fileName);
            }
        }

        public async Task<Object> ReadFromAbsolutePathAsync(string DatasetName, string Location)
        {
            var relativePath = Path.Combine(DatasetName, Location);
            
                return await ReadFileAsync(relativePath);
               
            
        }

        public Task<bool> FileExists(string fileName)
        {
            var filePath = Path.Combine(_targetDirectory, fileName);
            return Task.FromResult(File.Exists(filePath));
        }

        private Task<bool> FileExistsAbsolutePath(string filePath)
        {
            return Task.FromResult(File.Exists(filePath));
        }

        public Task CreateDirectoryAsync(string location)
        {
            var completePath = Path.Combine(_targetDirectory, location);
            Directory.CreateDirectory(completePath);
            return Task.CompletedTask;
        }

        public async Task StoreDataToDirectoryAsync(Object data, string location, string fileName)
        {
            var relativePath = Path.Combine(location, fileName);
            await StoreDataAsync(data, relativePath);
        }


        public async Task<Object> LoadObjectAsync(string name, string Location)
        {
            return await ReadFromAbsolutePathAsync(name, Location);

        }

       
        public async Task StoreObjectAsync(Object data, string fileName, string location)
        {
            await StoreDataToDirectoryAsync(data, location, fileName);
            return;
        }

        public async Task<string[]> ListPreprocessedDatasetsAsync(string Location)
        {
            return await ListFilesAsync(Location);
            
        }
    }
}
