﻿using Frankenweenie;
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

        public static void Load(string path)
        {
            World.Clear();
            OgmoLevel level = new OgmoLevel(TitleContainer.OpenStream($"assets/levels/{path}"));
            var tileLayer = level["Solids"];
            var tileLayerData = level.GridToTileLayer(tileLayer);     
            Level.Tiles = new Tilemap(tileLayerData, tileLayer.gridCellsX, tileLayer.gridCellsY);
            for (int x = 0; x < Level.Tiles.Width; x++)
                for (int y = 0; y < Level.Tiles.Height; y++)
                {
                    if (Level.Tiles.Data[x + y * Level.Tiles.Width] > -1)
                        Level.Solids.Add(new Hitbox(x * 8, y * 8, 8, 8));
                }
            var background = level["Background"];
            Level.BgTiles = new Tilemap(level.GridToTileLayer(background), background.gridCellsX, background.gridCellsY);
            var entityLayer = level["Entities"];

            foreach(OgmoEntity Entity in entityLayer.entities)
            {
                var e = EntityManager.Create(Entity.name);
                e.Position = new Vector2(Entity.x, Entity.y);
                var mover = e.Get<Mover>();
                if(mover != null)
                    Level.Actors.Add(mover.Hitbox);
                World.Add(e);
            }

            Tiler.Tile(Level.Tiles);

            level = null;
            tileLayer = null;
            tileLayerData = null;
            entityLayer = null;
            background = null;
          
        }

        protected override void Initialize()
        {
            Engine.RenderTarget.Scale = new Vector2(4f, 4f);
            Engine.Size(320 * 4, 180 * 4);
            if (ImGuiLayer.get<CommandPrompt>() == null)
                ImGuiLayer.add<CommandPrompt>();
            Load("one.json");
            base.Initialize();
        }

        protected override void Load()
        {
            Tileset = new Tileset("graphics/stone.png", 8, 8 );
            BgTile = Content.Texture("graphics/bg_tile.png");
        }

        protected override void End()
        {
            
        }


        protected override void Update()
        {
            if (Input.Reload.Released)
                Load("one.json");

            base.Update();
        }

        protected override void Render()
        {
            Engine.ClearColor(Color.Black);
            Level.BgTiles.Render(BgTile);
            base.Render();
            Level.Tiles.Render(Tileset);
        }

    }
}
