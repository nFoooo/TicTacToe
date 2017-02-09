using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Model;
using TicTacToe.View;

namespace TicTacToe.Controller
{
    public class WelcomeScreenController
    {
        //Constructors

        public WelcomeScreenController( WelcomeScreen wS )
        {
            _view = wS;
            _model = new GameEngine();
            _view.SetController(this);
        }


        //Attributes

        WelcomeScreen _view;
        GameEngine _model; 

        //Methods

        public void StartGame(bool multiplayer)
        {
            //Launch the game 
            GameView gV = new GameView();
            GameController gC = new GameController(gV, _model);

            //Set game to 1 or 2 players
            _model.SetMultiplayer(multiplayer);

            //In the case of a multiplayer game where the AI goes first, they play their first move right away
            if (!multiplayer && (_model.GetXFirst() == false))
                gC.AiMove();
            gV.ShowDialog();
        }

        public void StartSettings()
        {
            //Launch the settings form
            Settings settingsView = new Settings();
            SettingsController sC = new SettingsController(settingsView, _model);
            settingsView.ShowDialog();
        }

    }
}
