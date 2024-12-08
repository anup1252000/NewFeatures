//Span<int> numbers = stackalloc[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
//foreach (int number in numbers)
//{
//    Console.WriteLine(number);
//}

//Memory<int> numberss = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

//EmployeeStruct[] employees =
//[
//    new EmployeeStruct("John", 25, "HR"),
//    new EmployeeStruct("Jane", 30, "IT"),
//    new EmployeeStruct("Jack", 35, "Finance")
//];

//foreach (var employee in employees)
//{
//    Console.WriteLine($"{employee.Name}, {employee.Age}, {employee.Department}");
//}
using NewFeatures;
using BenchmarkDotNet.Running;
using System.Diagnostics;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

Stopwatch stopwatch = new();
GC.Collect();
GC.WaitForPendingFinalizers();
GC.Collect(); // Ensure garbage collection to get a clean slate for memory measurement.

long initialMemory = GC.GetTotalAllocatedBytes();
stopwatch.Start();

//StringBuilder a = new("hello world"); // Use StringBuilder for efficient concatenation
string a = "hello world";
var newEmployees1 = new List<Employee>
        {
            new Employee { Name = "Charlie", Id = 3 },
            new Employee { Name = "Dana", Id = 4 }
        };

Span<int> numbers = stackalloc[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
foreach (int number in numbers)
{
    a+=number;
    newEmployees1.Add(new Employee { Id = number,Name=number.ToString() });
}
StringBuilder b = new();

for (int i = 0; i < 10000; i++)
{
    try
    {
         a += i;
        b.Append(i);
    }
    catch { }
}
//}
//catch (Exception ex)
//{

//    throw;
//}


stopwatch.Stop();
long memoryUsed = GC.GetTotalAllocatedBytes() - initialMemory;

Console.WriteLine($"Total time elapsed: {stopwatch.ElapsedMilliseconds} ms");
Console.WriteLine($"Total memory used: {memoryUsed / 1024.0 / 1024.0:N2} MB");

Employee[] newEmployees =
     {
        new Employee { Name = "Charlie", Id = 3 },
        new Employee { Name = "Dana", Id = 4 }
    };

newEmployees.TryGetSpan<Employee>(out var span);


new StackAllockTest().HeapAllocTest();  

BenchmarkRunner.Run<StackAllockTest>();
Console.ReadLine();
public struct EmployeeStruct
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Department { get; set; }

    public EmployeeStruct(string name, int age, string department)
    {
        Name = name;
        Age = age;
        Department = department;
    }
}


