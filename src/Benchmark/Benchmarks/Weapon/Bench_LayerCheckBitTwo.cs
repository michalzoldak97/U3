using UnityEngine;

namespace U3.Benchmark.Weapon
{
    public class Bench_LayerCheckBitTwo : Benchmark
    {
        private const string benchName = "BIT &";

        private LayerMask layersToFind = new();
        private readonly string[] layersToFindNames = { "Player", "Item", "Enemy", "Default" };
        private GameObject obj;

        public override string GetBenchmarkName() => benchName;

        public override void Initialize()
        {
            layersToFind = LayerMask.GetMask("Player", "Item", "Enemy");
            obj = new GameObject();
        }

        private void AssignRandomLayer()
        {
            int randIndex = Random.Range(0, 4);
            obj.layer = LayerMask.NameToLayer(layersToFindNames[randIndex]);
        }

        public override void RunBenchmark()
        {
            AssignRandomLayer();
            if ((layersToFind.value & (1 << obj.layer)) != 0)
                DoSth();
            else
                DoSthElse();
        }
    }
}
