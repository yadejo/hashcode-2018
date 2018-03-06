using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Hashcode.Terminal.Models
{
    public class CarBoard
    {
        private int[,] map;
        private ValuesMap _valueMap;
        public Car[] cars;
        private Ride[] rides;
        private List<Ride> rideList = new List<Ride>();
        private List<int> takenRides = new List<int>();
        private List<Car> _availableCars = new List<Car>();
        public Dictionary<int, List<Ride>> handled { get; }
        private double avarageDistance;
        private double bonusOffset;
        
        public CarBoard(ValuesMap valueMap)
        {
            _valueMap = valueMap;
            map = new int[valueMap.rows, valueMap.columns];
            cars = new Car[valueMap.vihicules];
            handled = new Dictionary<int, List<Ride>>();
            for (var i = 0; i < cars.Length; i++)
            {
                cars[i] = new Car();
                cars[i].Id = i;
                _availableCars.Add(cars[i]);
            }
            rides = valueMap.Rides.OrderBy(r => r.startTick).ToArray();
            rideList = valueMap.Rides.OrderBy(r => r.startTick).ToList();
            avarageDistance = rideList.Average(x => CalculateDistance(x.Start, x.End));
            bonusOffset = (_valueMap.bonus + avarageDistance) / avarageDistance;
        }

        public void Start()
        {
            Console.WriteLine($"Calculating route for all cars");
            for (int i = 0; i < cars.Length; i++)
            {
                var current = cars[i];
                var iteration = 0;
                handled.Add(i, new List<Ride>());
                while(iteration < _valueMap.steps)
                {
                    if(current.ride == null || current.ride.handled)
                    {
                        if(current.ride!= null)
                            handled[i].Add(current.ride);
                        var ride = GetBestRide(current, iteration);
                        if (ride == null)
                            break;
                        current.AssignRide(ride);
                    }
                    current.Tick(iteration);
                    
                    iteration++;
                }
                Console.WriteLine($"Calculated Route for car {current.Id}. car has an efficiency rating of {(current.EfficientTicks * 1.0) / current.InefficientTicks}");
            }
        }

        private double GetRideValue(Ride ride, Point carPos, int iteration)
        {
            var rideDistance = CalculateDistance(ride.End, ride.Start);
            var score = CalculateDistance(ride.End, ride.Start);
            var distanceFromCar = CalculateDistance(carPos, ride.Start);
            var arrivalTime = iteration + distanceFromCar;
            if (arrivalTime <= ride.startTick)
            {
                score -= (ride.startTick - arrivalTime);
                score += _valueMap.bonus;
                if (rideDistance > distanceFromCar)
                    score += (rideDistance - distanceFromCar);
            }
            else
            {
                score -= distanceFromCar;
                if (rideDistance > distanceFromCar)
                    score += (rideDistance - distanceFromCar);
            }
            var earliestEndTime = iteration + distanceFromCar + rideDistance;
            if (earliestEndTime > ride.EndTick)
            {
                return int.MinValue;
            }
            return score;
        }

        private Ride GetBestRide(Car current, int iteration)
        {
            var ride = rideList.OrderByDescending(x => GetRideValue(x, current.Position, iteration)).FirstOrDefault();
            if (ride == null)
                return null;
            var index = rideList.FindIndex(x => x.Id == ride.Id);
            rideList.RemoveAt(index);
            return ride;
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

