using System.Linq;

namespace U3.Benchmark.Data
{
    public class Bench_GenerateSequenceEnum : Benchmark
    {
        private const string benchName = "Generate Enum";

        private int[] receiver;
        public override string GetBenchmarkName()
        {
            return benchName;
        }

        public override void Initialize()
        {
            
        }

        public override void RunBenchmark()
        {
            int[] seq = Enumerable.Range(0, 3).ToArray();
            int[] rest = Enumerable.Range(4, 7).ToArray();
            
            int[] result = seq.Concat(rest).ToArray();

            receiver = result;
        }
    }
}
