using System.Linq;
using System.Numerics;
using static System.Console;

namespace Simple
{
    class Program
    {
        const int LENGTH = 100;
        static void Main(string[] args)
        {
            var array = Sum(Enumerable.Range(0, LENGTH).ToArray(), Enumerable.Range(0, LENGTH).ToArray());
            WriteArray(array);
            ReadLine();
        }

        private static int[] Sum(int[] array1, int[] array2)
        {
            var result = new int[array1.Length];
            int count = 0;
            if (Vector.IsHardwareAccelerated)
            {
                int step = Vector<int>.Count;
                Vector<int> vector = Vector<int>.Zero;
                for (; count <= result.Length - step; count += step)
                {
                    WriteLine($"Using SIMD Count {count} - Step:{step}");
                    vector = new Vector<int>(array1, count) + new Vector<int>(array2, count);
                    vector.CopyTo(result, count);
                }
            }

            for (; count < array1.Length; count++)
            {
                WriteLine($"No SIMD Count:{count}");
                result[count] = array1[count] + array2[count];
            }

            return result;
        }

        private static void WriteArray<T>(T[] source)
        {
            WriteLine("Write Array:");
            for (int i = 0; i < source.Length; i++)
            {
                Write($"\t {i}:{source[i]}");
                if ((i + 1) % 4 == 0)
                    WriteLine();
            }
            WriteLine();
        }
    }
}
