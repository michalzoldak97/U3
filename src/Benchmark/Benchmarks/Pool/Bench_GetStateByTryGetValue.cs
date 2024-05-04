using System.Collections.Generic;
using System.Linq;

namespace U3.Benchmark.Pool
{
    internal class Bench_GetStateByTryGetValue : Benchmark
    {
        private const int listLen = 1000;
        private const string benchName = "HASH TABLES";
        private HashSet<int> availableIndexes;
        public override string GetBenchmarkName() => benchName;

        public override void Initialize()
        {
            availableIndexes = new(listLen);

            for (int i = 0; i < listLen / 2; i++)
            {
                availableIndexes.Add(i);
            }

        }

        private void Justify()
        {
            if (availableIndexes.Contains(5))
                DoSth();
            else
                DoSthElse();
        }

        private int GetAvailableIdx()
        {
            if (availableIndexes.Count < 1)
                return -1;

            int toReturn = availableIndexes.First();
            availableIndexes.Remove(toReturn);
            Justify();
            availableIndexes.Add(toReturn);

            return toReturn;
        }

        public override void RunBenchmark()
        {
            GetAvailableIdx();
        }
    }
}
