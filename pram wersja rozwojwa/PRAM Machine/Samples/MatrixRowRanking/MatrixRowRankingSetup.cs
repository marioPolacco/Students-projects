/*  Wykonał Mariusz Witkowski
  
  */
using System;
using System.Collections.Generic;
using PRAM_Machine.ComplexDataTypes;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples.MatrixRowRanking
{
    public static class MatrixRowRankingSetup
    {
        public static IPRAMMachine Setup(int matrixSize, int randLimit = 8)
        {
            int row, column;
            var processors = new List<Processor>();

            for (row = 0; row < matrixSize; row++)
                for (column = 0; column < matrixSize; column++)
                    processors.Add(new MatrixRowRankingProcessor(row, column, matrixSize));

            var memory = new PRAM<MemoryTypes.CREW>();
            var zeroToNineGenerator = new Random();
            var matrixA = new Matrix<int>(matrixSize, matrixSize);
            var matrixB = new Matrix<int>(matrixSize, matrixSize);
            List<int> ranking = new List<int>(matrixSize);

            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    matrixA[i, j] = zeroToNineGenerator.Next(randLimit);
                    matrixB[i, j] = 0;
                }
                ranking.Add(0);
            }
            memory.AddNamedMemory("MatrixA", matrixA);
            memory.AddNamedMemory("MatrixB", matrixB);
            memory.AddNamedMemory("Ranking", ranking);

            var machine = new PRAMMachine<MemoryTypes.CREW>(processors, memory);
            return machine;
        }
    }
}