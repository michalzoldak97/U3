using System.Collections.Generic;
using UnityEngine;

namespace U3.Benchmark.Pool
{
    internal class Bench_ListValueTuple : Benchmark
    {
        private const int listLen = 1000;
        private const string benchName = "List ValueTuple";
        private List<(int, bool)> statesList;
        public override string GetBenchmarkName() => benchName;

        public override void Initialize()
        {
            statesList = new(listLen);

            for (int i = 0; i < listLen; i++)
            {
                bool state = Random.Range(0, 2) != 0;
                statesList.Add((i, state));
            }
        }

        public override void RunBenchmark()
        {
            int randomIDX = Random.Range(0, listLen);

            if (statesList[randomIDX].Item2)
                DoSth();
            else
                DoSthElse();
        }
    }
}
