using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaderJaMa;

namespace SpaceInvaderJaMa
{
    public class GameController : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Level level;
        private SpriteFont font;
        private static int score = 0;

        public static int Score { get; set; }

        public GameController()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";
        }

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
                    if(Level.invaders.Count == 0) { GameState.CurrentGameState = "Game Over"; }
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        GameState.CurrentGameState = "Paused";
                    level.CheckMovement(gameTime);
                    break;
                
                case "Paused":
                    //if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        //Exit();
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        GameState.CurrentGameState = "Game";
                    break;
               
                case "Game Over": break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            if (GameState.CurrentGameState == "Menu")
                spriteBatch.DrawString(font, "Press Space to Play!", new Vector2(200, 500), Color.White);
            else if (GameState.CurrentGameState == "Game" || GameState.CurrentGameState == "Paused")
                spriteBatch.DrawString(font, "Score " + Score, new Vector2(100, 100), Color.White);
            else if (GameState.CurrentGameState == "Game Over")
                spriteBatch.DrawString(font, "Game Over!", new Vector2(200, 500), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
