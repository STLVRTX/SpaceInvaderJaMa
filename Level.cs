using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaderJaMa
{
    class Level
    {
        #region Properties
        public static Invader[,] Enemies { get; set; }
        public Game Game { get; set; }
        public PlayerShip PlayerShip { get; set; }
        public List<Invader> Invaders { get; set; }
        public List<Barrier> Barriers { get; set; }
        public int[] Size { get; private set; }
        public List<Invader> ShootingInvaders { get; set; }
        public List<Shot> InvaderShots { get; set; }
        #endregion

        #region Constructor
        public Level(Game game)
        {
            Game = game;
            Size = new int[] { 11, 5 };
            Enemies = new Invader[Size[0], Size[1]];
            Invaders = new List<Invader>();
            Barriers = new List<Barrier>();
            ShootingInvaders = new List<Invader>();
            InvaderShots = new List<Shot>();
            CreatePlayerShip();
            CreateInvaders();
            CreateBarriers();
            FindLowestInvaderRow();
        }
        #endregion

        #region Methods
        private PlayerShip CreatePlayerShip()
        {
            PlayerShip = new PlayerShip(Game, "PlayerShip", Game.Content.Load<Texture2D>("Ship"), this);
            Game.Components.Add(PlayerShip);
            return PlayerShip;
        }
        private void CreateInvaders()
        {
            for (int y = 0; y < Size[1]; y++)
            {
                for (int x = 0; x < Size[0]; x++)
                {
                    switch (y)
                    {
                        case 4: Enemies[x, y] = new Invader(Game, "InvaderA", Game.Content.Load<Texture2D>("InvaderA_00"), new Vector2(x, y), this); break;
                        case 3: Enemies[x, y] = new Invader(Game, "InvaderA", Game.Content.Load<Texture2D>("InvaderA_00"), new Vector2(x, y), this); break;
                        case 2: Enemies[x, y] = new Invader(Game, "InvaderB", Game.Content.Load<Texture2D>("InvaderB_00"), new Vector2(x, y), this); break;
                        case 1: Enemies[x, y] = new Invader(Game, "InvaderB", Game.Content.Load<Texture2D>("InvaderB_00"), new Vector2(x, y), this); break;
                        case 0: Enemies[x, y] = new Invader(Game, "InvaderC", Game.Content.Load<Texture2D>("InvaderC_00"), new Vector2(x, y), this); break;
                    }
                    Invaders.Add(Enemies[x,y]);
                    Game.Components.Add(Enemies[x,y]);
                }
            }
        }
        private void CreateBarriers()
        {
            for (int i = 0; i < 4; i++)
            {
                Barrier b = new Barrier(Game, "Barrier" + i, Game.Content.Load<Texture2D>("Barrier"), new Vector2(((i + 1) * 98) + i * 10, 500), this);
                Game.Components.Add(b);
                Barriers.Add(b);
            }
        }
        public void CheckMovement()
        {
            foreach (Invader i in Invaders)
            {
                if (i.Position.X >= Game.GraphicsDevice.Viewport.Width * 0.88f && Invader.DirRight)
                {
                    foreach (Invader ii in Invaders)
                    {
                        ii.Position = new Vector2(ii.Position.X, ii.Position.Y + (ii.Size.Y + Invader.Spacing));
                    }
                    Invader.DirRight = false;
                    break;
                }

                if (i.Position.X <= Game.GraphicsDevice.Viewport.Width * 0.07f && !Invader.DirRight)
                {
                    foreach (Invader ii in Invaders)
                    {
                        ii.Position = new Vector2(ii.Position.X, ii.Position.Y + (ii.Size.Y + Invader.Spacing));
                    }
                    Invader.DirRight = true;
                    break;
                }
            }

        }
        public void FindLowestInvaderRow()
        {
            ShootingInvaders.Clear();

            for(int i = 0; i < Size[0]; i++)
            {
                for(int j = Size[1]-1; j > 0; j--)
                {
                    if(Enemies[i,j] != null && !ShootingInvaders.Contains(Enemies[i,j]) && Invaders.Contains(Enemies[i,j]))
                    {
                        ShootingInvaders.Add(Enemies[i,j]);
                        break;
                    }
                }
            }
        }
        #endregion
    }
}

