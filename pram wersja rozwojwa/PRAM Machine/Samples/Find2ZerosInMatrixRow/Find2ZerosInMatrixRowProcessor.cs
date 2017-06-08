using System;
using System.Collections.Generic;
using PRAM_Machine.ComplexDataTypes;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples.Find2ZerosInMatrixRow{
    internal class Find2ZerosInMatrixRowProcessor : Processor{
     private readonly int _column;
     private readonly int _row;
     private readonly int _matrixSize;
        public Find2ZerosInMatrixRowProcessor(int row, int column, int matrixSize){
            _column = column;
            _row = row;
            _matrixSize = matrixSize;
            
            DataToRead = new MemoryAddress("MatrixA", _row, _column);            
        }
        public override dynamic Run(dynamic data){            
            if (TickCount == 0)
            {
                if (data == 0) {
                    if (_column < _matrixSize - 1){
                        DataToRead = new MemoryAddress("MatrixA", _row, _column + 1);
                    }else
                        Stop();
                }else
                    Stop();
            }
            if (TickCount == 1){ 
                if (data == 0)                   
                    DataToWrite = new MemoryAddress("MatrixB", _row);
                    return true;
                }

            if (TickCount == 2){
                DataToRead = new MemoryAddress("MatrixB", _row);
            }
            if (TickCount == 3){
                if (data == false){
                    DataToWrite = new MemoryAddress("Result", 0);
                    Stop();                    
                    return false;
                }
                else
                    Stop();
            }
            return null;
          }   
     }
}
