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
    public class Shot : BasicSpriteComponent
    {
        public float Speed
        {
            get; set;
        }

        public Shot(Game game, string name, Texture2D image) : base(game, name, image)
        {
            Speed = 5f;
        }

        public override void Update(GameTime gameTime)
        {
            Position += Up * (float)gameTime.ElapsedGameTime.TotalSeconds * Speed;
            if (Position.Y < 0) { this.UnloadContent(); }
        }
    }
}

