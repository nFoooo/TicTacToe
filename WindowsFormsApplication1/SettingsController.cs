using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Model;

namespace TicTacToe.View
{
    public class SettingsController
    {
        //Constructors

        public SettingsController(Settings settings, GameEngine gE)
        {
            _model = gE;
            _view = settings;
            _view.SetController(this);
        }


        //Attributes

        Settings _view;
        GameEngine _model;

        //Methods

        public void ChangeFirstPlayer(string player)
        {
            bool xFirst = true;
            if (player == "Player 2")
                xFirst = false;
            _model.SetXFirst(xFirst);
        }

        public void SetAiDifficulty(string difficulty)
        {
            bool isHard = false;
            if (difficulty == "Difficult")
                isHard = true;
            _model.SetIsHard(isHard);
        }
    }
}
