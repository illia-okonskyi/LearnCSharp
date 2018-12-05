using System;
using System.Diagnostics;

namespace HelloSharp
{
    class ArrayCopyTestCodeSample
    {
        const int ARRAY_SIZE_N = 1000;
        const int ARRAY_SIZE_M = 1000;

        static void InitializeArray(Random r, int[,] arr)
        {
            for (int i = 0; i < ARRAY_SIZE_N; ++i)
                for (int j = 0; j < ARRAY_SIZE_M; ++j)
                    arr[i, j] = r.Next();
        }

        static void EnshureArrayEquality(string testName, int[,] arr1, int[,] arr2)
        {
            for (int i = 0; i < ARRAY_SIZE_N; ++i)
                for (int j = 0; j < ARRAY_SIZE_M; ++j)
                    if (arr2[i, j] != arr1[i, j])
                        throw new Exception(testName);
        }

        static void TestArrayCopy_Loop(Random r)
        {
            int[,] arr1 = new int[ARRAY_SIZE_N, ARRAY_SIZE_M];
            int[,] arr2 = new int[ARRAY_SIZE_N, ARRAY_SIZE_M];
            InitializeArray(r, arr1);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < ARRAY_SIZE_N; ++i)
                for (int j = 0; j < ARRAY_SIZE_M; ++j)
                    arr2[i, j] = arr1[i, j];

            sw.Stop();
            Console.WriteLine("TestArrayCopy_Loop: {0}", sw.Elapsed);
        }

        static void TestArrayCopy_UnsafeLoop(Random r)
        {
            int[,] arr1 = new int[ARRAY_SIZE_N, ARRAY_SIZE_M];
            int[,] arr2 = new int[ARRAY_SIZE_N, ARRAY_SIZE_M];
            InitializeArray(r, arr1);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            unsafe
            {
                fixed (int* pArr1 = arr1)
                fixed (int* pArr2 = arr2)
                {
                    for (int i = 0; i < ARRAY_SIZE_N; ++i)
                    {
                        int baseIndex = i * ARRAY_SIZE_N;
                        for (int j = 0; j < ARRAY_SIZE_M; ++j)
                        {
                            int index = baseIndex + j;
                            pArr2[index] = pArr1[index];
                        }
                    }
                }
            }

            sw.Stop();
            Console.WriteLine("TestArrayCopy_UnsafeLoop: {0}", sw.Elapsed);
            EnshureArrayEquality("TestArrayCopy_UnsafeLoop", arr1, arr2);
        }

        static void TestArrayCopy_ArrayCopy(Random r)
        {
            int[,] arr1 = new int[ARRAY_SIZE_N, ARRAY_SIZE_M];
            int[,] arr2 = new int[ARRAY_SIZE_N, ARRAY_SIZE_M];
            InitializeArray(r, arr1);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Array.Copy(arr1, arr2, ARRAY_SIZE_N * ARRAY_SIZE_M);

            sw.Stop();
            Console.WriteLine("TestArrayCopy_ArrayCopy: {0}", sw.Elapsed);
            EnshureArrayEquality("TestArrayCopy_ArrayCopy", arr1, arr2);
        }

        static void TestArrayCopy_BlockCopy(Random r)
        {
            int[,] arr1 = new int[ARRAY_SIZE_N, ARRAY_SIZE_M];
            int[,] arr2 = new int[ARRAY_SIZE_N, ARRAY_SIZE_M];
            InitializeArray(r, arr1);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Buffer.BlockCopy(arr1, 0, arr2, 0, ARRAY_SIZE_N * ARRAY_SIZE_M * sizeof(int));

            sw.Stop();
            Console.WriteLine("TestArrayCopy_BlockCopy: {0}", sw.Elapsed);
            EnshureArrayEquality("TestArrayCopy_BlockCopy", arr1, arr2);
        }

        static void TestArrayCopy_Clone(Random r)
        {
            int[,] arr1 = new int[ARRAY_SIZE_N, ARRAY_SIZE_M];
            InitializeArray(r, arr1);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            int[,] arr2 = (int[,]) arr1.Clone();

            sw.Stop();
            Console.WriteLine("TestArrayCopy_Clone: {0}", sw.Elapsed);
            EnshureArrayEquality("TestArrayCopy_Clone", arr1, arr2);
        }


        public static void Run()
        {
            var r = new Random(unchecked((int) DateTime.Now.ToBinary()));

            TestArrayCopy_Loop(r);
            GC.Collect();
            TestArrayCopy_UnsafeLoop(r);
            GC.Collect();
            TestArrayCopy_ArrayCopy(r);
            GC.Collect();
            TestArrayCopy_BlockCopy(r);
            GC.Collect();
            TestArrayCopy_Clone(r);
            GC.Collect();
        }
    }
}
