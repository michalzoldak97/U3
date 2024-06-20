using System.Collections.Generic;
using UnityEngine;

namespace U3.Benchmark.Pool
{
    internal struct PoolObjState
    {
        public int Index;
        public bool IsAvailable;

        public PoolObjState(int idx, bool stat)
        {
            Index = idx;
            IsAvailable = stat;
        }
    }

    internal class Bench_ListStruct : Benchmark
    {
        private const int listLen = 1000;
        private const string benchName = "List State Struct";
        private List<PoolObjState> statesList;
        public override string GetBenchmarkName() => benchName;

        public override void Initialize()
        {
            statesList = new(listLen);

            for (int i = 0; i < listLen; i++)
            {
                bool state = Random.Range(0, 2) != 0;
                statesList.Add(new PoolObjState(i, state));
            }
        }

        public override void RunBenchmark()
        {
            int randomIDX = Random.Range(0, listLen);

            if (statesList[randomIDX].IsAvailable)
                DoSth();
            else
                DoSthElse();
        }
    }
}
