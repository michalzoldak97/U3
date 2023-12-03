using UnityEngine;

namespace U3.Benchmark.Strings
{
    public class Bench_StringAddition : Benchmark
    {
        private const string benchName = "Adding a strings";
        private string testStr;
        private Vector3 dummy;
        public override string GetBenchmarkName() => benchName;

        public override void Initialize()
        {
            dummy = new Vector3(23.431f, .3f, 5492143443f);
        }

        public override void RunBenchmark()
        {
            testStr = "";
            testStr += string.Format("{0}\n", benchName);
            testStr += dummy.x.ToString();
            testStr += dummy.y.ToString();
            testStr += dummy.z.ToString();
        }
    }
}
