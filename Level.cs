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
        public static Invader[] Enemies { get; set; }
        public Game Game { get; set; }
        public PlayerShip PlayerShip { get; set; }
        public List<Invader> Invaders { get; set; }
        public List<Barrier> Barriers { get; set; }
        public int[] Size { get; private set; }
        public List<Invader> ShootingInvaders { get; set; }
        public List<Shot> InvaderShots { get; set; }
        public float InvaderShotDelay {  get; set; }
        public bool InvaderCanShoot {  get; set; }
        #endregion

        #region Constructor
        public Level(Game game)
        {
            Game = game;
            Size = new int[] { 11, 5 };
            Enemies = new Invader[Size[0] * Size[1]];
            Invaders = new List<Invader>();
            Barriers = new List<Barrier>();
            ShootingInvaders = new List<Invader>();
            InvaderShots = new List<Shot>();
            InvaderShotDelay = 500;
            InvaderCanShoot = true;
            CreatePlayerShip();
            CreateInvaders();
            CreateBarriers();
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
            int index;
            for (int y = 0; y < Size[1]; y++)
            {
                for (int x = 0; x < Size[0]; x++)
                {
                    index = x + (y * Size[0]);
                    Enemies[index] = new Invader(Game, "InvaderA", Game.Content.Load<Texture2D>("InvaderA_00"), new Vector2(x, y), this);
                    Invaders.Add(Enemies[index]);
                    Game.Components.Add(Enemies[index]);
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
            for (int i = 0; i < Size[0]; i++)
            {
                for (int j = Size[1]; j > 0; j--)
                {
                    if(Enemies[i+j] != null)
                    {
                        ShootingInvaders.Add(Enemies[i + j]);
                        break;
                    }
                }
            }
        }


        public void ShootDelay(GameTime gameTime)
        {
            if (InvaderCanShoot)
            {
                InvaderShot();
                InvaderShotDelay = 5000;
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
            if (ShootingInvaders.Count == 0)
                return;
            int random = new Random().Next(0, ShootingInvaders.Count - 1);
            ShootingInvaders[random].Shoot();
        }



        #endregion
    }
}

