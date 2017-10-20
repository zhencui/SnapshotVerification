using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnapshotTest
{
    public class ValueBiggerThan500Exception : Exception
    {
        public ValueBiggerThan500Exception(long val) : base($"Value {val} is bigger than 500")
        { }
    }

    public static class CPUIntensiveComputation
    {
        public static int MinimumMemoryUsageInMB
        {
            get => _minimumMemoryUsageInMB;
            set
            {
                if (value > 2000)
                {
                    throw new ArgumentOutOfRangeException(nameof(MinimumMemoryUsageInMB), value, "Cannot be greater than 2000");
                }

            }
        }

        public static long RecusiveCall1(int depth)
        {
            AccessMemoryHolder();
            if (depth < 0)
            {
                return FindNthPrimeNumber(55);
            }
            if (depth % 2 == 1)
            {
                FindNthPrimeNumber(55);
            }
            SortList(1000);
            if (depth % 3 == 1)
            {
                return RecusiveCall2(depth - 1);
            }
            else
            {
                return RecusiveCall1(depth - 1);
            }
        }

        public static long RecusiveCall2(int depth)
        {
            AccessMemoryHolder();
            if (depth < 0)
            {
                return 0;
            }
            if (depth % 2 == 1)
            {
                // both 'randomNumber' and 'primeNumber' should show up in snapshot view as local variables.
                int randomNumber = _rand.Next() % 300;
                long primeNumber = FindNthPrimeNumber(randomNumber);
                if (primeNumber > 500)
                {
                    throw new ValueBiggerThan500Exception(primeNumber);
                }
                randomNumber++;
                return primeNumber + randomNumber;
            }
            SortList(1000);
            return RecusiveCall1(depth - 1);
        }

        public static long FindNthPrimeNumber(int n)
        {
            int count = 0;
            long a = 2;
            while (count < n)
            {
                long b = 2;
                int prime = 1;// to check if found a prime
                while (b * b <= a)
                {
                    if (a % b == 0)
                    {
                        prime = 0;
                        break;
                    }
                    b++;
                }
                if (prime > 0)
                    count++;
                a++;
            }
            return (--a);
        }

        public static void SortList(int listSize)
        {
            int i = 0;
            List<int> list = new List<int>(listSize + 1);
            for (i = listSize; i > 0; i--)
            {
                if (i % 10 == 0)
                {
                    list.Add(_rand.Next() % 5000);
                }
                else
                {
                    list.Add(i);
                }
            }
            list.Sort();
            list.Clear();
        }

        private static void SetMemoryHolder(int sizeInMB)
        {
            _minimumMemoryUsageInMB = sizeInMB;
            int capacity = _minimumMemoryUsageInMB * 1024 * 1024;
            _memoryHolder = new List<byte>(capacity);
            _memoryHolder.AddRange(Enumerable.Repeat((byte)0x20, capacity));
        }

        private static void AccessMemoryHolder(int accessCount = 50)
        {
            for (int i = 0; i < accessCount; i++)
            {
                var part1 = (_rand.Next() % _minimumMemoryUsageInMB) * 1024 * 1024;
                var idx = part1 + (_rand.Next() % 1024 * 1024);
                _memoryHolder[idx] = (byte)(_rand.Next() % 256);
            }
        }

        static CPUIntensiveComputation()
        {
            SetMemoryHolder(_minimumMemoryUsageInMB);
        }

        private static int _minimumMemoryUsageInMB = 120;

        private static Random _rand = new Random();
        private static List<byte> _memoryHolder;
    }
}
