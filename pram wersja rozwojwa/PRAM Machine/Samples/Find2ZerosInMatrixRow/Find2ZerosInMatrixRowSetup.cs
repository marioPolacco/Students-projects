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
        public static IPRAMMachine Setup(int matrixSize, int randLimit = 2)
        {
            int row, column;
            var processors = new List<Processor>();

            for (row = 0; row < matrixSize; row++)
                for (column = 0; column < matrixSize; column++)
                    processors.Add(new Find2ZerosInMatrixRowProcessor(row, column, matrixSize));

            var memory = new PRAM<MemoryTypes.CRCW>();
            var binaryGenerator = new Random();
            var matrixA = new Matrix<int>(matrixSize, matrixSize);
            List<bool> matrixB = new List<bool>(matrixSize);
            //var matrixB = new Matrix<bool>(matrixSize, 1);

            for (int i = 0; i < matrixSize; i++){
                for (int j = 0; j < matrixSize; j++)
                    matrixA[i, j] = binaryGenerator.Next(randLimit);
                matrixB.Add(false);
                //matrixB[i, 0] = false;
            }
            memory.AddNamedMemory("MatrixA", matrixA);
            memory.AddNamedMemory("MatrixB", matrixB);
            memory.AddNamedMemory("Result", true);

            var machine = new PRAMMachine<MemoryTypes.CRCW>(processors, memory);
            return machine;
        }
    }
}