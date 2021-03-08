using System;
using RomanMath.Impl;

namespace RomanMath.Console
{
	class Program
	{
		/// <summary>
		/// Use this method for local debugging only. The implementation should remain in RomanMath.Impl project.
		/// See TODO.txt file for task details.
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
            Service.RomanNumbers();

            bool finishEnter = false;
            do
            {
                System.Console.WriteLine("Math expression:  ");
                string str = System.Console.ReadLine();

                System.Console.WriteLine("Calculation result = " + Service.Evaluate(str));

                System.Console.Write("Press 'y' to finish or any other key to continue: ");
                if (System.Console.ReadLine() == "y") finishEnter = true;
            }
            while (!finishEnter);

			System.Console.ReadKey();
		}
	}
}
