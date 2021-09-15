using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaderJaMa.Model
{
    class Level
    {
        #region Fields
        private PlayerShip playerShip;
        private static int[] size = { 11, 5 };
        #endregion

        #region Properties
        public Invader[] Enemies {  get; set; }
        #endregion

        #region Constructor
        public Level(Game game)
        {
            Enemies = new Invader[size[0] * size[1]];
            CreatePlayerShip(game);
            CreateInvaders(game);
        }
        #endregion

        #region Methods
        private void CreatePlayerShip(Game game)
        {
            playerShip = new PlayerShip(game, "PlayerShip", game.Content.Load<Texture2D>("Ship"));
            game.Components.Add(playerShip);
        }

        private void CreateInvaders(Game game)
        {
            int index = 0;
            for (int y = 0; y < size[1]; y++)
            {
                for (int x = 0; x < size[0]; x++)
                {
                    index = x + (y * size[0]);
                    Enemies[index] = new Invader(game, "InvaderA", game.Content.Load<Texture2D>("InvaderA_00"), new Vector2(x, y));
                    game.Components.Add(Enemies[index]);
                }
            }
        }
        #endregion
    }
}

