using UnityEngine;

namespace U3.Benchmark.Pool
{
    public static class StaticBenchPoolProvider
    {
        public static GameObjectArray ObjArray = new(1000);
    }
    public class Bench_StaticPoolProvider : Benchmark
    {
        private const string benchName = "STATIC";
        public override string GetBenchmarkName() => benchName;

        public override void Initialize()
        {
            
        }

        public override void RunBenchmark()
        {
            int randomIDX = Random.Range(0, 1000);

            GameObject res = StaticBenchPoolProvider.ObjArray.GetPooledObject(randomIDX);

            if (res != null)
                DoSth();
        }
    }
}
