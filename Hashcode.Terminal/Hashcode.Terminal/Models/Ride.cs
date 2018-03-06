using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Hashcode.Terminal.Models
{
    public class Ride
    {
        public int Id { get; set; }
        public Point Start { get; set; }
        public Point End { get; set; }
        public int startTick { get; set; }
        public int EndTick { get; set; }
        public bool Taken { get; set; }
        public bool handled { get; set; }

        public int GetDuration()
        {
            return 0;
        }


    }
}
