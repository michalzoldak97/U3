using UnityEngine;

namespace U3.Benchmark.Pool
{
    public struct PoolResponseStruct
    {
        public bool isAvailable;
        public GameObject obj;
    }
    public class PoolClassStruct
    {
        private readonly GameObject[] objArray;
        public PoolClassStruct(int arrLen)
        {
            objArray = new GameObject[arrLen];

            for (int i = 0; i < arrLen; i++)
            {
                objArray[i] = new GameObject();
            }
        }

        public PoolResponseStruct GetPoolObject(int idx)
        {
            PoolResponseStruct res;
            res.isAvailable = true;
            res.obj = objArray[idx];

            return res;
        }
    }
    public class Bench_PoolStructArray : Benchmark
    {
        private const int arrLen = 1000;
        private const string benchName = "Struct Array";

        private PoolClassStruct classStruct;
        public override string GetBenchmarkName() => benchName;

        public override void Initialize()
        {
            classStruct = new(arrLen);
        }

        public override void RunBenchmark()
        {
            int randomIDX = Random.Range(0, arrLen);

            PoolResponseStruct res = classStruct.GetPoolObject(randomIDX);
           
            if (res.isAvailable)
                DoSth();
        }
    }
}
