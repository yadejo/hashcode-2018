using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hashcode.Terminal.Models
{
    public class ValuesMap
    {
        public int rows { get; set; }
        public int columns { get; set; }
        public int vihicules { get; set; }


        private int _rides;
        public int ridesCount
        {
            get { return _rides; }
            set
            {
                _rides = value;
            }
        }

        public int bonus { get; set; }
        public int steps { get; set; }

        public List<Ride> Rides { get; set; }
        public ValuesMap()
        {
            Rides = new List<Ride>();
        }

        public void AddRide(Ride r)
        {
            Rides.Add(r);
        }


    }
}
