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
        public int Hp { get; set; }


        public Barrier(Game game, string name, Texture2D image, Vector2 pos) : base(game, name, image)
        {
            Position = pos;
            Scale = new Vector2(2, 2);
            Hp = 5;
        }

        public override void Update(GameTime gameTime)
        {
            if(Hp <= 0)
            {
                Level.barriers.Remove(this);
                Game.Components.Remove(this);
            }
            DetectCollision();
        }

        public void DetectCollision()
        {
            foreach (Barrier b in Level.barriers)
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
                    }
                }
            }
        }
    }
}
