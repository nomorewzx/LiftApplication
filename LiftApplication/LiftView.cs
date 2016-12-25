using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
                Console.WriteLine("Lift: " + liftStatus.Lift.ID + " ON: " + liftStatus.CurrentFloor +" f. Going: " + liftStatus.MontionStatus);                
            }
        }

        public void RetrieveAndShowLiftStatus()
        {
            liftController.AddDestinationFloor(1, 24);
            liftController.RetrieveAndChangeLiftStatus();

            while (!liftController.CheckAllLiftsInStillStatus())
            {
                Thread.Sleep(1000);
                liftController.RetrieveAndChangeLiftStatus();
                ShowLiftStatus();
            }

        }
    }
}
