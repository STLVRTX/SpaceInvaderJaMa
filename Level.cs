﻿using Microsoft.Xna.Framework;
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
        private static PlayerShip playerShip;
        private static int[] size = { 11, 5 };
        public static List<Invader> invaders = new List<Invader>();
        #endregion

        #region Properties
        public Invader[] Enemies {  get; set; }
        public Game Game { get; set; }
        public PlayerShip PlayerShip { get; set; }
        #endregion

        #region Constructor
        public Level(Game game)
        {
            Game = game;
            Enemies = new Invader[size[0] * size[1]];
            CreatePlayerShip();
            CreateInvaders();
        }
        #endregion

        #region Methods
        private PlayerShip CreatePlayerShip()
        {
            playerShip = new PlayerShip(Game, "PlayerShip", Game.Content.Load<Texture2D>("Ship"));  
            Game.Components.Add(playerShip);
            return playerShip;
        }

        private void CreateInvaders()
        {
            int index = 0;
            for (int y = 0; y < size[1]; y++)
            {
                for (int x = 0; x < size[0]; x++)
                {
                    index = x + (y * size[0]);
                    Enemies[index] = new Invader(Game, "InvaderA", Game.Content.Load<Texture2D>("InvaderA_00"), new Vector2(x, y));
                    invaders.Add(Enemies[index]);
                    Game.Components.Add(Enemies[index]);
                }
            }
        }

        public void CheckMovement(GameTime gameTime)
        {
            foreach(Invader i in invaders)
            {
                if (i.Position.X >= Game.GraphicsDevice.Viewport.Width * 0.95f && Invader.DirRight)
                {
                    foreach(Invader ii in invaders)
                    {
                        ii.Position = new Vector2(ii.Position.X, ii.Position.Y + (ii.Size.Y + Invader.Spacing));
                    }
                    Invader.DirRight = false;
                    break;
                }

                if (i.Position.X <= Game.GraphicsDevice.Viewport.Width * 0.05f && !Invader.DirRight)
                {
                    foreach(Invader ii in invaders)
                    {
                        ii.Position = new Vector2(ii.Position.X, ii.Position.Y + (ii.Size.Y + Invader.Spacing));
                    }
                    Invader.DirRight = true;
                    break;
                }
            }

        }
        #endregion
    }
}
