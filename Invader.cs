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
        public bool AnimSwitch {  get; set; }
        public float TempTime {  get; set; }
        public Shot InvaderShot { get; set; }
        #endregion

        #region Constructor
        public Invader(Game game, string name, Texture2D image, Vector2 internPos, Level level) : base(game, name, image)
        {
            Level = level;
            InternPos = internPos;
            AnimationDelay = 400;
            Speed = 15;
            Spacing = 15;
            StartPos = new Vector2(game.GraphicsDevice.Viewport.Width * 0.15f, game.GraphicsDevice.Viewport.Height * 0.1f);
            DirRight = true;
            InvaderShot = new Shot(Game, "Shot", Game.Content.Load<Texture2D>("InvaderShot"), Position, Level);
            AnimSwitch = true;
            TempTime = 0;
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
                InvaderShot.Position = Position;
                MoveInvader(gameTime);
                Animation(gameTime);
                DetectCollision();
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
            TempTime += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
            
            if(TempTime >= AnimationDelay)
            {
                string name = Texture.Name.Substring(0, Texture.Name.Length - 1);
                if (AnimSwitch == true)
                {
                    Texture = Game.Content.Load<Texture2D>(name + "1");
                    AnimSwitch = false;
                }
                else
                {
                    Texture = Game.Content.Load<Texture2D>(name + "0");
                    AnimSwitch = true;
                }

                TempTime -= TempTime;
            }
        }
        private void DetectCollision()
        {
            foreach(Shot s in PlayerShip.Bullets.ToArray())
            {
                if (new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y).Intersects(new Rectangle((int)s.Position.X, (int)s.Position.Y, s.Size.X, s.Size.Y)))
                {
                    GameController.InvaderHit.Play(0.1f, 0, 0);
                    Level.ShootingInvaders.Remove(this);
                    PlayerShip.Bullets.Remove(s);
                    Level.Invaders.Remove(this);
                    Game.Components.Remove(s);
                    Game.Components.Remove(this);
                    Level.FindLowestInvaderRow();
                    switch (Level.Invaders.Count)
                    {
                        case 25: Speed += 3; break;
                        case 15: Speed += 5; break;
                        case 5: Speed += 7; break;
                        case 3: Speed += 10; break;
                        case 1: Speed += 12; break;
                    }
                    switch (Name)
                    {
                        case "InvaderA": GameController.Score += 50; break; 
                        case "InvaderB": GameController.Score += 100; break;
                        case "InvaderC": GameController.Score += 250; break;
                    }
                    switch (GameController.Score)
                    {
                        case 500: GameController.ShotDelay -= 50; break;
                        case 1000: GameController.ShotDelay -= 50; break;
                        case 1500: GameController.ShotDelay -= 50; break;
                        case 2000: GameController.ShotDelay -= 50; break;
                        case 2500: GameController.ShotDelay -= 50; break;
                        case 3000: GameController.ShotDelay -= 50; break;
                        case 3500: GameController.ShotDelay -= 100; break;
                        case 4000: GameController.ShotDelay -= 100; break;
                    }
                }
            }
        }
        public void Shoot()
        {
            Shot s = new Shot(Game, "Shot", Game.Content.Load<Texture2D>("InvaderShot"), new Vector2(Position.X + (Size.X/2), Position.Y + (Size.Y/2)), Level);
            Game.Components.Add(s);
            Level.InvaderShots.Add(s);
            GameController.InvaderBullet.Play(0.05f, 0, 0);
        }
        #endregion
    }
}
