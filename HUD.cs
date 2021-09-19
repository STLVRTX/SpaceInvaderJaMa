﻿using Bib.Bg.Xna2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaderJaMa
{
    class HUD : Game
    {
        private SpriteFont font;
        private int score = 0;
        public SpriteBatch SpriteBatch { get; set; }
        public Game Game { get; set; }

        public HUD(Game game, SpriteBatch spriteBatch)
        {
            Game = game;
            SpriteBatch = spriteBatch;
        }

        protected override void LoadContent()
        {
            font = Content.Load<SpriteFont>("GameFont");
        }

        protected override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            SpriteBatch.DrawString(font, "Score " + score, new Vector2(100, 100), Color.White);

            SpriteBatch.End();
        }

        protected override void Update(GameTime gameTime)
        {
            score++;
        }
    }
}
