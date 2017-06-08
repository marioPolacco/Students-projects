﻿using System;
using PRAM_Machine.Gui;
using System.Windows;

namespace PRAM_Machine.Samples {
    class UsageSamples {
        [STAThread]
        static void Main() {
            //SimulatorGui.Run(FastAddition.FastAdditionSetup.Setup(10));
            // SimulatorGui.Run(VectorAddition.VectorAdditionSetup.Setup(15));
             //SimulatorGui.Run(FastAdditionDivideAndConquer.FastAdditionDivideAndConquerSetup.Setup(20));
            // SimulatorGui.Run(LogicalAnd.LogicalAndSetup.Setup(5));
            //   SimulatorGui.Run(Sorting.SortingSetup.Setup(4));
            //SimulatorGui.Run(ListRanking.ListRankingSetup.Setup(10));
            //SimulatorGui.Run(MatrixMultiplication.MatrixMultiplicationSetup.Setup(3));
            //SimulatorGui.Run(MatrixMultiplicationUsingMatrixDataType.MatrixMultiplicationUsingMatrixDataTypeSetup.Setup(2));
            //SimulatorGui.Run(Find3OnesInMatrix.Find3OnesInMatrixSetup.Setup(4));
            //SimulatorGui.Run(Find2ZerosInMatrixRow.Find2ZerosInMatrixRowSetup.Setup(4));
            
            SimulatorGui.Run(MatrixRowRanking.MatrixRowRankingSetup.Setup(8));
        }
    }
}
