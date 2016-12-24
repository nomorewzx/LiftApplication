using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftApplication
{
    public class LiftStatus
    {
        public int CurrentFloor { get; set; }
        public Lift Lift { get; set; }
        public LiftMotionStatus MontionStatus { get; set; }
        public int DestinationFloor { get; set; }
    }
}
