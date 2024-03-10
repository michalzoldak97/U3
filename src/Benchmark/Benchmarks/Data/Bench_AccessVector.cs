using UnityEngine;

namespace U3.Benchmark.Data
{
    public class Bench_AccessVector : Benchmark
    {
        private const string benchName = "Access Vector";

        private Vector3 testVec;
        public override string GetBenchmarkName() => benchName;

        public override void Initialize()
        {
            testVec = new Vector3(32.5f, -652.452f, 5553164f);
        }

        public override void RunBenchmark()
        {
            Vector3 tempVec = DoSth();
            testVec.x += tempVec.x;
            testVec.y += tempVec.y;
            testVec.z += tempVec.z;
        }
    }
}
