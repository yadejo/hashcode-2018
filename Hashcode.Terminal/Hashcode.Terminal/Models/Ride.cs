using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Hashcode.Terminal.Models
{
    public class Ride
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public int startTick { get; set; }
        public int EndTick { get; set; }

        public int GetDuration()
        {
            return 0;
        }
    }
}
