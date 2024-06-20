using UnityEngine;

namespace U3.Benchmark.Camera
{
    internal class Bench_WorldToView : Benchmark
    {
        private const string benchName = "VIEWPORT";
        private UnityEngine.Camera m_Camera;

        public override string GetBenchmarkName() => benchName;

        public override void Initialize()
        {
            m_Camera = UnityEngine.Camera.main;
        }

        public override void RunBenchmark()
        {
            Vector3 randPos = new(Random.Range(0, 100), Random.Range(0, 100), Random.Range(0, 100));
            Vector3 viewPoint = m_Camera.WorldToViewportPoint(randPos);
            if (viewPoint.x > 0 && viewPoint.x < 1 && viewPoint.y > 0 && viewPoint.y < 1 && viewPoint.z > 0)
                DoSth();
        }
    }
}
