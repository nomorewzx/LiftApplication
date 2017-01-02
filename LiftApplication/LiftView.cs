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
        private readonly LiftController _liftController;
        private readonly UserInputHandler _userInputHandler;

        public LiftView()
        {
            _liftController = new LiftController();
            _userInputHandler = new UserInputHandler();
        }

        public void ShowLiftStatus()
        {
            var lifts = _liftController.GetAllLifts();
            foreach (var lift in lifts)
            {
                Console.WriteLine("Lift: " + lift.Id + " ON: " + lift.LiftStatus.CurrentFloor +" f. Going: " + lift.LiftStatus.MontionStatus);                
            }
        }

        public void RetrieveAndShowLiftStatus()
        {
            ShowLiftStatus();
            while (true)
            {
                if (_liftController.CheckAllLiftsInStillStatus())
                {
                    var inputs = _userInputHandler.HandleInputInsideLift();
                    _liftController.AddDestinationFloor(inputs[0], inputs[1]);
                }

                Thread.Sleep(1000);
                _liftController.RetrieveAndChangeLiftStatus();
                ShowLiftStatus();
            }
        }
    }
}
