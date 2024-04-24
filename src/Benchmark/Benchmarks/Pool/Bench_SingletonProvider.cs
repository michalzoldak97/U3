using UnityEngine;

namespace U3.Benchmark.Pool
{
    public class BenchPoolSingleton : MonoBehaviour
    {
        public static BenchPoolSingleton Instance;

        private GameObjectArray gameObjectArray;

        private void Awake()
        {
            gameObjectArray = new GameObjectArray(1000);

            if (Instance == null)
                Instance = this;
        }

        public GameObject GetObjFromArray(int idx)
        {
            return gameObjectArray.GetPooledObject(idx);
        }
    }
    public class Bench_SingletonProvider : Benchmark
    {
        private const string benchName = "SINGLETON";
        public override string GetBenchmarkName() => benchName;

        public override void Initialize()
        {
            GameObject singletonKeeper = new();
            singletonKeeper.AddComponent<BenchPoolSingleton>();
        }

        public override void RunBenchmark()
        {
            int randomIDX = Random.Range(0, 1000);

            GameObject res = BenchPoolSingleton.Instance.GetObjFromArray(randomIDX);

            if (res != null)
                DoSth();
        }
    }
}
