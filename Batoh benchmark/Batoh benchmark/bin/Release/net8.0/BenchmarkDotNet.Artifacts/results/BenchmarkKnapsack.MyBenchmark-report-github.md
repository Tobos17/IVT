```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.3476)
11th Gen Intel Core i5-11400H 2.70GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method                      | Mean     | Error   | StdDev  | Gen0   | Allocated |
|---------------------------- |---------:|--------:|--------:|-------:|----------:|
| Knapsack_Backtracking       | 439.7 ns | 8.43 ns | 7.47 ns | 0.0200 |     128 B |
| Knapsack_DynamicProgramming | 706.4 ns | 9.18 ns | 8.59 ns | 0.1669 |    1048 B |
