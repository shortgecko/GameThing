using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frankenweenie
{
    public class Profile
    {
        public static int Height => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        public static int Width => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

    }
}
