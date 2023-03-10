using UnityEngine;

namespace U3.Benchmark.Controller
{
    public class Bench_MoveValue : Benchmark
    {
        private const string benchName = "Move Value";

        private Vector3 CalculateMovDir()
        {
            Vector3 testVec = DoSth();

            Vector3 movDir = new();
            movDir.x = testVec.x;
            movDir.y = testVec.y;
            movDir.z = testVec.z;

            return movDir;
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
            Vector3 movDir = CalculateMovDir();
            movDir.x = movDir.z * movDir.y;
        }
    }
}
