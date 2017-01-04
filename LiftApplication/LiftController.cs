using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiftApplication
{
    public class LiftController
    {
        private const int MaxFloor = 30;
        private const int MiniFloor = -2;

        #region init lifts
        private readonly IList<Lift> _lifts = new List<Lift>()
        {
            new Lift()
            {
                Id = 1,
                LiftStatus = new LiftStatus()
                {
                    CurrentFloor = 1,
                    MontionStatus = LiftMotionStatus.Still,
                    DestinationFloors = new List<int>()
                }
            },
            new Lift()
            {
                Id = 2,
                LiftStatus = new LiftStatus()
                {
                    CurrentFloor = 1,
                    MontionStatus = LiftMotionStatus.Still,
                    DestinationFloors = new List<int>()
                }
            }
        };
        # endregion

        public IList<Lift> GetAllLifts()
        {
            return _lifts;
        }

        public void RetrieveAndChangeLiftStatus()
        {
            foreach (var lift in _lifts)
            {
                UpdateLiftStatus(lift);
            }
        }

        public void AddDestinationFloors(List<int> destinationFloors)
        {
            var lift = _lifts.FirstOrDefault(x => x.Id == destinationFloors[0]);
            for (int i = 1; i < destinationFloors.Count; i++)
            {
                if (lift.LiftStatus.DestinationFloors.All(x => x != destinationFloors[i]))
                {
                    AddDestionFloor(destinationFloors[i], lift);
                }
            }
        }

        private void AddDestionFloor(int i, Lift lift)
        {
            if (CheckIsFloorRight(i))
            {
                lift.LiftStatus.DestinationFloors.Add(i);
            }
        }

        public bool CheckAllLiftsInStillStatus()
        {
            return _lifts.All(x => x.LiftStatus.MontionStatus == LiftMotionStatus.Still);
        }

        private void UpdateLiftStatus(Lift lift)
        {
            UpdateLiftMotionStatus(lift);

            UpdateLiftCurrentFloor(lift);
        }

        private static void UpdateLiftMotionStatus(Lift lift)
        {
            lift.ShouldLiftMove();

            if (lift.LiftStatus.DestinationFloors.Any() &&
                lift.LiftStatus.CurrentFloor > lift.LiftStatus.DestinationFloors.Max())
            {
                lift.LiftStatus.MontionStatus = LiftMotionStatus.Down;
            }

            if (lift.LiftStatus.DestinationFloors.Any() &&
                lift.LiftStatus.CurrentFloor < lift.LiftStatus.DestinationFloors.Min())
            {
                lift.LiftStatus.MontionStatus = LiftMotionStatus.Up;
            }

            if (lift.ShouldStopMoving())
            {
                Console.WriteLine("<<<<<PLEASE GO IN OR OUT AND CLOSE THE DOOR>>>>>");
                Thread.Sleep(1000);
                lift.LiftStatus.DestinationFloors.Remove(lift.LiftStatus.CurrentFloor);
                if (!lift.LiftStatus.DestinationFloors.Any())
                {
                    lift.LiftStatus.MontionStatus = LiftMotionStatus.Still;
                }
            }
        }

        private void UpdateLiftCurrentFloor(Lift lift)
        {
            switch (lift.LiftStatus.MontionStatus)
            {
                case LiftMotionStatus.Up:
                    lift.LiftStatus.CurrentFloor = GetUpperNextFloor(lift.LiftStatus.CurrentFloor);
                    break;
                case LiftMotionStatus.Down:
                    lift.LiftStatus.CurrentFloor = GetLowerNextFloor(lift.LiftStatus.CurrentFloor);
                    break;
            }
        }

        private int GetLowerNextFloor(int currentFloor)
        {
            var lowerOneFloor = currentFloor - 1 == 0 ? -1 : currentFloor - 1;
            if (CheckIsFloorRight(lowerOneFloor))
            {
                return lowerOneFloor;
            }
            return currentFloor;
        }

        private int GetUpperNextFloor(int currentFloor)
        {
            var upperOneFloor = currentFloor + 1 == 0 ? 1 : currentFloor + 1;
            if (CheckIsFloorRight(upperOneFloor))
            {
                return upperOneFloor;
            }

            return currentFloor;
        }

        public bool CheckIsFloorRight(int floor)
        {
            return floor <= MaxFloor && floor >= MiniFloor && floor != 0;
        }
    }
}
