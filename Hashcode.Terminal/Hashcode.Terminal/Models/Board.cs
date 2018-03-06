using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Hashcode.Terminal.Models
{
    public class Board
    {
        private int[,] map;
        private ValuesMap _valueMap;
        public Car[] cars;
        private Ride[] rides;
        private List<Ride> rideList = new List<Ride>();
        private List<int> takenRides = new List<int>();
        private List<Car> _availableCars = new List<Car>();
        public Dictionary<int, List<Ride>> handled { get; }
        public Board(ValuesMap valueMap)
        {
            _valueMap = valueMap;
            map = new int[valueMap.rows, valueMap.columns];
            cars = new Car[valueMap.vihicules];
            handled = new Dictionary<int, List<Ride>>();
            for (var i= 0; i < cars.Length; i++)
            {
                cars[i] = new Car();
                cars[i].Id = i;
                _availableCars.Add(cars[i]);
            }
            rides = valueMap.Rides.OrderBy(r => r.startTick).ToArray();
            rideList = valueMap.Rides.OrderBy(r => r.startTick).ToList();
        }

        public void Start()
        {
            for (int i = 0; i < _valueMap.steps; i++)
            {
                //assign car
                AssignCars(i);
                
                for (int j = 0; j < cars.Length; j++)
                {
                    cars[j].Tick(i);
                    if(cars[j].ride != null && cars[j].ride.handled && (!handled.ContainsKey(cars[j].Id) || !handled[cars[j].Id].Contains(cars[j].ride)))
                    {
                        Console.WriteLine($"ride {cars[j].ride.Id} has been completed on iteration {i}!");
                        _availableCars.Add(cars[j]);
                        if(!handled.ContainsKey(cars[j].Id))
                        {
                            handled.Add(cars[j].Id, new List<Ride>());
                        }
                        handled[cars[j].Id].Add(cars[j].ride);
                    }
                }
            }
        }

        private void AssignCars(int iteration)
        {
            var toRemove = new List<int>();
            for (int j = 0; j < _availableCars.Count; j++)
            {
                for (int k = 0; k < rideList.Count; k++)
                {
                    var distance = CalculateDistance(_availableCars[j].Position, rideList[k].Start);
                    if (distance + iteration <= rideList[k].startTick && !takenRides.Contains(rideList[k].Id))
                    {
                        _availableCars[j].AssignRide(rideList[k]);
                        takenRides.Add(rideList[k].Id);
                        toRemove.Add(k);
                        break;
                    }
                }
            }
            toRemove = toRemove.OrderByDescending(x => x).ToList();
            foreach (var item in toRemove)
            {
                rideList.RemoveAt(item);
            }
            //if(toRemove.Count > 0)
            //    Console.WriteLine($"Assigned {toRemove.Count} rides to cars on iteration {iteration}");
            _availableCars.Clear();
        }

        private Ride GetBestRide(Car car, int iteration, ref int rangecounter)
        {
            return searchRideRecursive(car, ref rangecounter, null, iteration);
        }

        private Ride searchRideRecursive(Car car,ref int range, Ride ride, int iteration)
        {
            if (ride != null)
                return ride;
            var maxX = car.Position.X + range;
            var minX = car.Position.X - range;
            var minY = car.Position.Y - range;
            var maxY = car.Position.Y + range;
            maxX = maxX >= _valueMap.rows ? _valueMap.rows: maxX;
            minX = minX <= 0 ? 0: maxX;
            minY = minY <= 0 ? 0: minY;
            maxY = maxY >= _valueMap.columns ? _valueMap.columns : maxY;
            var availableRides = new List<Ride>();
            for (int y = minY; y <= maxY ; y++)
            {
                if (y == minY || y == maxY) {
                    for (int x = minX; x <= maxX; x++)
                    {
                        var found = rides.FirstOrDefault(r => r != null &&!r.Taken && !r.handled && r.Start.X == x && r.Start.Y == y);
                        if (found != null)
                        {
                            availableRides.Add(found);
                        }
                    }
                }
                else
                {
                    var found = rides.FirstOrDefault(r => r!= null &&!r.Taken && !r.handled && r.Start.X == minX && r.Start.Y == y);
                    if (found != null)
                        availableRides.Add(found);
                    found = rides.FirstOrDefault(r => r!= null && !r.Taken && !r.handled && r.Start.X == maxX && r.Start.Y == y);
                    if (found != null)
                        availableRides.Add(found);
                }
            }
            if(!availableRides.Any())
            {
                range++;
                return searchRideRecursive(car, ref range, null, iteration);
            }
            range++;
            var ridesss = availableRides.Where(r => r != null && r.EndTick >= iteration + CalculateDistance(r.Start, car.Position));
            return searchRideRecursive(car, ref range, ridesss.OrderBy(r => CalculateDistance(r.Start, car.Position)).FirstOrDefault(), iteration);
        }

        private int CalculateDistance(Point a, Point B)
        {
            var x = a.X - B.X;
            if (x < 0)
                x *= -1;
            var y = a.Y - B.Y;
            if (y < 0)
                y *= -1;
            return x + y;
        }
    }
}
