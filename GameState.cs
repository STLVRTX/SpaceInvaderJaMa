using Bib.Bg.Xna2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaderJaMa
{
    class GameState
    {
        #region Fields
        private static string currentGameState;
        #endregion

        #region Properties
        public static string CurrentGameState
        {
            get { return currentGameState; }
            set
            {
                switch (value)
                {
                    case "Menu": currentGameState = "Menu"; break;
                    case "Game": currentGameState = "Game"; break;
                    case "Paused": currentGameState = "Paused"; break;
                    case "Game Over": currentGameState = "Game Over"; break;
                }
            }
        }
        #endregion

        #region Constructors
        public GameState()
        {
            currentGameState = "Menu";
        }
        #endregion
    }
}
