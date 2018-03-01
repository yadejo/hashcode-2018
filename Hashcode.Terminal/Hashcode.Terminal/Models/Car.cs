using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Hashcode.Terminal.Models
{
    public class Car
    {
        public Point Position { get; set; }
        private Ride ride;
        private int points;
        private bool hasPassanger;
        public Car()
        {
            Position = new Point(0, 0);
            points = 0;
        }

        public void AssignRide(Ride ride)
        {
            this.ride = ride;
            this.hasPassanger = false;
            if (ride.Start == Position)
                this.hasPassanger = true;
        }

        public void Tick(int iteration)
        {
            if(hasPassanger)
            {
                if (ride.startTick >= iteration)
                    DriveTo(ride.End);
            }
            else
            {
                DriveTo(ride.Start);
            }
        }

        private void DriveTo(Point location)
        {
            if(Position.X < location.X)
            {
                Position = new Point(Position.X + 1, Position.Y);
            }
            else if(Position.X > location.X)
            {
                Position = new Point(Position.X - 1, Position.Y);
            }
            else if(Position.Y < location.Y)
            {
                Position = new Point(Position.X, Position.Y + 1);
            }
            else if (Position.Y > location.Y)
            {
                Position = new Point(Position.X, Position.Y - 1);
            } else
            {
                //arrived
            }
        }
        
    }
}
