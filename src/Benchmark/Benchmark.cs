using UnityEngine;
namespace U3.Benchmark
{
    public abstract class Benchmark
    {
        /// <summary>
        /// Method can be used as a dummy "do anything" operation
        /// </summary>
        /// <returns></returns>
        protected Vector3 DoSth()
        {
            Vector3 res = new(.432f, .52351245f, 425235124f);

            res.x = Mathf.Sqrt(res.x);
            res.y = Mathf.Sqrt(res.y);
            res.z = Mathf.Sqrt(res.z);

            return res;
        }

        protected Vector3 DoSthElse()
        {
            Vector3 res = new(.678f, 1245f, 5464768.4f);

            res.x = Mathf.Sqrt(res.x);
            res.y = Mathf.Sqrt(res.y);
            res.z = Mathf.Sqrt(res.z);

            return res;
        }

        /// <summary>
        /// Benchmark name to show in the results
        /// </summary>
        /// <returns></returns>
        public abstract string GetBenchmarkName();

        /// <summary>
        /// Initialize any class data
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Runs evaluated code once
        /// </summary>
        public abstract void RunBenchmark();
    }
}
