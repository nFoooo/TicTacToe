using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToe.Controller;

namespace TicTacToe.View
{
    public partial class GameView : Form
    {
        //Constructors

        public GameView()
        {
            InitializeComponent();
            
        }



        //Attributes

        GameController _controller;



        //Methods

        public void SetController( GameController gC )
        {
            _controller = gC;
        }

        public void ResetSquares()
        {
            this.topLeft.Text = "";
            this.topCenter.Text = "";
            this.topRight.Text = "";
            this.centerLeft.Text = "";
            this.label9.Text = "";
            this.center.Text = "";
            this.bottomLeft.Text = "";
            this.bottomCenter.Text = "";
            this.bottomRight.Text = "";
            this.centerRight.Text = "";
        }

        public void UpdateSquare(int position, int move, bool xFirst)
        {
            string movePlayed = "";
            if (move == 0)
            {
                if (xFirst)
                    movePlayed = "O";
                else
                    movePlayed = "X";
            }
                
            else if (xFirst)
                movePlayed = "X";
            else
                movePlayed = "O";


            switch (position)
            {
                case 0:
                    topLeft.Text = movePlayed;
                    break;
                case 1:
                    topCenter.Text = movePlayed;
                    break;
                case 2:
                    topRight.Text = movePlayed;
                    break;
                case 3:
                    centerLeft.Text = movePlayed;
                    break;
                case 4:
                    center.Text = movePlayed;
                    break;
                case 5:
                    centerRight.Text = movePlayed;
                    break;
                case 6:
                    bottomLeft.Text = movePlayed;
                    break;
                case 7:
                    bottomCenter.Text = movePlayed;
                    break;
                case 8:
                    bottomRight.Text = movePlayed;
                    break;
                default:
                    break;
            }
        }

        //Events

        private void centerLeft_Click(object sender, EventArgs e)
        {
            var square = sender as Label;
            _controller.SquareClick(square, 3);
        }

        private void center_Click(object sender, EventArgs e)
        {
            var square = sender as Label;
            _controller.SquareClick(square, 4);
        }

        private void centerRight_Click(object sender, EventArgs e)
        {
            var square = sender as Label;
            _controller.SquareClick(square, 5);
        }

        private void topLeft_Click(object sender, EventArgs e)
        {
            var square = sender as Label;
            _controller.SquareClick(square, 0);
        }

        private void topRight_Click(object sender, EventArgs e)
        {
            var square = sender as Label;
            _controller.SquareClick(square, 2);
        }

        private void topCenter_Click(object sender, EventArgs e)
        {
            var square = sender as Label;
            _controller.SquareClick(square, 1);
        }

        private void bottomRight_Click(object sender, EventArgs e)
        {
            var square = sender as Label;
            _controller.SquareClick(square, 8);
        }

        private void bottomCenter_Click(object sender, EventArgs e)
        {
            var square = sender as Label;
            _controller.SquareClick(square, 7);
        }

        private void bottomLeft_Click(object sender, EventArgs e)
        {
            var square = sender as Label;
            _controller.SquareClick(square, 6);
        }

        private void GameView_FormClosed(object sender, FormClosedEventArgs e)
        {
            _controller.GameEnded("");
        }
    }
}
