using UnityEngine;

namespace U3.Benchmark.Pool
{
    public class Bench_CashedSingletonProvider : Benchmark
    {
        private const string benchName = "CASCHED";
        public override string GetBenchmarkName() => benchName;

        private BenchPoolSingleton singleton;

        public override void Initialize()
        {
            singleton = BenchPoolSingleton.Instance;
        }

        public override void RunBenchmark()
        {
            int randomIDX = Random.Range(0, 1000);

            GameObject res = singleton.GetObjFromArray(randomIDX);

            if (res != null)
                DoSth();
        }
    }
}
