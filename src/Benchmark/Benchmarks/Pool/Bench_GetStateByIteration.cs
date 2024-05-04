using System.Collections.Generic;

namespace U3.Benchmark.Pool
{
    internal class Bench_GetStateByIteration : Benchmark
    {
        private const int listLen = 1000;
        private const string benchName = "ITERATION";
        private List<bool> statesList;
        public override string GetBenchmarkName() => benchName;

        public override void Initialize()
        {
            statesList = new(listLen);

            for (int i = 0; i < listLen; i++)
            {
                statesList.Add(false);
            }

            int trueIdx = (int)(listLen / 2);
            statesList[trueIdx] = true;
        }

        private void Justify(bool j)
        {
            if (j)
                DoSth();
            else
                DoSthElse();
        }

        private int GetAvailableIdx()
        {
            for (int i = 0; i < statesList.Count; i++)
            {
                if (statesList[i])
                {
                    statesList[i] = false;
                    Justify(statesList[i]);
                    statesList[i] = true;
                    return i;
                }
            }

            return -1;
        }

        public override void RunBenchmark()
        {
            GetAvailableIdx();
        }
    }
}
