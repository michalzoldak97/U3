using UnityEngine;

namespace U3.Benchmark.Data
{
    public class Bench_AccessArray : Benchmark
    {
        private const string benchName = "Access Array";

        private float[] testArr;
        public override string GetBenchmarkName()
        {
            return benchName;
        }

        public override void Initialize()
        {
            testArr = new float[] { 32.5f, -652.452f, 5553164f };
        }

        public override void RunBenchmark()
        {
            Vector3 tempVec = DoSth();
            testArr[0] += tempVec.x;
            testArr[1] += tempVec.y;
            testArr[2] += tempVec.z;
        }
    }
}