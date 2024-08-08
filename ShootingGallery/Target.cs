using System;
using Microsoft.Xna.Framework;

namespace ShootingGallery
{
    internal class Target
    {
        public Vector2 Position;
        public const int Radius = 45;

        public Target()
        {
            RandomizeLocation();
        }

        public void RandomizeLocation()
        {
            var rnd = new Random();

            Position.X = rnd.Next(0, Global.Graphics.PreferredBackBufferWidth);
            Position.Y = rnd.Next(0, Global.Graphics.PreferredBackBufferHeight);
        }
    }
}
