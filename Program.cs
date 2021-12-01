using AoC21.Problems;

Problem problem; 

problem = new P1(@".\Inputs\P1.txt");
Console.WriteLine($"P1 = {problem.Compute()}");

problem = new P2(@".\Inputs\P2.txt");
Console.WriteLine($"P2 = {problem.Compute()}");