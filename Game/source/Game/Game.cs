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

        public static void Load(string path)
        {
            World.Clear();
            OgmoLevel Level = Content.LoadOgmo($"levels/{path}");
            var tileLayer = Level["Solids"];
            var tileLayerData = tileLayer.GridToTileLayer();     
            global::Game.Level.Tiles = new Tilemap(tileLayerData, tileLayer.gridCellsX, tileLayer.gridCellsY);
            for (int x = 0; x < global::Game.Level.Tiles.Width; x++)
                for (int y = 0; y < global::Game.Level.Tiles.Height; y++)
                {
                    if (global::Game.Level.Tiles.Data[x + y * global::Game.Level.Tiles.Width] > -1)
                        global::Game.Level.Solids.Add(new Hitbox(x * 8, y * 8, 8, 8));
                }

            var background = Level["Background"];
            global::Game.Level.BgTiles = new Tilemap(background.GridToTileLayer(), background.gridCellsX, background.gridCellsY);
            var entityLayer = Level["Entities"];

            foreach(OgmoEntity ogmoEntity in entityLayer.entities)
            {
                Entity Entity = EntityManager.Create(ogmoEntity.name, new Parameters(ogmoEntity));
                Entity.Position = new Vector2(ogmoEntity.x, ogmoEntity.y);
                World.Add(Entity);
            }

            Autotiler.Tile(global::Game.Level.Tiles);
            Level = null;
            tileLayer = null;
            tileLayerData = null;
            entityLayer = null;
            background = null;
          
        }

        protected override void Initialize()
        {
            Engine.RenderTarget.Scale = new Vector2(4f, 4f);
            Window.Size(320 * 4, 180 * 4);
            ImGuiLayer.add<CommandPrompt>();
            Load("1.json");
            base.Initialize();
        }

        Entity Player;

        protected override void Load()
        {
            Tileset = new Tileset("graphics/tileset.png", 8, 8 );
            BgTile = Content.LoadTexture("graphics/bg_tile.png");
            Level.Tiles.LayerDepth = 0.2f;
            Level.BgTiles.LayerDepth = 0.1f;
            Camera.Position = Vector2.One;
            
        }


        protected override void End()
        {
            
        }


        Vector2 GetCameraInput()
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

        protected override void Update()
        {
            if (Input.Reload.Released)
                Load("1.json");

            base.Update();
        }

        protected override void Render()
        {
            Engine.ClearColor(Color.Black);
            //Level.BgTiles.Render(BgTile);
            base.Render();
            //Level.Tiles.Render(Tileset);
        }

    }
}
