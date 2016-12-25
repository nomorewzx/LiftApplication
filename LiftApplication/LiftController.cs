﻿using System;
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

        private readonly IList<LiftStatus> _lifts = new List<LiftStatus>()
        {
            new LiftStatus()
            {
                CurrentFloor = 0,
                Lift = new Lift()
                {
                    ID = 1
                },
                MontionStatus = LiftMotionStatus.Still,
                DestinationFloor = 0
            },
            new LiftStatus()
            {
                CurrentFloor = 0,
                Lift = new Lift()
                {
                    ID = 2
                },
                MontionStatus = LiftMotionStatus.Still,
                DestinationFloor = 0
            }
        };

        public void RetrieveAndChangeLiftStatus()
        {
            foreach (var liftStatus in _lifts)
            {
                //Todo: deal with sad path
                if (IsLiftInLegalStatus(liftStatus))
                {
                    updateLiftStatus(liftStatus);
                }
            }
        }

        public IList<LiftStatus> GetAllLifts()
        {
            return _lifts;
        } 

        public LiftStatus CheckLiftStatus(int liftId)
        {
            return _lifts.FirstOrDefault(x => x.Lift.ID == liftId);
        }

        public void AddDestinationFloor(int liftId, int floor)
        {
            foreach (var liftStatus in _lifts)
            {
                //Todo: deal with sad path
                if ( liftStatus.Lift.ID == liftId &&floor <= MaxFloor && floor >= MiniFloor)
                {
                    liftStatus.DestinationFloor = floor;
                }
            }
        }

        public bool CheckAllLiftsInStillStatus()
        {
            return _lifts.All(x => x.MontionStatus == LiftMotionStatus.Still);
        }

        private void updateLiftStatus(LiftStatus liftStatus)
        {
            var flag = liftStatus.DestinationFloor - liftStatus.CurrentFloor;

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

            liftStatus.MontionStatus = shouldMotionStatusChangeTo;

            switch (liftStatus.MontionStatus)
            {
                case LiftMotionStatus.Up:
                    liftStatus.CurrentFloor += 1;
                    break;
                case LiftMotionStatus.Down:
                    liftStatus.CurrentFloor -= 1;
                    break;
            }
            
        }

        private bool IsLiftInLegalStatus(LiftStatus liftStatus)
        {
            return liftStatus.CurrentFloor <= MaxFloor && liftStatus.CurrentFloor >= MiniFloor &&
                   liftStatus.DestinationFloor <= MaxFloor && liftStatus.DestinationFloor >= MiniFloor;
        }
    }
}
