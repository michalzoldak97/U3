using UnityEngine;


namespace U3.Benchmark.Data
{
    public class MockController
    {
        public float radius { get; set; }
        public float height { get; set; }
    }
    public class Bemch_AccessProperty : Benchmark
    {
        private const string benchName = "Access Property";

        private Vector3 m_Pos;
        private MockController m_CharacterController;
        public override string GetBenchmarkName()
        {
            return benchName;
        }

        public override void Initialize()
        {
            m_Pos = new(234f, .52f, 5245f);

            m_CharacterController = new()
            {
                radius = .75f,
                height = 1.85f
            };
        }

        public override void RunBenchmark()
        {
            Physics.SphereCast(m_Pos, m_CharacterController.radius, -Vector3.up, out RaycastHit hitInfo,
                                   m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        }
    }
}
