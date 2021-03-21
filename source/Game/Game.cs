using Frankenweenie;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Game.Editor;
using ImMonoGame;

namespace Game
{
    public class Game : Scene
    {
        public States State = States.Editor;
        public float LevelTime;
        public Texture2D Background;
        private StateMachine StateMachine;
        private Camera Camera = new Camera();
        public List<Hitbox> Solids = new List<Hitbox>();
        public LevelEditor LevelEditor;
        public ImguiComponent ImGui;

        public enum States
        {
            Playing,
            Paused,
            Editor
        };
        
        private void Load(string path)
        {
            //string filepath = Engine.Config.AssetDirectory + "/" + path;
            var data = LevelData.Load(path);
            foreach(EntityData entityData in data.EntityData)
            {
                Entity entity = Factory.Create(entityData.Name);
                entity.position = entityData.Position;
                World.Add(entity);
            }
        }

        protected override void Initialize()
        {
            Engine.RenderTarget.Scale = new Vector2(4f, 4f);
            LevelEditor = new LevelEditor();
            Load(@"Assets/Levels/test.gre");
            ImGui = new ImguiComponent(Engine.Device, Engine.Instance);
            ImGui.Initialize();
            ImGui.Elements.Add(new ImGuiDemo(ImGui.ImGuiTexture).Main);
            base.Initialize();
        }

        protected override void Update()
        {
            switch(State)
            {
                case States.Playing:
                    LevelEditor.End();
                    base.Update();
                    break;
                case States.Editor:
                    LevelEditor.Update();
                    break;
                case States.Paused:
                    break;
            }
        }

        protected override void Render()
        {
            base.Render();
            if (State == States.Editor)
                LevelEditor.Render();
            ImGui.Draw(Engine.GameTime);
        }

    }
}
