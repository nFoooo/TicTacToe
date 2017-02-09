using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TicTacToe.Model;
using TicTacToe.Controller;

namespace TicTacToe.View
{
    public partial class WelcomeScreen : Form
    {
        //Constructors

        public WelcomeScreen()
        {
            InitializeComponent();     
        }

        //Attributes

        WelcomeScreenController _controller;

        //Methods

        public void SetController(WelcomeScreenController wSC)
        {
           _controller = wSC;
        }

        //Events
       
        private void btn1P_Click(object sender, EventArgs e)
        {
            //TODO : Create AI for 1P
            _controller.StartGame(false); //Starts the game with multiplayer set to false
        }

        private void btn2P_Click(object sender, EventArgs e)
        {
            //TODO : Start GameView
            _controller.StartGame(true); //Starts the game with multiplayer set to true
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            //TODO : Create Settings Winform
            _controller.StartSettings();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

   
}
