using UnityEngine;

namespace U3.Benchmark.Weapon
{
    internal enum SwitchModes
    {
        Mode1 = 1,
        Mode2 = 2,
        Mode3 = 3
    }
    public class Bench_SwitchMode : Benchmark
    {
        private const string benchName = "SWITCH";

        private Vector3 justifyVec = new();

        public override string GetBenchmarkName() => benchName;

        public override void Initialize()
        {
           
        }

        private void SwitchMode(SwitchModes mode)
        {
            switch (mode)
            {
                case SwitchModes.Mode1:
                    justifyVec.x += 1.0f;
                    break;
                case SwitchModes.Mode2:
                    justifyVec.y += 1.0f;
                    break;
                case SwitchModes.Mode3:
                    justifyVec.z += 1.0f;
                    break;
                default:
                    break;
            }
        }

        public override void RunBenchmark()
        {
            int randMode = Random.Range(1, 4);
            SwitchMode((SwitchModes)randMode);
        }
    }
}
