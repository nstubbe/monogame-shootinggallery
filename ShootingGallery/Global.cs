using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingGallery
{
    static class Global
    {
        public static GraphicsDeviceManager Graphics;
        public static SpriteBatch Spritebatch;

        public static void Init(GraphicsDeviceManager graphics)
        {
            Graphics = graphics;
        }

    }
}
