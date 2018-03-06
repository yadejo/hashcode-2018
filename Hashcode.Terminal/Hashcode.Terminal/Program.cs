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
            var example = "./a_example.in";
            var easy = "./b_should_be_easy.in";
            var noHurry = "./c_no_hurry.in";
            var metroplis = "./d_metropolis.in";
            var highBonus = "./e_high_bonus.in";
            var aMap = InputParser.ParseInput(example);
            var bMap = InputParser.ParseInput(easy);
            var cMap = InputParser.ParseInput(noHurry);
            var dMap = InputParser.ParseInput(metroplis);
            var eMap = InputParser.ParseInput(highBonus);
            Console.WriteLine("Starting example map");
            DoMap(aMap, "a_output");
            Console.WriteLine("Starting easy map");
            DoMap(bMap, "b_output");
            Console.WriteLine("Starting no hurry map");
            DoMap(cMap, "c_output");
            Console.WriteLine("Starting metropolis map");
            DoMap(dMap, "d_output");
            Console.WriteLine("Starting highbonus map");
            DoMap(eMap, "e_output");
            Console.Read();
        }

        private static void DoMap(ValuesMap map, string outputFileName)
        {
            var aBoard = new Board(map);
            Console.WriteLine("starting");
            aBoard.Start();
            Console.WriteLine(aBoard.handled.Count);
            Console.WriteLine("finishing up");
            var result = new List<string>();
            foreach (var item in aBoard.handled)
            {
                var str = $"{item.Value.Count}";
                foreach (var rides in item.Value)
                {
                    str += $" {rides.Id}";
                }
                result.Add(str);
            }
            File.WriteAllLines($"./{outputFileName}.txt", result.ToArray());
            Console.WriteLine("done");
        }
    }
}
