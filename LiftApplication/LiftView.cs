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
        private UserInputHandler userInputHandler;

        public LiftView()
        {
            liftController = new LiftController();
            userInputHandler = new UserInputHandler();
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

            while (true)
            {
                if (liftController.CheckAllLiftsInStillStatus())
                {
                    var inputs = userInputHandler.HandleInputInsideLift();
                    liftController.AddDestinationFloor(inputs[0], inputs[1]);
                }

                Thread.Sleep(1000);
                liftController.RetrieveAndChangeLiftStatus();
                ShowLiftStatus();
            }
        }
    }
}
