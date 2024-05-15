using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace U3.Benchmark.Weapon
{
    public class Bench_LayerCheckDict : Benchmark
    {
        private const string benchName = "DICT";

        private LayerMask layersToFind = new();
        private readonly string[] layersToFindNames = { "Player", "Item", "Enemy", "Default" };
        private GameObject obj;

        private readonly Dictionary<int, int> layerMasks = new();

        public override string GetBenchmarkName() => benchName;

        private IEnumerable GetAllLayerNumbers(LayerMask layerMask)
        {
            for (int i = 0; i < 32; i++)
            {
                if ((layerMask.value & (1 << i)) != 0)
                {
                    yield return i;
                }
            }
        }

        public override void Initialize()
        {
            layersToFind = LayerMask.GetMask("Player", "Item", "Enemy");
            obj = new GameObject();

            foreach (int layer in GetAllLayerNumbers(layersToFind))
            {
                layerMasks[layer] = 1 << layer;
            }
        }

        private void AssignRandomLayer()
        {
            int randIndex = Random.Range(0, 4);
            obj.layer = LayerMask.NameToLayer(layersToFindNames[randIndex]);
        }

        public override void RunBenchmark()
        {
            AssignRandomLayer();

            int objLayer = obj.layer;

            if (layerMasks.ContainsKey(objLayer) && (layersToFind.value & layerMasks[objLayer]) != 0)
                DoSth();
            else
                DoSthElse();
        }
    }
}
