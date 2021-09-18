using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaderJaMa.Model;

namespace SpaceInvaderJaMa
{
    public class GameController : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Level level;

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

            base.Draw(gameTime);
        }
    }
}
