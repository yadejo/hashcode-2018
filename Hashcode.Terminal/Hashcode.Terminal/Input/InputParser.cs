using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Hashcode.Terminal.Models;

namespace Hashcode.Terminal
{
    public class InputParser
    {

        public static ValuesMap ParseInput(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);

           // read first line
            var values = lines[0].Split(' ');
            var rows = values[0];
            var columns = values[1];
            var vihicules = values[2];
            var rides = values[3];
            var bonus = values[4];
            var steps = values[5];
            
            foreach (string line in lines)
            {
               
            }

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }
    }
}
