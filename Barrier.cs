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
    class Barrier : BasicSpriteComponent
    {
        #region Properties
        public int Hp { get; set; }
        public Level Level {  get; set; }
        #endregion

        #region Constructor
        public Barrier(Game game, string name, Texture2D image, Vector2 pos, Level level) : base(game, name, image)
        {
            Position = pos;
            Level = level;
            Scale = new Vector2(2, 2);
            Hp = 5;
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            if(Hp <= 0)
            {
                Level.Barriers.Remove(this);
                Game.Components.Remove(this);
            }
            DetectCollision();
        }
        public void DetectCollision()
        {
            foreach (Barrier b in Level.Barriers)
            {
                foreach (Shot s in PlayerShip.bullets.ToArray())
                {
                    if (new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y).Intersects(new Rectangle((int)s.Position.X, (int)s.Position.Y, s.Size.X, s.Size.Y)))
                    {
                        PlayerShip.bullets.Remove(s);
                        Game.Components.Remove(s);
                    } 
                }
                foreach (Shot s in Invader.invaderShots.ToArray())
                {
                    if (new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y).Intersects(new Rectangle((int)s.Position.X, (int)s.Position.Y, s.Size.X, s.Size.Y)))
                    {
                        Invader.invaderShots.Remove(s);
                        Game.Components.Remove(s);
                        Hp--;
                        return;
                    }
                }
            }
        }
        #endregion
    }
}
