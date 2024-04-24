using UnityEngine;

namespace U3.Benchmark.Pool
{
    public class Bench_GetPoolObjectByCopy : Benchmark
    {
        private int currentIndex;
        private const int arrLen = 100;
        private const string benchName = "COPY";

        private GameObject[] objectArray;

        public override string GetBenchmarkName() => benchName;

        public override void Initialize()
        {
            objectArray = new GameObject[arrLen];

            for (int i = 0; i < objectArray.Length; i++)
            {
                objectArray[i] = new("copy");
            }
        }

        public override void RunBenchmark()
        {
            int randomIDX = Random.Range(0, arrLen);
            currentIndex = randomIDX;

            GameObject res = objectArray[currentIndex];

            currentIndex++;

            res.SetActive(false);
            res.SetActive(true);

            if (res != null)
                DoSth();
        }
    }
}
