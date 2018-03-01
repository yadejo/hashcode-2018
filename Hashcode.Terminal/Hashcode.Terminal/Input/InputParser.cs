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
            output.ridesCount = int.Parse(values[3]);
            output.bonus = int.Parse(values[4]);
            output.steps = int.Parse(values[5]);

            for (int index = 1; index < lines.Length; index++)
            {
                var line = lines[index].Split(' ');
                output.AddRide(new Ride
                {
                    StartPoint = new Point(int.Parse(line[0]),int.Parse(line[1])),
                    EndPoint = new Point(int.Parse(line[2]), int.Parse(line[3])),
                    EndTime = int.Parse(line[4]),
                    StartTime = int.Parse(line[5])
                });
            }

            return output;
        }
    }
}
