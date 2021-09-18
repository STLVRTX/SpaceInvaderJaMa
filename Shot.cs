using Bib.Bg.Xna2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaderJaMa.Model
{
    public class Shot : BasicSpriteComponent
    {
        public float Speed
        {
            get; set;
        }

        public Shot(Game game, string name, Texture2D image, Vector2 shipPosition) : base(game, name, image)
        {
            Speed = 200f;
            Position = new Vector2(shipPosition.X, shipPosition.Y-12);
        }

        public bool OutOfFrame()
        {
            if(Position.Y <= 200) { Game.Components.Remove(this); return true;}
            return false;
        }
    }
}

