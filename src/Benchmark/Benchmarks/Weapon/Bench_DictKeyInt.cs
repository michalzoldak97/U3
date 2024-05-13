using System.Collections.Generic;
using UnityEngine;

namespace U3.Benchmark
{
    internal class Bench_DictKeyInt : Benchmark
    {
        private const string benchName = "Int";

        private readonly List<Transform> transforms = new(1000);
        private readonly Dictionary<int, GameObject> tDict = new();

        public override string GetBenchmarkName() => benchName;

        public override void Initialize()
        {
            for (int i = 0; i < 1000; i++)
            {
                GameObject obj = new GameObject($"Transform {i}");
                tDict.Add(obj.GetInstanceID(), obj);
                transforms.Add(obj.transform);
            }
        }

        private void EditObj(int idx)
        {
            GameObject obj = tDict[transforms[idx].gameObject.GetInstanceID()];
            obj.name = $"{obj.name}{idx}";
        }

        public override void RunBenchmark()
        {
            int randIDX = Random.Range(0, 999);
            EditObj(randIDX);
        }
    }
}
