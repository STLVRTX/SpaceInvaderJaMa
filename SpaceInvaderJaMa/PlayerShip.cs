using Bib.Bg.Xna2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaderJaMa.Model
{
    class PlayerShip : BasicSpriteComponent
    {
        #region Properties
        private float Speed {  get; set; }
        #endregion

        #region Constructor
        public PlayerShip(Game game, string name, Texture2D image) : base(game, name, image)
        {
            Speed = 200;
            Position = new Vector2((int)(game.GraphicsDevice.Viewport.Width * 0.1), (int)(game.GraphicsDevice.Viewport.Height * 0.9));
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            Controls(gameTime);
        }

        private void Controls(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.D))
            {
                if (Position.X >= (Game.GraphicsDevice.Viewport.Width * 0.1) - Size.X && Position.X <= (Game.GraphicsDevice.Viewport.Width * 0.85))
                    Position += Right * (float)gameTime.ElapsedGameTime.TotalSeconds * Speed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.A))
            {
                if (Position.X >= (Game.GraphicsDevice.Viewport.Width * 0.1) && Position.X <= (Game.GraphicsDevice.Viewport.Width * 0.85) + Size.X)
                    Position -= Right * (float)gameTime.ElapsedGameTime.TotalSeconds * Speed;
            }
        }
        #endregion
    }
}
