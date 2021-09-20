using Bib.Bg.Xna2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaderJaMa
{
    class PlayerShip : BasicSpriteComponent
    {
        #region Fields
        public static List<Shot> bullets = new List<Shot>();
        public Shot shot;
        #endregion

        #region Properties
        private float Speed { get; set; }
        private float ShotSpeed { get; set; }
        private bool OnCooldown { get; set; }
        private float ShotDelay { get; set; }

        public Level Level { get; set; }
        #endregion

        #region Constructor
        public PlayerShip(Game game, string name, Texture2D image, Level level) : base(game, name, image)
        {
            Speed = 200;
            ShotSpeed = 500;
            Position = new Vector2((int)(game.GraphicsDevice.Viewport.Width * 0.1), (int)(game.GraphicsDevice.Viewport.Height * 0.9));
            OnCooldown = false;
            ShotDelay = 750;
            Level = level;
            shot = new Shot(Game, "Shot", Game.Content.Load<Texture2D>("InvaderShot"), CenterPosition, Level);
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            if (GameState.CurrentGameState == "Game")
            {
                Controls(gameTime);
                foreach (Shot s in bullets.ToArray())
                {
                    if (s.Position.Y <= 50) { bullets.Remove(s); Game.Components.Remove(s); }
                    s.Position += Up * (float)gameTime.ElapsedGameTime.TotalSeconds * ShotSpeed;
                }
                DetectCollision();
            }
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

            if (currentKeyboardState.IsKeyDown(Keys.Space))
            {
                if (!OnCooldown)
                {
                    Shot s = shot.CopyShot();
                    s.Position = new Vector2(CenterPosition.X, CenterPosition.Y - 10);
                    Game.Components.Add(s);
                    bullets.Add(s);
                    OnCooldown = true;
                    ShotDelay = 750;
                }
            }

            if (ShotDelay <= 0)
            {
                OnCooldown = false;
            }
            else
            {
                ShotDelay -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }

        public void DetectCollision()
        {
            foreach (Shot s in Level.InvaderShots.ToArray())
            {
                if (new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Size.X, this.Size.Y).Intersects(new Rectangle((int)s.Position.X, (int)s.Position.Y, s.Size.X, s.Size.Y)))
                {
                    Level.InvaderShots.Remove(s);
                    //reduce hp
                    Game.Components.Remove(s);
                    GameState.CurrentGameState = "Game Over";
                }
            }
        }
        #endregion
    }
}
