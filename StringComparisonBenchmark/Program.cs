using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;

public class Program
{
    private static void Main(string[] args)
    {
        _ = BenchmarkRunner.Run<StringComparisonBenchmarkerDemo>();
    }
}

[MemoryDiagnoser]
public class StringComparisonBenchmarkerDemo
{
    private readonly int dataMaxCount = 1000;
    private readonly List<string> data = new List<string>();
    private static Random random = new Random();

    [GlobalSetup]
    public void GlobalSetup()
    {
        for (int i = 0; i < dataMaxCount; i++)
        {
            data.Add(RandomString(random.Next(25)));
        }
    }

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    [Benchmark]
    public bool StringComparisonUsingEqualsOperator()
    {
        return data[random.Next(dataMaxCount)] == data[random.Next(dataMaxCount)];
    }

    [Benchmark]
    public bool StringComparisonUsingEqualsKeyword()
    {
        return data[random.Next(dataMaxCount)].Equals(data[random.Next(dataMaxCount)]);
    }

    [Benchmark]
    public bool StringComparisonUsingEqualsKeywordWithOrdinal()
    {
        return data[random.Next(dataMaxCount)].Equals(data[random.Next(dataMaxCount)], StringComparison.Ordinal);
    }
}