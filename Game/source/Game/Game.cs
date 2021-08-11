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
        private void Follow(Entity entity)
        {
            if(last != entity.Position)
            {
                last = Vector2.Zero;
            }
            
            Camera.Position += last;
        }
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
            ResizeAction.Invoke();
            Window.ResizeActions.Add(ResizeAction);
            ImGuiLayer.Add<CommandPrompt>();
            ImGuiLayer.Add<Logger>();

            LevelLoader.Load("area one/a1.json");
            Player = World.All<Player>()[0].Entity;
            Logger.Log(World.All<Hitbox>().Count);
            base.Initialize();
        }

        protected override void Load()
        {
            Engine.Color(Color.Black);
            Tileset = new Tileset("graphics/tileset.png", 8, 8);
            BgTile = Content.LoadTexture("graphics/bg_tile.png");
            Level.Tiles.LayerDepth = 100f / Sprite.MaxLayerDepth;
            Level.BgTiles.LayerDepth = 10f / Sprite.MaxLayerDepth;
            Camera.Position = Vector2.One;

        }


        protected override void End()
        {
            
        }

        Vector2 GetCameraInput
        {
            get
            {
                Vector2 input;
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    input.Y = -1;
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    input.Y = 1;
                else
                    input.Y = 0;

                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    input.X = -1;
                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    input.X = 1;
                else
                    input.X = 0;
                return input;
            }
        }

        protected override void Update()
        {
            
            Follow(Player);
            Engine.Transform = Camera.Transform;
            base.Update();
        }

        protected override void Render()
        {
            Level.BgTiles.Render(BgTile);
           
            World.Render();
            Level.Tiles.Render(Tileset);
            DebugDraw.Draw();
        }

    }
}