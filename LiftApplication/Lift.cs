using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftApplication
{
    public class Lift
    {
        public int Id { get; set; }
        public LiftStatus LiftStatus { get; set; }

        public bool ShouldLiftMove()
        {
            if (this.LiftStatus.MontionStatus == LiftMotionStatus.Still && this.LiftStatus.DestinationFloors.Any())
            {
                var minDistWithCurrentFloor = 10000;
                foreach (var destinationFloor in this.LiftStatus.DestinationFloors)
                {
                    var dist = destinationFloor - this.LiftStatus.CurrentFloor;
                    if (Math.Abs(dist) < minDistWithCurrentFloor)
                    {
                        minDistWithCurrentFloor = Math.Abs(dist);
                        if (dist < 0)
                        {
                            this.LiftStatus.MontionStatus = LiftMotionStatus.Down;
                        }
                        else
                        {
                            this.LiftStatus.MontionStatus = LiftMotionStatus.Up;
                        }
                    }
                }
                return true;   
            }
            return false;
        }

        public bool ShouldStopMoving()
        {
            if (LiftStatus.CurrentFloor == GetNextStopFloor() && LiftStatus.MontionStatus != LiftMotionStatus.Still)
            {
                this.LiftStatus.DestinationFloors.Remove(this.LiftStatus.CurrentFloor);
                return true;
            }
            return false;
        }

        public int GetNextStopFloor()
        {
            if (this.LiftStatus.MontionStatus == LiftMotionStatus.Up)
            {
                this.LiftStatus.DestinationFloors.Sort();
                foreach (int t in this.LiftStatus.DestinationFloors)
                {
                    if (t >= this.LiftStatus.CurrentFloor)
                    {
                        return t;
                    }
                }
            }
            else if(this.LiftStatus.MontionStatus == LiftMotionStatus.Down)
            {
                foreach (var t in this.LiftStatus.DestinationFloors.OrderByDescending(x => x))
                {
                    if (t <= this.LiftStatus.CurrentFloor)
                    {
                        return t;
                    }
                }
            }
            
            return this.LiftStatus.CurrentFloor;
        }
    }
}
