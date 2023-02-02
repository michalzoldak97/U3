using System.Text;
using UnityEngine;

namespace U3.Benchmark.Strings
{
    [System.Serializable]
    public class Bench_StringBuilder : Benchmark
    {
        private const string benchName = "Building a string";
        private Vector3 dummy;
        public override string GetBenchmarkName()
        {
            return benchName;
        }

        public override void Initialize()
        {
            dummy = new Vector3(23.431f, .3f, 5492143443f);
        }

        public override void RunBenchmark()
        {
            StringBuilder testStr = new StringBuilder();
            testStr.AppendLine(benchName);
            testStr.Append(dummy.x.ToString());
            testStr.Append(dummy.y.ToString());
            testStr.Append(dummy.z.ToString());
        }
    }
}
