using System;
using System.Collections.Generic;
using PRAM_Machine.ComplexDataTypes;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples.MatrixRowRanking
{
    internal class MatrixRowRankingProcessor : Processor
    {
        private readonly int _column;
        private readonly int _row;
        private readonly int _matrixSize;
        private int sum;
        private float columnsAdded;
        bool isAdditionPhase;
        bool isRankingSortingPhase;
        private string matrixSummed;
        private int rowSum;
        private bool isAdditionPhaseB;
        private bool isEndingPhase;
        private int Counter;

        public MatrixRowRankingProcessor(int row, int column, int matrixSize)
        {
            _column = column;
            _row = row;
            _matrixSize = matrixSize;
            columnsAdded = matrixSize/2;
            sum = 0;
            isAdditionPhase = true;
            isAdditionPhaseB = false;
            isRankingSortingPhase = false;     
            isEndingPhase = false;
            matrixSummed = "MatrixA";
            Counter = 0;

            if (_column < columnsAdded)
                DataToRead = new MemoryAddress(matrixSummed, _row, 2 * _column);
        }

        public override dynamic Run(dynamic data)
        {         

            if (2 * Math.Log(_matrixSize, 2) == TickCount)
            {
                isAdditionPhase = false;
                isRankingSortingPhase = true;
            }

            if(2 * Math.Log(_matrixSize, 2) + 3 == TickCount)
            {
                matrixSummed = "MatrixB";
                //Counter += 1;
                isAdditionPhase = true;
            }

            if (matrixSummed == "MatrixB")
            {
                Counter = TickCount + 1;
            }
            else
                Counter = TickCount;


            if (isAdditionPhase)
            {
                if (_column < columnsAdded)
                {
                    if (Counter % 2 == 0)
                        sum = 0;
                    sum += data;
                }

                if (_column < columnsAdded)
                {
                    if (Counter % 2 == 0)
                    {
                        DataToRead = new MemoryAddress(matrixSummed, _row, (2 * _column + 1));
                        DataToWrite = new MemoryAddress();
                    }
                    else
                    {
                        columnsAdded /= 2;
                        if (columnsAdded < 1)
                        {
                            isAdditionPhase = false;
                            if (matrixSummed == "MatrixB")
                            {
                                DataToRead = new MemoryAddress(matrixSummed, _row, 0);
                                isEndingPhase = true;
                            }

                        }
                        else
                            DataToRead = new MemoryAddress(matrixSummed, _row, (2 * _column));

                        DataToWrite = new MemoryAddress(matrixSummed, _row, _column);
                    }
                    return sum;
                }
                else
                {
                    if (matrixSummed == "MatrixB")
                    {
                        DataToWrite = new MemoryAddress();
                        Stop();
                    }
                    return sum;
                }
            }

            if (isRankingSortingPhase)
            {
                if (2 * Math.Log(_matrixSize, 2) == TickCount)
                {
                    DataToWrite = new MemoryAddress();
                    DataToRead = new MemoryAddress("MatrixA", _row, 0);
                    return null;
                }         
                 
                if (2 * Math.Log(_matrixSize, 2) + 1 == TickCount)
                {
                    rowSum = data;
                    DataToRead = new MemoryAddress("MatrixA", _column, 0);
                    DataToWrite = new MemoryAddress();
                    return null;
                }

                if (2 * Math.Log(_matrixSize, 2) + 2 == TickCount)
                {
                    DataToWrite = new MemoryAddress("MatrixB", _row, _column);        
                    isAdditionPhaseB = true;
                    isRankingSortingPhase = false;
                    columnsAdded = _matrixSize / 2;
                    sum = 0;
                    if (_column < columnsAdded)
                        DataToRead = new MemoryAddress("MatrixB", _row, 2 * _column);
 
                    if (rowSum < data || (rowSum == data && _row > _column))
                        return 1;
                    else
                        return 0;
                }
            }
       
            if (isEndingPhase)
            {
                DataToWrite = new MemoryAddress("Ranking", _row);
                Stop();
                return (data + 1);
            }
            return sum;
        }
    }
}

