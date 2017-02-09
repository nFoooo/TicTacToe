using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using TicTacToe.Model;
using TicTacToe.View;

namespace TicTacToe.Controller
{
    public class GameController
    {
        //Constructors

        public GameController(GameView gV, GameEngine gE)
        {
            _model = gE;
            _view = gV;
            _view.SetController(this);
        }

        //Attributes

        GameEngine _model;
        GameView _view;

        //Methods

        public void SquareClick(Label sender, int position)
        {
            // If the single player mode is activated and Player 2 plays first, the AI must play first. 
            if (!_model.GetMultiplayer() && !_model.GetXFirst() && _model.GetCounter() == 0)
                AiMove();



            int clickCount = _model.GetCounter() + 1;
            bool aiPlays = false; //This boolean is used to figure out if the AI needs to make a play or not

            //Verify that the clicked label was empty
            if (sender.Text != "O" && sender.Text != "X")
            {
                aiPlays = true; //Our move was valid so the AI must play next 
                 
                if (clickCount % 2 == 0)
                {
                    if (_model.GetXFirst())
                        sender.Text = "O";
                    else
                        sender.Text = "X";

                }
                else
                {
                    if (_model.GetXFirst())
                        sender.Text = "X";
                    else
                        sender.Text = "O";
                }         

                _model.IncrementCounter();
                _model.UpdateGameState(position);
            }

        

            //Check for a win
            if (_model.GetGameWon())
            {
                if (clickCount % 2 == 0) //0 = O, GameEnded("loss") send a Player 2 won message
                    GameEnded("loss");
                else                     //1 = X, GameEnded("win") send a Player 1 won message
                    GameEnded("won");

                //reset game
                aiPlays = false; //the game is over, we do not want the AI playing
                _model.ResetGameState();
                _view.ResetSquares();
               
            }
            else if (clickCount >= 9)
            {
                GameEnded("tie");

                //resetgame
                aiPlays = false; //the game is over, we do not want the AI playing
                _model.ResetGameState();
                _view.ResetSquares();
            }

            //Update view so that the played move shows before AI plays their move
            _view.Update();

            //The AI plays only if the initial move was valid and if it's in 2 player mode
            if (_model.GetMultiplayer())
                aiPlays = false;

            if (aiPlays)
            {
                int move = 0; //string used to inform view if the move is an X or an O

                //increment and retrieve count
                _model.IncrementCounter();
                clickCount = _model.GetCounter(); 

                move = clickCount % 2;

                if (_model.GetIsHard())
                    _view.UpdateSquare(_model.AiPlayMoveHard(), move, _model.GetXFirst());
                else
                    _view.UpdateSquare(_model.AiPlayMoveEasy(), move, _model.GetXFirst());


                //Check for a win again
                if (_model.GetGameWon())
                {
                    if (move == 0)         //0 = O, GameEnded("loss") send a Player 2 won message
                        GameEnded("loss");
                    else                   //1 = X, GameEnded("win") send a Player 1 won message
                        GameEnded("won");

                    //reset game
                    _model.ResetGameState();
                    _view.ResetSquares();

                    //Play AI move if AI is playing and goes first
                    if (!_model.GetMultiplayer() && !_model.GetXFirst())
                        AiMove();
                }
                else if (clickCount >= 9)
                {
                    GameEnded("tie");

                    //resetgame
                    _model.ResetGameState();
                    _view.ResetSquares();

                    //Play AI move if AI is playing and goes first
                    if (!_model.GetMultiplayer() && !_model.GetXFirst())
                        AiMove();
                }
            }
           
        }

        public void GameEnded(string result) //"won" = 1P wins, "loss" = 2P wins, "tie" = no winner 
        {
            String message = "";
            if (result == "won")
                message = "Congratulations to Player1 for winning! Play again?";
            if (result == "loss")
                message = "Congratulations to Player2 for winning! Play again?";
            if (result == "tie")
                message = "This game resulted in a tie! Play again?";

            //Normal behavior
            if (result != "")
            {
                DialogResult dialogResult = MessageBox.Show(message, "Game Ended", MessageBoxButtons.YesNo);
                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        break;
                    case DialogResult.No:
                        _view.Close();
                        break;
                }
            }
            //The window was closed without the game finishing
            else
                _model.ResetGameState();
        }

        public void AiMove()
        {
            {
                int move; //int used to inform view if the move is an X or an O

                //increment and retrieve count
                _model.IncrementCounter();

                move = _model.GetCounter() % 2;

                if (_model.GetIsHard())
                    _view.UpdateSquare(_model.AiPlayMoveHard(), move, _model.GetXFirst());
                else
                    _view.UpdateSquare(_model.AiPlayMoveEasy(), move, _model.GetXFirst());
            }
        }

    }
}
