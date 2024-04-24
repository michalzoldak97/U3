using UnityEngine;

namespace U3.Benchmark.Pool
{
    public class Bench_GetPoolObjectByInt : Benchmark
    {
        private int currentIndex;
        private const int arrLen = 100;
        private const string benchName = "INDEX";

        private GameObject[] objectArray;

        public override string GetBenchmarkName() => benchName;

        public override void Initialize()
        {
            objectArray = new GameObject[arrLen];

            for (int i = 0; i < objectArray.Length; i++)
            {
                objectArray[i] = new("index");
            }
        }

        public override void RunBenchmark()
        {
            int randomIDX = Random.Range(0, arrLen);
            currentIndex = randomIDX;

            int currIdx = currentIndex;
            currentIndex++;

            objectArray[currIdx].SetActive(false);
            objectArray[currIdx].SetActive(true);

            if (objectArray[currIdx] != null)
                DoSth();
        }
    }
}
