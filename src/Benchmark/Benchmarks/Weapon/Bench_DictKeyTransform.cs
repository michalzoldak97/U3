using System.Collections.Generic;
using UnityEngine;

namespace U3.Benchmark
{
    internal class Bench_DictKeyTransform : Benchmark
    {
        private const string benchName = "Transform";

        private readonly List<Transform> transforms = new(1000);
        private readonly Dictionary<Transform, GameObject> tDict = new();

        public override string GetBenchmarkName() => benchName;

        public override void Initialize()
        {
            for (int i = 0; i < 1000; i++)
            {
                GameObject obj = new GameObject($"Transform {i}");
                tDict.Add(obj.transform, obj);
                transforms.Add(obj.transform);
            }
        }

        private void EditObj(int idx)
        {
            GameObject obj = tDict[transforms[idx]];
            obj.name = $"{obj.name}{idx}";
        }

        public override void RunBenchmark()
        {
            int randIDX = Random.Range(0, 999);
            EditObj(randIDX);
        }
    }
}
