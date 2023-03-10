using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using U3.Benchmark.Controller;
using U3.Benchmark.Data;
using U3.Benchmark.Strings;
using UnityEngine;

namespace U3.Benchmark
{
    public class BenchmarkRunner : MonoBehaviour
    {
        [SerializeField] private int runsCount = 100;

        private const int runCyclesCount = 40;

        private Benchmark[] benchmarkSerie;

        /// <summary>
        /// Runs cycle of benchmarks for the Benchamrk of benchmarkSerie[idx]
        /// Returns elapsed miliseconds and tics
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        private double[] RunSingleCycle(int idx)
        {
            Stopwatch watch = Stopwatch.StartNew();
            watch.Start();

            for (int i = 0; i < runCyclesCount; i++)
            {
                benchmarkSerie[idx].RunBenchmark();
            }

            watch.Stop();

            double[] cycleRes = new double[2];
            cycleRes[0] = watch.Elapsed.TotalMilliseconds;
            cycleRes[1] = watch.ElapsedTicks;

            return cycleRes;
        }

        /// <summary>
        /// Runs serie of benchmarks for the Benchamrk of benchmarkSerie[idx]
        /// Returns average result
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        private double[] GetBenchmarkResults(int idx)
        {
            double[] avgResult = new double[2];

            for (int i = 0; i < runsCount; i++)
            {
                double[] res = RunSingleCycle(idx);

                avgResult[0] += res[0];
                avgResult[1] += res[1];
            }

            avgResult[0] = avgResult[0] / runsCount;
            avgResult[1] = avgResult[1] / runsCount;

            return avgResult;
        }

        /// <summary>
        /// Runs all benchmarks from the benchmarkSerie
        /// Prints result as a Debug.Log() string message
        /// </summary>
        public void StartBenchmark()
        {
            Dictionary<string, double[]> results = new();
            StringBuilder resultsText = new();

            for (int i = 0; i < benchmarkSerie.Length; i++)
            {
                string benchName = benchmarkSerie[i].GetBenchmarkName();
                if (results.ContainsKey(benchName))
                    results.Add((benchName + i.ToString()), GetBenchmarkResults(i));
                else
                    results.Add(benchName, GetBenchmarkResults(i));
            }

            foreach (var result in results)
            {
                resultsText.AppendLine(
                    string.Format("Bench: {0} Elapsed: {1} Tics: {2}", result.Key, result.Value[0], result.Value[1]));
            }

            UnityEngine.Debug.Log(resultsText.ToString());
        }

        /// <summary>
        /// Creates benchmark serie
        /// </summary>
        private void Start()
        {
            Bench_MoveValue benchOne = new();
            Bench_MoveInstance benchTwo = new();

            benchmarkSerie = new Benchmark[] { benchTwo, benchOne, benchTwo, benchOne };

            foreach (Benchmark benchmark in benchmarkSerie)
            {
                benchmark.Initialize();
            }
        }
    }
}
