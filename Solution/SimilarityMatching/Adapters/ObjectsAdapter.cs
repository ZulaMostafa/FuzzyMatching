using FuzzyMatching.Definitions.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzyMatching.Core.Adapters
{
    public static class ObjectsAdapter
    {
        public static StoredProcessedDataset ProcessedToStored (ProcessedDataset processedDataset)
        {
            var storedDataset = new StoredProcessedDataset();
            storedDataset.TFIDFMatrix = ArraysAdapter.Make1DArray<float>(processedDataset.TFIDFMatrix);
            storedDataset.TFIDFMatrixAbsoluteValues = processedDataset.TFIDFMatrixAbsoluteValues;
            storedDataset.IDFVector = processedDataset.IDFVector;
            storedDataset.UniqueNGramsVector = processedDataset.UniqueNGramsVector;
            storedDataset.Height = processedDataset.TFIDFMatrix.Length;
            storedDataset.Width = processedDataset.TFIDFMatrix[0].Length;
            return storedDataset;
        }
        public static ProcessedDataset StoredToProcessed (StoredProcessedDataset storedDataset)
        {
            var processedDataset = new ProcessedDataset();
            processedDataset.TFIDFMatrix = ArraysAdapter.Make2DArray<float>(storedDataset.TFIDFMatrix,storedDataset.Height,storedDataset.Width);
            processedDataset.TFIDFMatrixAbsoluteValues = storedDataset.TFIDFMatrixAbsoluteValues;
            processedDataset.IDFVector = storedDataset.IDFVector;
            processedDataset.UniqueNGramsVector = storedDataset.UniqueNGramsVector;
       
            return processedDataset;
        }


    }
}
