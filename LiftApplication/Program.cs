﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftApplication
{
    public class Program
    {
        static void Main(string[] args)
        {
            var liftView = new LiftView();
            liftView.ShowLiftStatus();
            Console.ReadLine();
        }
    }
}
