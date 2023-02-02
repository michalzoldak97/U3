using UnityEngine;

namespace U3.Benchmark.Pool
{
    public class PoolResponseClass
    {
        public bool isAvailable;
        public GameObject obj;
    }
    public class PoolClassArray
    {
        private readonly GameObject[] objArray;
        public PoolClassArray(int arrLen)
        {
            objArray = new GameObject[arrLen];

            for (int i = 0; i < arrLen; i++)
            {
                objArray[i] = new GameObject();
            }
        }

        public PoolResponseClass GetPoolObject(int idx)
        {
            PoolResponseClass res = new()
            {
                isAvailable = true,
                obj = objArray[idx]
            };

            return res;
        }
    }
    public class Bench_PoolClassArray : Benchmark
    {
        private const int arrLen = 1000;
        private const string benchName = "Class Array";

        private PoolClassArray classArray;

        public override string GetBenchmarkName()
        {
            return benchName;
        }

        public override void Initialize()
        {
            classArray = new(arrLen);
        }

        public override void RunBenchmark()
        {
            int randomIDX = Random.Range(0, arrLen);

            PoolResponseClass res = classArray.GetPoolObject(randomIDX);

            if (res.isAvailable)
                DoSth();
        }
    }
}
