using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Collections;
using TicTacToe.View;
using TicTacToe.Controller;


namespace StartUp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            WelcomeScreen wS = new WelcomeScreen();
            WelcomeScreenController wSC = new WelcomeScreenController(wS);
            wS.ShowDialog();
        }
    }
}
