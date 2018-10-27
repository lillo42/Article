using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using static System.Console;

namespace Benchmark
{
    public class Program
    {
        const int LENGTH = 10_000_000;
        private static readonly int[] array1 = Enumerable.Range(0, LENGTH).ToArray();
        private static readonly int[] array2 = Enumerable.Range(0, LENGTH).ToArray();

        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Program>();
            ReadLine();
        }

        [Benchmark]
        public void Sum2ArraySIMD()
        {
            var result = new int[array1.Length];
            int count = 0;
            if (Vector.IsHardwareAccelerated)
            {
                int step = Vector<int>.Count;
                Vector<int> vector = Vector<int>.Zero;
                for (; count <= result.Length - step; count += step)
                {
                    vector = new Vector<int>(array1, count) + new Vector<int>(array2, count);
                    vector.CopyTo(result, count);
                }
            }

            for (; count < array1.Length; count++)
            {
                result[count] = array1[count] + array2[count];
            }
        }

        [Benchmark]
        public void Sum2ArraysNoSIMD()
        {
            var result = new int[array1.Length];
            for (int count = 0; count < array1.Length; count++)
            {
                result[count] = array1[count] + array2[count];
            }
        }

        [Benchmark]
        public void MinAndMaxSIMD()
        {
            int min = int.MaxValue, max = int.MinValue;
            var vmin = new Vector<int>(int.MaxValue);
            var vmax = new Vector<int>(int.MinValue);
            int count = 0;
            if (Vector.IsHardwareAccelerated)
            {
                int step = Vector<int>.Count;
                Vector<int> vector = Vector<int>.Zero;
                for (; count <= array1.Length - step; count += step)
                {
                    vector = new Vector<int>(array1, count);
                    vmin = Vector.Min(vmin, vector);
                    vmax = Vector.Max(vmax, vector);
                }

                for (var j = 0; j < step; ++j)
                {
                    min = Math.Min(min, vmin[j]);
                    max = Math.Max(max, vmax[j]);
                }
            }

            // Process any remaining elements
            for (; count < array1.Length; count++)
            {
                min = Math.Min(min, array1[count]);
                max = Math.Max(max, array1[count]);
            }
        }

        [Benchmark]
        public void NonSIMDMinMax()
        {
            int min = int.MaxValue;
            int max = int.MinValue;
            for (int i = 0; i < array1.Length; i++)
            {
                min = Math.Min(min, array1[i]);
                max = Math.Max(max, array1[i]);
            }
        }

        [Benchmark]
        public void SumSIMD()
        {
            int count = 0;
            int result = 0;
            if (Vector.IsHardwareAccelerated)
            {
                int step = Vector<int>.Count;
                var vector = new Vector<int>(array1, 0);
                for (; count <= array1.Length - step; count += step)
                    vector += new Vector<int>(array1, count);

                var final = new int[step];
                vector.CopyTo(final);

                for (int i = 0; i < final.Length; i++)
                    result += final[i];
            }
            for (; count < array1.Length; count++)
                result += array1[count];
        }

        [Benchmark]
        public void SumNoSIMD()
        {
            int result = 0;
            for (int i = 0; i < array1.Length; i++)
                result += array1[i];
        }

    }
}
