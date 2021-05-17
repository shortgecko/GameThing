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

        protected override void Initialize()
        {
            Engine.RenderTarget.Scale = new Vector2(6);
            ImGuiLayer.add<CommandPrompt>();
            LevelLoader.Load("1.json");
            base.Initialize();
        }

        protected override void Load()
        {

            Tileset = new Tileset("graphics/tileset.png", 8, 8);
            BgTile = Content.LoadTexture("graphics/bg_tile.png");
            Level.Tiles.LayerDepth = 1f;
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
            base.Update();
        }

        protected override void Render()
        {
            Engine.Color(Color.Black);
            Level.BgTiles.Render(BgTile);
            World.Render();
            Level.Tiles.Render(Tileset);

            DebugDraw.Draw();
        }

    }
}