using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TicTacToe.Model
{
    public class GameEngine
    {
        //Constructors

        public GameEngine()
        {
            _clickCounter = 0;
            _gameState = new int[9];
            for (int i = 0; i < 9; i++)
            {
                _gameState[i] = -10;
            }
            _gameWon = false;
            _path = 0;
            _isXFirst = true;
        }

        //Attributes

        int _clickCounter;
        int[] _gameState;
        // Game state position numbers : 
        //  0  |  1  |  2
        // ----------------
        //  3  |  4  |  5
        // ----------------
        //  6  |  7  |  8

        int _path; //used to remember previous moves for the AI
        bool _gameWon;
        bool _multiplayer;
        bool _isXFirst;
        bool _isHard; 

        //Get-Set
    
        public int GetCounter()
        {
            return _clickCounter;
        }

        public void SetCounter(int counter)
        {
            _clickCounter = counter;
        }

        public bool GetGameWon()
        {
            return _gameWon;
        }

        public bool GetMultiplayer()
        {
            return _multiplayer;
        }

        public void SetMultiplayer(bool multiplayer)
        {
            _multiplayer = multiplayer;
        }

        public bool GetXFirst()
        {
            return _isXFirst;
        }

        public void SetXFirst(bool xFirst)
        {
            _isXFirst = xFirst;
        }

        public bool GetIsHard()
        {
            return _isHard;
        }

        public void SetIsHard(bool isHard)
        {
            _isHard = isHard;
        }

        //Methods

        public void IncrementCounter()
        {
            _clickCounter++;
        }

        /// <summary>
        /// UpdateGameState updates _gameState with the moveplayed and then checks to see if someone has won the game.
        /// </summary>
        /// <param name="movePlayed"> 0 represents X, 1 represents O </param>
        /// <param name="position">Represents the board from top left to bottom right, numbered 0 to 8</param>
        public void UpdateGameState(int position) 
        {
            //Update Game State
            _gameState[position] = _clickCounter%2;

            //Check for a win
            int i = 0; 
            int gameSumHorizontal = 0; //sum used to verify if a line has 3 X or 3 O
            int gameSumVertical = 0;
            int gameSumDiagonal = 0;


            //Check horizontal and vertical win conditions
            while (i < 3 && _gameWon == false)
            {
                gameSumHorizontal = _gameState[3 * i] + _gameState[3 * i + 1] + _gameState[3 * i + 2];
                gameSumVertical = _gameState[i] + _gameState[i+ 3] + _gameState[i + 6];

                if (gameSumHorizontal == 0 || gameSumHorizontal == 3 || gameSumVertical == 0 || gameSumVertical == 3)
                    _gameWon = true;
                i++;
            }

            //Check for diagonal win conditions
            gameSumDiagonal = _gameState[0] + _gameState[4] + _gameState[8];
            if (gameSumDiagonal == 0 || gameSumDiagonal == 3)
                _gameWon = true;
            gameSumDiagonal = _gameState[2] + _gameState[4] + _gameState[6];
            if (gameSumDiagonal == 0 || gameSumDiagonal == 3)
                _gameWon = true;
        }

        /// <summary>
        /// ResetGameState resets all the relevant attributes to their original state.
        /// </summary>
        public void ResetGameState()
        {
            _gameWon = false;
            for (int i = 0; i < 9; i++)
                _gameState[i] = -10;
            _clickCounter = 0;
            _path = 0;
        }

        /// <summary>
        /// AiPlayMoveEasy calculates and returns the easy AI's next move (completely random). It also updates the game state.
        /// </summary>
        /// <returns>
        /// This method returns an integer representing the position of the move played by the AI.</returns>
        public int AiPlayMoveEasy()
        {
            Random randomGenerator = new Random();
            int position = 0;
            //Generates a random int elemnt of [0,9[ representing a game position and then checks if that position is valid (-10 indicates an unplayed position)
            do
            {
                position = randomGenerator.Next(0, 9);
            }
            while (_gameState[position] != -10);
            UpdateGameState(position);
              
            return position;
        }

        /// <summary>
        /// AiPlayMovehard calculates and returns the hard AI's next move. It also updates the game state.
        /// </summary>
        /// <returns></returns>
        public int AiPlayMoveHard()
        {
            //I have found 2 ways to beat this bot, play 1 most top left and 3rd move bottom center or center right

            int positionPlayed = 0;
            int positionToPlay = 0;

            switch(_clickCounter)
            {
                //
                // AI plays X
                //

                //1st move : play in the center
                case 1:
                    positionToPlay = 4; 
                    break;

                //3rd move : If the human played in the corner, play next to it [Path 0]. If the human played in a center lane (vertical or horizontal) play in the opposite one. [Path 1]
                case 3:
                    //find the position of the move played by the human
                    while (_gameState[positionPlayed] != 0)
                        positionPlayed++;

                    if (positionPlayed % 2 == 0) // corner move
                    {
                        _path = 0;
                        positionToPlay = FindEmptyAdjacentPosition(positionPlayed);
                    }
                    else                // center lane move
                    {
                        _path = 1;
                        if (positionPlayed == 1 || positionPlayed == 7) 
                            positionToPlay = 3; //if the move was played in the vertical center lane, we play in the horizontal center lane
                        else
                            positionToPlay = 1; //if the move was played in the horizontal center lane, we play in the vertical center lane
                    }
                    break;          

                //5th move : Check for possible X win and play it. Check for possible O win and block it. If there are none, [Path 0]: play next to any O, [Path 1]: play in a corner next to both X's
                case 5:
                    //Play an X win
                    positionToPlay = CheckForXWin();
                    if (positionToPlay != -10)
                        break;

                    //Block an O win
                    positionToPlay = CheckForOWin();
                    if (positionToPlay != -10)
                        break;

                    //Path 0 : play next to any O

                    //find the position of the move played by the human
                    while (_gameState[positionPlayed] != 0)
                        positionPlayed++;
                    if (_path == 0)
                        positionToPlay = FindEmptyAdjacentPosition(positionPlayed);

                    //Path 1 : play in the corner next to both X (in either possible case, corner 0 is adjacent to both.
                    else
                        positionToPlay = 0;
                    break;

                //7th move : Check for possible X win and play it. Check for a possible O win and block it. Play at random.
                case 7:
                    //Play an X win
                    positionToPlay = CheckForXWin();
                    if (positionToPlay != -10)
                        break;

                    //Block an O win
                    positionToPlay = CheckForOWin();
                    if (positionToPlay != -10)
                        break;

                    //Play first available move
                    while (_gameState[positionPlayed] != -10)
                        positionPlayed++;
                    positionToPlay = positionPlayed;
                    break;

                //9th move : search for the only playable option
                case 9:
                    while (_gameState[positionPlayed] != -10)
                        positionPlayed++;
                    positionToPlay = positionPlayed;
                    break;
                    
                //
                // AI Plays O
                //

                //2nd move : either take the center [Path 0 ] or play in a corner to force a tie [Path 1]
                case 2:
                    if (_gameState[4] == -10)
                    {
                        _path = 0;
                        positionToPlay = 4;
                    }
                    else
                    {
                        _path = 1;
                        positionToPlay = 0;
                    }

                    break;

                //4th move : Check for a possible X win and block it. If there are none, [Path 0] play next to an O, [Path 1] : play in the top right corner.
                case 4:
                    //Check for an X win
                    positionToPlay = CheckForXWin();
                    if (positionToPlay != -10)
                        break;

                    //play next to an O
                    if (_path == 0) 
                    {
                        //find one of your moves.
                        while (_gameState[positionPlayed] != 0)
                            positionPlayed++;
                        positionToPlay = FindEmptyAdjacentPosition(positionPlayed);
                    }

                    // play the top right corner
                    else
                    {
                        positionToPlay = 2;
                    }
                    break;   

                //6th move : Check for possible O win and play it. Check for a possible X win and block it. [Path 0] = ,[Path 1] = play at random.
                case 6:
                    //Play an O win
                    positionToPlay = CheckForOWin();
                    if (positionToPlay != -10)
                        break;

                    //Block an X win
                    positionToPlay = CheckForXWin();
                    if (positionToPlay != -10)
                        break;

                    //Play first available move
                    positionToPlay = 0;
                    while (_gameState[positionToPlay] != -10)
                        positionToPlay++;
                    break;

                //8th move : Check for possible O win and play it. Check for a possible X win and block it. Play at random.
                case 8:
                    //Play an O win
                    positionToPlay = CheckForOWin();
                    if (positionToPlay != -10)
                        break;

                    //Block an X win
                    positionToPlay = CheckForXWin();
                    if (positionToPlay != -10)
                        break;

                    //Play first available move
                    positionToPlay = 0;
                    while (_gameState[positionToPlay] != -10)
                        positionToPlay++;
                    break;

                default:
                    break;
            }

            UpdateGameState(positionToPlay);
            
            return positionToPlay;
        }

        /// <summary>
        /// Checks if there is an open column or row with 2 X's in it.
        /// </summary>
        /// <returns>Returns the position of the next move necessary for an X win. Returns -10 if there are none.</returns>
        public int CheckForXWin()
        {
            //Check for a win
            int position = -10;
            int i = 0;
            int gameSumHorizontal = 0; //sum used to verify if a line has 2 O and an empty space (1 + 1 - 10 = -8)
            int gameSumVertical = 0;
            int gameSumDiagonal = 0;
            bool possibleWin = false;

            //Check horizontal and vertical win conditions
            while (i < 3 && possibleWin == false)
            {
                gameSumHorizontal = _gameState[3 * i] + _gameState[3 * i + 1] + _gameState[3 * i + 2];
                gameSumVertical = _gameState[i] + _gameState[i + 3] + _gameState[i + 6];

                if (gameSumHorizontal == -8)
                {
                    //set possibleWin to true to exit while
                    possibleWin = true;
                    //find which of the three moves is possible
                    if (_gameState[3 * i] == -10)
                        position = 3 * i;
                    else if (_gameState[3 * i + 1] == -10)
                        position = 3 * i + 1;
                    else 
                        position = 3 * i + 2;
                }
                if (gameSumVertical == -8)
                {
                    //set possibleWin to true to exit while
                    possibleWin = true;
                    //find which of the three moves is possible
                    if (_gameState[i] == -10)
                        position = i;
                    else if (_gameState[i + 3] == -10)
                        position = i + 3;
                    else
                        position = i + 6;
                }
                i++;
            }

            //Check for diagonal win conditions
            gameSumDiagonal = _gameState[0] + _gameState[4] + _gameState[8];
            if (gameSumDiagonal == -8)
            {
                if (_gameState[0] == -10)
                    position = 0;
                else if (_gameState[4] == -10)
                    position = 4;
                else
                    position = 8;
            }

            gameSumDiagonal = _gameState[2] + _gameState[4] + _gameState[6];
            if (gameSumDiagonal == -8)
            {
                if (_gameState[2] == -10)
                    position = 2;
                else if (_gameState[4] == -10)
                    position = 4;
                else
                    position = 6;
            }

            return position;
        }


        /// <summary>
        /// Checks if there is an open column or row with 2 O's in it.
        /// </summary>
        /// <returns>Returns the position of the next move necessary for an O win. Returns -10 if there are none.</returns>
        public int CheckForOWin()
        {
            //Check for a win
            int position = -10;
            int i = 0;
            int gameSumHorizontal = 0; //sum used to verify if a line has 2 O and an empty space (0 + 0 - 10 = -10)
            int gameSumVertical = 0;
            int gameSumDiagonal = 0;
            bool possibleWin = false;

            //Check horizontal and vertical win conditions
            while (i < 3 && possibleWin == false)
            {
                gameSumHorizontal = _gameState[3 * i] + _gameState[3 * i + 1] + _gameState[3 * i + 2];
                gameSumVertical = _gameState[i] + _gameState[i + 3] + _gameState[i + 6];

                if (gameSumHorizontal == -10)
                {
                    //set possibleWin to true to exit while
                    possibleWin = true;
                    //find which of the three moves is possible
                    if (_gameState[3 * i] == -10)
                        position = 3 * i;
                    else if (_gameState[3 * i + 1] == -10)
                        position = 3 * i + 1;
                    else
                        position = 3 * i + 2;
                }
                if (gameSumVertical == -10)
                {
                    //set possibleWin to true to exit while
                    possibleWin = true;
                    //find which of the three moves is possible
                    if (_gameState[i] == -10)
                        position = i;
                    else if (_gameState[i + 3] == -10)
                        position = i + 3;
                    else
                        position = i + 6;
                }
                i++;
            }

            //Check for diagonal win conditions
            gameSumDiagonal = _gameState[0] + _gameState[4] + _gameState[8];
            if (gameSumDiagonal == -10)
            {
                if (_gameState[0] == -10)
                    position = 0;
                else if (_gameState[4] == -10)
                    position = 4;
                else
                    position = 8;
            }
            
            gameSumDiagonal = _gameState[2] + _gameState[4] + _gameState[6];
            if (gameSumDiagonal == -8)
            {
                if (_gameState[2] == -10)
                    position = 2;
                else if (_gameState[4] == -10)
                    position = 4;
                else
                    position = 6;
            }
            
            return position;
        }

        /// <summary>
        /// Finds and returns an empty adjacent position to the position specified in parameters. Returns -10 if there are none
        /// </summary>
        /// <param name="position"></param>
        /// <returns>Empty adjacent position [0,8]</returns>
        public int FindEmptyAdjacentPosition(int position)
        {
            int positionToPlay = -10;

            //Center is always played when this function is called therefore we don't consider it as a possible positionToPlay
            switch(position)
            {
                case 0: //try 1 or 3
                    if (_gameState[1] == -10)
                        positionToPlay = 1;
                    else
                        positionToPlay = 3;
                    break;
                case 1: //try 0 or 2
                    if (_gameState[0] == -10)
                        positionToPlay = 0;
                    else
                        positionToPlay = 2;
                    break;
                case 2: //try 1 or 5
                    if (_gameState[1] == -10)
                        positionToPlay = 1;
                    else
                        positionToPlay = 5;
                    break;
                case 3: //try 0 or 6
                    if (_gameState[0] == -10)
                        positionToPlay = 0;
                    else
                        positionToPlay = 6;
                    break;
                case 4: //try 1 or 3 or 5 or 7
                    if (_gameState[1] == -10)
                        positionToPlay = 1;
                    else if (_gameState[3] == -10)
                        positionToPlay = 3;
                    else if (_gameState[5] == -10)
                        positionToPlay = 5;
                    else 
                        positionToPlay = 7;
                    break;
                case 5: //try 2 or 8
                    if (_gameState[2] == -10)
                        positionToPlay = 2;
                    else
                        positionToPlay = 8;
                    break;
                case 6: //try 3 or 7
                    if (_gameState[3] == -10)
                        positionToPlay = 3;
                    else
                        positionToPlay = 7;
                    break;
                case 7: //try 6 or 8
                    if (_gameState[6] == -10)
                        positionToPlay = 6;
                    else
                        positionToPlay = 8;
                    break;
                case 8: //try 5 or 7
                    if (_gameState[5] == -10)
                        positionToPlay = 5;
                    else
                        positionToPlay = 7;
                    break;
                default:
                    break;
            }

            return positionToPlay;
        }


    }
}
