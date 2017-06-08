﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples.VectorAddition {
    /// <summary>
    /// Class used to initialize sample of vector addition implementation.
    /// </summary>
    static class VectorAdditionSetup {
        /// <summary>
        /// This method prepares PRAM machine ready to start adding two vectors
        /// initialized with random numbers, ranging from 0 to limit. Prepares memory
        /// with two source vectors named 'a' and 'b' and result vector named 'c'.
        /// 
        /// This implementation uses VectorAddingProcessor from this namespace.
        /// </summary>
        /// <param name="count">Length of vectors that are going to be added.</param>
        /// <param name="limit">Maximum value, defaults to 10</param>
        /// <returns>Ready PRAM machine.</returns>
        public static IPRAMMachine Setup(int count, int limit = 10) {
            Random random = new Random();
            List<int> a = new List<int>();
            for (int i = 0; i < count; i++) {
                a.Add(random.Next(limit));
            }
            List<int> b = new List<int>();
            for (int i = 0; i < count; i++) {
                b.Add(random.Next(limit));
            }
            List<Processor> processors = new List<Processor>();
            for (int i = 0; i < count; i++) {
                processors.Add(new VectorAddingProcessor());
            }
            PRAM<MemoryTypes.EREW> memory = new PRAM<MemoryTypes.EREW>();
            memory.AddNamedMemory("a", a);
            memory.AddNamedMemory("b", b);
            memory.AddNamedMemory("c", count, 0);
            PRAMMachine<MemoryTypes.EREW> machine = new PRAMMachine<MemoryTypes.EREW>(processors, memory);
            return machine;
        }
    }
}
