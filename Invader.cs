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
    class Invader : BasicSpriteComponent
    {
        #region Fields
        private Vector2 internPos;
        private static float speed;
        private float tempTime = 0;
        private bool animSwitch = true;
        private float invaderShotDelay = 500;
        private bool invaderCanShoot = true;
        private Level level;
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
        public static float Spacing { get; set; }
        public static Vector2 StartPos {  get; set; }
        public static bool DirRight {  get; set; }
        public static float AnimationDelay {  get; set; }
        public Level Level {  get; set; }
        #endregion

        #region Constructor
        public Invader(Game game, string name, Texture2D image, Vector2 internPos, Level level) : base(game, name, image)
        {
            Level = level;
            InternPos = internPos;
            AnimationDelay = 200;
            Speed = 15;
            Spacing = 15;
            StartPos = new Vector2(game.GraphicsDevice.Viewport.Width * 0.1f, game.GraphicsDevice.Viewport.Height * 0.3f);
            DirRight = true;
            CalcPosition();
        }
        #endregion

        #region Methods
        private void CalcPosition()
        {
            float x = StartPos.X + (internPos.X * (Size.X + Spacing));
            float y = StartPos.Y + (internPos.Y * (Size.Y + Spacing));

            Position = new Vector2(x, y);
        }

        public override void Update(GameTime gameTime)
        {
            if(GameState.CurrentGameState == "Game")
            {
                MoveInvader(gameTime);
                Animation(gameTime);
                DetectCollision();
                if (invaderCanShoot)
                {
                    InvaderShot();
                    invaderShotDelay = 500;
                    invaderCanShoot = false;
                }   
                else { invaderShotDelay -= (float) gameTime.ElapsedGameTime.TotalMilliseconds; }
                if(invaderShotDelay <= 0)
                {
                    invaderCanShoot = true;
                }

                foreach(Shot s in invaderShots.ToArray())
                {
                    if (s.OutOfFrame())
                    {
                        invaderShots.Remove(s);
                        Game.Components.Remove(s);
                    }
                    s.Position -= Up * (float) gameTime.ElapsedGameTime.TotalSeconds * 3f;
                }
            }
        }

        private void MoveInvader(GameTime gameTime)
        {
            if (DirRight)
                Position += Right * (float) gameTime.ElapsedGameTime.TotalSeconds * Speed;
            else
                Position -= Right * (float) gameTime.ElapsedGameTime.TotalSeconds * Speed;
        }

        private void Animation(GameTime gameTime)
        {
            tempTime += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
            
            if(tempTime >= AnimationDelay)
            {
                string name = Texture.Name.Substring(0, Texture.Name.Length - 1);
                if (animSwitch == true)
                {
                    Texture = Game.Content.Load<Texture2D>(name + "1");
                    animSwitch = false;
                }
                else
                {
                    Texture = Game.Content.Load<Texture2D>(name + "0");
                    animSwitch = true;
                }

                tempTime -= tempTime;
            }
        }

        public void DetectCollision()
        {
            foreach(Shot s in PlayerShip.bullets.ToArray())
            {
                if (new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Size.X, this.Size.Y).Intersects(new Rectangle((int)s.Position.X, (int)s.Position.Y, s.Size.X, s.Size.Y)))
                {
                    PlayerShip.bullets.Remove(s);
                    Level.Invaders.Remove(this);
                    shootingInvaders.Remove(this);
                    Game.Components.Remove(s);
                    Game.Components.Remove(this);
                    FindLowestInvaderRow();
                    GameController.Score += 50;
                }
            }
        }

        public void Shoot()
        {
            Shot s = new Shot(Game, "Shot", Game.Content.Load<Texture2D>("InvaderShot"), CenterPosition);
            Game.Components.Add(s);
            invaderShots.Add(s);
        }

        public void InvaderShot()
        {
            if (shootingInvaders.Count == 0)
                return;
            int random = new Random().Next(0, shootingInvaders.Count-1);
            shootingInvaders[random].Shoot();
        }
        #endregion
    }
}
