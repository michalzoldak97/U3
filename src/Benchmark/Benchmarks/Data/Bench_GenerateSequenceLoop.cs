using System;
using UnityEngine;

namespace U3.Benchmark.Data
{
    public class Bench_GenerateSequenceLoop : Benchmark
    {
        private const string benchName = "Generate Loop";

        private int[] receiver;
        public override string GetBenchmarkName()
        {
            return benchName;
        }

        public override void Initialize()
        {

        }

        public override void RunBenchmark()
        {
            int[] seq = new int[4];

            for (int i = 0; i < seq.Length; i++)
            {
                seq[i] = i;
            }

            int[] rest = new int[4];

            for (int i = 0; i < rest.Length; i++)
            {
                rest[i] = i + 4;
            }

            int[] result = new int[(seq.Length + rest.Length)];

            for (int i = 0; i < seq.Length; i++)
            {
                result[i] = seq[i];
            }

            int nextIdx = seq.Length;
            for (int i = 0; i < rest.Length; i++)
            {
                result[i + nextIdx] = rest[i];
            }

            receiver = result;
        }
    }
}