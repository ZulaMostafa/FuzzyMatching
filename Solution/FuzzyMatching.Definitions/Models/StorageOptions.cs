using FuzzyMatching.Definitions.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzyMatching.Definitions.Models
{
    public class StorageOptions
    {
        private StorageType storageType;
        private string connectionString;
        private string containerName;
        private string baseDirectory;

        public StorageType StorageType { get => storageType; set => storageType = value; }
        public string ConnectionString { get => connectionString; set => connectionString = value; }
        public string ContainerName { get => containerName; set => containerName = value; }
        public string BaseDirectory { get => baseDirectory; set => baseDirectory = value; }
    }
}
