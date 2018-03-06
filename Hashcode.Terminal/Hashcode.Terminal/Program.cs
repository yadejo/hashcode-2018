using Hashcode.Terminal.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Hashcode.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            var res = InputParser.ParseInput("./d_metropolis.in");
            var board = new Board(res);
            Console.WriteLine("starting");
            board.Start();
            Console.WriteLine(board.handled.Count);
            Console.WriteLine("finishing up");
            var result = new List<string>();
            foreach (var item in board.handled)
            {
                var str = $"{item.Value.Count}";
                foreach (var rides in item.Value)
                {
                    str += $" {rides.Id}";
                }
                result.Add(str);
            }
            File.WriteAllLines("./output.txt", result.ToArray());
            Console.WriteLine("done");
            Console.Read();
        }
    }
}
