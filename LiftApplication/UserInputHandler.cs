using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftApplication
{
    public class UserInputHandler
    {
        public int[] HandleInputInsideLift()
        {
            Console.WriteLine("Now please input LIFT ID and expected floor splited with one space. i.e. 1 23");
            //Todo: deal with sad path. Mainly illegal input
            var rawInput = Console.ReadLine();
            var inputArgs = rawInput.Split(' ').ToArray();

            var inputInsideLift = new int[] {int.Parse(inputArgs[0]), int.Parse(inputArgs[1])};
            return inputInsideLift;
        }
    }
}
