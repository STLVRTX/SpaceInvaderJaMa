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
    class Shot : BasicSpriteComponent
    {
        #region Properties
        public float Speed { get; set; }
        public Level Level { get; set; }
        #endregion

        #region Constructor
        public Shot(Game game, string name, Texture2D image, Vector2 position, Level level) : base(game, name, image)
        {
            Speed = 200f;
            Position = new Vector2(position.X, position.Y);
            Level = level;
        }
        #endregion

        #region Methods
        public Shot CopyShot()
        {
            return (Shot)this.MemberwiseClone();
        }

        public override void Update(GameTime gameTime)
        {
            if (Level.InvaderShots.Contains(this))
            {
                if (Position.Y >= 666)
                {
                    Level.InvaderShots.Remove(this);
                    Game.Components.Remove(this);
                }
                Position -= Up * (float)gameTime.ElapsedGameTime.TotalSeconds * Speed;
            }
        }
        #endregion
    }
}

