using UnityEngine;

namespace U3.Benchmark.Weapon
{
    public class Bench_IfMode : Benchmark
    {
        private const string benchName = "IF";

        private Vector3 justifyVec = new();

        public override string GetBenchmarkName() => benchName;

        public override void Initialize()
        {

        }

        private void SwitchMode(SwitchModes mode)
        {
            if (mode == SwitchModes.Mode1)
                justifyVec.x += 1.0f;
            else if (mode == SwitchModes.Mode2)
                justifyVec.y += 1.0f;
            else if (mode == SwitchModes.Mode3)
                justifyVec.z += 1.0f;
        }

        public override void RunBenchmark()
        {
            int randMode = Random.Range(1, 4);
            SwitchMode((SwitchModes)randMode);
        }
    }
}
