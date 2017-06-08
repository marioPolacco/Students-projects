/*  Wykonał Mariusz Witkowski
  
  */
using System;
using System.Collections.Generic;
using PRAM_Machine.ComplexDataTypes;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples.Find2ZerosInMatrixRow
{
    public static class Find2ZerosInMatrixRowSetup
    {
        public static IPRAMMachine Setup(int size = 2, int limit = 3)
        {
            List<Processor> processors = new List<Processor>();
            for (int row = 0; row < size; row++)
                for (int column = 0; column < size; column++)
                    processors.Add(new Find2ZerosInMatrixRowProcessor(row, column, size));
            var memory = new PRAM<MemoryTypes.CRCW>();
            var random = new Random();

            var matrix = new Matrix<int>(size, size);
            
            for(int i = 0; i < matrix.RowsCount; i++) 


        }
    }
}