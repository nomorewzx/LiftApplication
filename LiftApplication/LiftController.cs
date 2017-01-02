using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LiftApplication
{
    public class LiftController
    {
        private const int MaxFloor = 30;
        private const int MiniFloor = -2;

        private readonly IList<Lift> _lifts = new List<Lift>()
        {
            new Lift()
            {
                Id = 1,
                LiftStatus = new LiftStatus()
                {
                    CurrentFloor = 1,
                    MontionStatus = LiftMotionStatus.Still,
                    DestinationFloor = 1
                }
            },
            new Lift()
            {
                Id = 2,
                LiftStatus = new LiftStatus()
                {
                    CurrentFloor = 1,
                    MontionStatus = LiftMotionStatus.Still,
                    DestinationFloor = 1
                }
            }
        };

        public void RetrieveAndChangeLiftStatus()
        {
            foreach (var lift in _lifts)
            {
                //Todo: deal with sad path
                if (IsLiftInLegalFloor(lift))
                {
                    UpdateLiftStatus(lift);
                }
            }
        }

        public IList<Lift> GetAllLifts()
        {
            return _lifts;
        }

        public void AddDestinationFloor(int liftId, int floor)
        {
            foreach (var lift in _lifts)
            {
                //Todo: deal with sad path
                if ( lift.Id == liftId && CheckIsFloorRight(floor))
                {
                    lift.LiftStatus.DestinationFloor = floor;
                }
            }
        }

        public bool CheckAllLiftsInStillStatus()
        {
            return _lifts.All(x => x.LiftStatus.MontionStatus == LiftMotionStatus.Still);
        }

        private void UpdateLiftStatus(Lift lift)
        {
            var flag = lift.LiftStatus.DestinationFloor - lift.LiftStatus.CurrentFloor;

            //Todo: If we only have one destination floor, the liftStatus.MotionStatus will be useless, but we will store more than one destination floor.
            //Todo: so keep the liftStatus.MotionStatus right here for now!

            var shouldMotionStatusChangeTo = LiftMotionStatus.Still;

            if (flag > 0)
            {
                shouldMotionStatusChangeTo = LiftMotionStatus.Up;
            }
            else if(flag < 0)
            {
                shouldMotionStatusChangeTo = LiftMotionStatus.Down;
            }
            else
            {
                shouldMotionStatusChangeTo = LiftMotionStatus.Still;
            }

            lift.LiftStatus.MontionStatus = shouldMotionStatusChangeTo;

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

        private bool IsLiftInLegalFloor(Lift lift)
        {
            return CheckIsFloorRight(lift.LiftStatus.CurrentFloor) &&
                   CheckIsFloorRight(lift.LiftStatus.DestinationFloor);
        }

        public bool CheckIsFloorRight(int floor)
        {
            return floor <= MaxFloor && floor >= MiniFloor && floor != 0;
        }
    }
}
