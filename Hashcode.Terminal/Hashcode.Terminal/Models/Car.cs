using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Hashcode.Terminal.Models
{
    public class Car
    {
        public int Id { get; set; }
        public Point Position { get; set; }
        public Ride ride { get; private set; }
        private int points;
        private bool hasPassanger;
        public List<int> HandledIds { get; set; }
        public int Score { get; set; }
        public Car()
        {
            Position = new Point(0, 0);
            points = 0;
            Score = 0;
            HandledIds = new List<int>();
        }

        public void AssignRide(Ride ride)
        {
            this.ride = ride;
            this.hasPassanger = false;
            ride.Taken = true;
            if (ride.Start == Position)
                this.hasPassanger = true;
        }

        public void Tick(int iteration)
        {
            if(hasPassanger)
            {
                if (ride.startTick >= iteration)
                {
                    Score++;
                    DriveTo(ride.End, iteration);
                }
            }
            else
            {
                if(ride != null)
                DriveTo(ride.Start, iteration);
            }
        }

        private void DriveTo(Point location, int iteration)
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
                HandledIds.Add(ride.Id);
                ride.handled = true;
            }
        }
        
    }
}
