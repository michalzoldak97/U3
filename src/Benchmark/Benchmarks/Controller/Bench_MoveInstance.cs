using UnityEngine;

namespace U3.Benchmark.Controller
{
    public class Bench_MoveInstance : Benchmark
    {
        private const string benchName = "Move Instance";

        private Vector3 movDir = Vector3.zero;

        private void CalculateMovDir()
        {
            Vector3 testVec = DoSth();
            movDir.x = testVec.x;
            movDir.y = testVec.y;
            movDir.z = testVec.z;
        }
        public override string GetBenchmarkName()
        {
            return benchName;
        }

        public override void Initialize()
        {
            
        }

        public override void RunBenchmark()
        {
            CalculateMovDir();
            movDir.x = movDir.z * movDir.y;
        }
    }
}
