using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaderJaMa;
using System;
using System.IO;

namespace SpaceInvaderJaMa
{
    public class GameController : Game
    {
        #region Fields
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Level level;
        private SpriteFont font;
        private Random rnd;
        #endregion

        #region Properties
        public static int Score { get; set; }
        public float InvaderShotDelay { get; set; }
        public bool InvaderCanShoot { get; set; }
        public static float ShotDelay { get; set; }
        public static SoundEffect InvaderHit { get; set; }
        public static SoundEffect PlayerHit { get; set; }
        public static SoundEffect InvaderBullet { get; set; }
        public static SoundEffect PlayerBullet { get; set; }
        #endregion

        #region Constructors
        public GameController()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 700;
            Content.RootDirectory = "Content";
        }
        #endregion

        #region Methods
        protected override void Initialize()
        {
            GameState.CurrentGameState = "Menu";
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("GameFont");
            level = new Level(this);
            ShotDelay = 1000;
            InvaderShotDelay = ShotDelay;
            InvaderCanShoot = true;
            rnd = new Random();
            InvaderHit = Content.Load<SoundEffect>("InvaderHit");
            PlayerHit = Content.Load<SoundEffect>("ShipHit");
            InvaderBullet = Content.Load<SoundEffect>("InvaderBullet");
            PlayerBullet = Content.Load<SoundEffect>("ShipBullet");
            SoundEffect.MasterVolume = 0.1f;
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            switch (GameState.CurrentGameState)
            {
                case "Menu":
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        GameState.CurrentGameState = "Game";
                    break;

                case "Game":
                    if (level.Invaders.Count == 0) { GameState.CurrentGameState = "Game Over"; }
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        GameState.CurrentGameState = "Paused";
                    }
                    level.CheckMovement();
                    InvaderFire(gameTime);
                    break;

                case "Paused":
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        //Exit();
                        if (Keyboard.GetState().IsKeyDown(Keys.Space))
                            GameState.CurrentGameState = "Game";
                    break;

                case "Game Over":
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        Exit();
                    }
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            if (GameState.CurrentGameState == "Menu")
                spriteBatch.DrawString(font, "Press Space to Play!", new Vector2(200, 325), Color.White);
            else if (GameState.CurrentGameState == "Game")
            {
                switch (level.PlayerShip.Hp)
                {
                    case 3: spriteBatch.DrawString(font, "HP " + level.PlayerShip.Hp, new Vector2(75, 650), Color.White); break;
                    case 2: spriteBatch.DrawString(font, "HP " + level.PlayerShip.Hp, new Vector2(75, 650), Color.Yellow); break;
                    case 1: spriteBatch.DrawString(font, "HP " + level.PlayerShip.Hp, new Vector2(75, 650), Color.Red); break;
                }
                spriteBatch.DrawString(font, "Score " + Score, new Vector2(75, 30), Color.White);
                base.Draw(gameTime);
            }
            else if (GameState.CurrentGameState == "Paused")
            {
                spriteBatch.DrawString(font, "Score " + Score, new Vector2(75, 30), Color.White);
                spriteBatch.DrawString(font, "Press Space to Continue", new Vector2(200, 325), Color.White);
            }
            else if (GameState.CurrentGameState == "Game Over")
                spriteBatch.DrawString(font, "Game Over!", new Vector2(250, 400), Color.White);
            spriteBatch.End();
        }

        public void InvaderFire(GameTime gameTime)
        {
            if (InvaderCanShoot)
            {
                InvaderShot();
                InvaderShotDelay = ShotDelay;
                InvaderCanShoot = false;
            }
            else
                InvaderShotDelay -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (InvaderShotDelay <= 0)
            {
                InvaderCanShoot = true;
            }
        }
        public void InvaderShot()
        {
            if (level.ShootingInvaders.Count == 0)
                return;
            int random = rnd.Next(0, level.ShootingInvaders.Count);
            level.ShootingInvaders[random].Shoot();
        }
        #endregion
    }
}
