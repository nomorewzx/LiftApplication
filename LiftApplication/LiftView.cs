using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftApplication
{
    public class LiftView
    {
        private LiftController liftController;

        public LiftView()
        {
            liftController = new LiftController();
        }

        public void ShowLiftStatus()
        {
            var lifts = liftController.GetAllLifts();
            foreach (var liftStatus in lifts)
            {
                Console.WriteLine("The lift " + liftStatus.Lift.ID + " is now on " + liftStatus.CurrentFloor +" floor. Going " + liftStatus.MontionStatus);                
            }
        }
    }
}
