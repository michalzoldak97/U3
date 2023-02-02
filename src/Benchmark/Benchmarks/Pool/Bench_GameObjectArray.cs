using UnityEngine;

namespace U3.Benchmark.Pool
{
    public class GameObjectArray
    {
        private readonly GameObject[] objArray;
        public GameObjectArray(int arrLen)
        {
            objArray = new GameObject[arrLen];

            for (int i = 0; i < arrLen; i++)
            {
                objArray[i] = new GameObject();
            }
        }

        public GameObject GetPooledObject(int idx)
        {
            return objArray[idx];
        }
    }
    public class Bench_GameObjectArray : Benchmark
    {
        private const int arrLen = 1000;
        private const string benchName = "Game Object Array";

        private GameObjectArray objectArray;

        public override string GetBenchmarkName()
        {
            return benchName;
        }

        public override void Initialize()
        {
            objectArray = new(arrLen);
        }

        public override void RunBenchmark()
        {
            int randomIDX = Random.Range(0, arrLen);

            GameObject res = objectArray.GetPooledObject(randomIDX);

            if (res != null)
                DoSth();
        }
    }
}
