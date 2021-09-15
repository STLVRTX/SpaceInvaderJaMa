﻿using Bib.Bg.Xna2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaderJaMa.Model
{
    class Invader : BasicSpriteComponent
    {
        #region Fields
        private static float speed = 15;
        private Vector2 internPos;
        private static float spacing = 15;
        #endregion

        #region Properties
        public static float Speed
        {
            get { return speed;}
            set 
            {
                if (value > 0)
                    speed = value;
                else
                    throw new Exception("Invaders have a negative speed value or no speed!");

            }

        }

        public Vector2 InternPos
        {
            get {  return internPos; }
            set
            {
                if (value.X >= 0 && value.Y >= 0)
                    internPos = value;
                else
                    throw new Exception("Invalid invader position");
            }
        }

        public static Vector2 StartPos {  get; set; }
        public static bool DirRight {  get; set; }
        #endregion

        #region Constructor
        public Invader(Game game, string name, Texture2D image, Vector2 internPos) : base(game, name, image)
        {
            Speed = speed;
            InternPos = internPos;
            StartPos = new Vector2(game.GraphicsDevice.Viewport.Width * 0.1f, game.GraphicsDevice.Viewport.Height * 0.3f);
            DirRight = true;
            CalcPosition();
        }
        #endregion

        #region Methods
        private void CalcPosition()
        {
            float x = StartPos.X + (internPos.X * (Size.X + spacing));
            float y = StartPos.Y + (internPos.Y * (Size.Y + spacing));

            Position = new Vector2(x, y);
        }

        public override void Update(GameTime gameTime)
        {
            MoveInvader(gameTime);
            CheckMovement(gameTime);

        }

        private void MoveInvader(GameTime gameTime)
        {
            if (DirRight)
                Position += Right * (float)gameTime.ElapsedGameTime.TotalSeconds * Speed;
            else
                Position -= Right * (float)gameTime.ElapsedGameTime.TotalSeconds * Speed;
        }
        private void CheckMovement(GameTime gameTime)
        {
            
        }


        #endregion
    }
}
