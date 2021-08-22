using Frankenweenie;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Xml;
using IO = System.IO;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Linq;

namespace Game
{
   
    public class Game : Scene
    {
        public float LevelTime;
        private Tileset Tileset;
        private Texture2D BgTile;
        private Camera Camera = new Camera();
        private Entity Player;
        

        Vector2 last;

        private Action ResizeAction = () =>
        {
            Vector2 scale = new Vector2()
            {
                X = (int)Window.Width / 320,
                Y = (int)Window.Width / 320,
            };

            Logger.Log($"scale x {scale.X}");
            Logger.Log($"scale y {scale.Y}");

            Vector2 screenCenter = new Vector2()
            {
                X = (Window.Width - (320 * scale.X)) / 2,
                Y = (Window.Height - (180 * scale.Y)) / 2,
            };

            
            Engine.RenderTarget.Scale = scale;
            Engine.RenderTarget.Position = screenCenter;

            if (Window.Width > Profile.Width || Window.Height > Profile.Height)
            {
                Point DesktopPosition = new Point((Profile.Width - Window.Width) / 2, (Profile.Height - Window.Height) / 2);
                Window.SetPosition(DesktopPosition);
            }
        };

        protected override void Initialize()
        {
            Engine.Fullscreen(true);
            World.CreateCamera();
            ResizeAction.Invoke();
            Window.ResizeActions.Add(ResizeAction);
            Logger.Log(World.All<Hitbox>().Count);
            base.Initialize();
        }




        protected override void Render()
        {           
            World.Render();
        }

    }
}