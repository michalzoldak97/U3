using UnityEngine;

namespace U3.Benchmark.Data
{
    public class Bench_AccessInstance : Benchmark
    {
        private const string benchName = "Access Instance";

        private Vector3 m_Pos;
        private float radius, height;
        public override string GetBenchmarkName() => benchName;

        public override void Initialize()
        {
            radius = .75f;
            height = 1.85f;
            m_Pos = new(234f, .52f, 5245f);
        }

        public override void RunBenchmark()
        {
            Physics.SphereCast(m_Pos, radius, -Vector3.up, out RaycastHit hitInfo,
                                   height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        }
    }
}
