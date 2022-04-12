using BenchmarkDotNet.Attributes;
using csharp_valuetask;

[MemoryDiagnoser]
public class TaskBenchmark
{
    private GitHubService _service;
    private int _iterations;
    private string[] _names;

    [GlobalSetup]
    public void Setup()
    {
        _service = new GitHubService();
        _iterations = 100_000;
        _names = new string[] { "matheusmig", "PoaTek", "microsoft" };
    }

    [Benchmark]
    public async Task RunTask()
    {
        for (int x = 0; x < _iterations; x++)
        {
            foreach (var name in _names)
            {
                var repos = await _service.GetRepoAsyncTask(name);
            }
        };
    }

    [Benchmark]
    public async Task RunValueTask()
    {
        for (int x = 0; x < _iterations; x++)
        {
            foreach (var name in _names)
            {
                var repos = await _service.GetRepoAsyncValueTask(name);
            }
        };
    }
}