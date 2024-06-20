using UnityEngine;

namespace U3.Benchmark.Camera
{
    internal class Bench_WorldToScreen : Benchmark
    {
        private const string benchName = "SCREEN";
        private UnityEngine.Camera m_Camera;

        public override string GetBenchmarkName() => benchName;

        public override void Initialize()
        {
            m_Camera = UnityEngine.Camera.main;
        }

        public override void RunBenchmark()
        {
            Vector3 randPos = new(Random.Range(0, 100), Random.Range(0, 100), Random.Range(0, 100));
            Vector3 viewPoint = m_Camera.WorldToScreenPoint(randPos);
            if ((viewPoint.x > 0) || (viewPoint.x < Screen.width) ||
                (viewPoint.y > 0) || (viewPoint.y < Screen.height))
                DoSth();
        }
    }
}
