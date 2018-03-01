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

            var output = new ValuesMap();
            string[] lines = System.IO.File.ReadAllLines(path);

           // read first line
            var values = lines[0].Split(' ');
            output.rows = int.Parse( values[0]);
            output.columns = int.Parse(values[1]); 
            output.vihicules = int.Parse(values[2]);
            output.rides = int.Parse(values[3]);
            output.bonus = int.Parse(values[4]);
            output.steps = int.Parse(values[5]);
            
            foreach (string line in lines)
            {
               
            }

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }
    }
}
