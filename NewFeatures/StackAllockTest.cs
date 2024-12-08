using BenchmarkDotNet.Attributes;
using System.Diagnostics;

namespace NewFeatures
{
    // [MemoryDiagnoser(displayGenColumns: true)]
    public class StackAllockTest
    {
        [Benchmark]
        public void StackAllocTest()
        {
            Stopwatch stopwatch = new();
            long mem = GC.GetTotalAllocatedBytes();
            stopwatch.Restart();
            Span<int> numbers = stackalloc[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            foreach (int number in numbers)
            {
                //Console.WriteLine(number);
            }
            stopwatch.Stop();
            mem = GC.GetTotalAllocatedBytes() - mem;
            Console.WriteLine($"total time elapsed:{stopwatch.Elapsed} and total memory used:{mem/1024/1024:N2}mb");
        }

        public void Something(Span<int> numbers)
        {
            foreach (int number in numbers)
            {
                //Console.WriteLine(number);
            }
        }

        private void Test(IEnumerable<int> numbers)
        {
            if (numbers.TryGetSpan(out ReadOnlySpan<int> span))
            {
                foreach (int number in span)
                {
                    //Console.WriteLine(number);
                }
            }
        }

        [Benchmark]
        public void HeapAllocTest()
        {
            Employee[] newEmployees =
     {
        new Employee { Name = "Charlie", Id = 3 },
        new Employee { Name = "Dana", Id = 4 }
    };

            //Memory<Employee> memoryEmployees = newEmployees.AsMemory();
            //AddEmployees(memoryEmployees, employeeList);
            Memory<int> numberss = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            foreach (var numver in numberss.ToArray().Chunk(3))
            {
                //numver.TakeLast()
                Console.WriteLine(string.Join(",", numver));
            }
            newEmployees.TryGetSpan<Employee>(out var span);

            //Unsafe.As<int>(newEmployees)

            Something(numberss.Span);
            //foreach (int number in numberss.Span) {
            //    //Console.WriteLine(number);
            //}
            //Console.WriteLine(result);
        }

        [Benchmark(Baseline = true)]
        public void NormalLoopTest()
        {
            var numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            foreach (int number in numbers)
            {
                //Console.WriteLine(number);
            }
        }

        public void AddEmployees(Memory<Employee> employees)
        {
            employees.ToArray().Chunk(3);
        }
    }

    public class Employee
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
