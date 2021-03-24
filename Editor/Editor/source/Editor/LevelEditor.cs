using Frankenweenie;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game.Editor
{
    public class LevelEditor : Scene
    {
        private enum States
        {
            Placing,
            Removing,
            None
        };

        private States State = States.None;
        private Entity Selected;
        private EditorComponent SelectedData;
        public static LevelData LevelData = new LevelData();
        private Vector2 CameraPosition;
        private float CameraOffset = 2f;
        private Matrix Camera;
        private Vector2 MousePosition;
        private EditorGUI GUI;
        public static Rectangle Bounds;
        private float CameraZoom = 4f;

        protected override void Initialize()
        {
            Engine.RenderTarget.Scale = Vector2.One;
            Engine.RenderTarget.Set(1920, 1080);
            Engine.Size(1920, 1080);
            Bounds = new Rectangle(0,0,160,200);
        }
        protected override void Load()
        {
            ImGuiLayer.add(GUI = new EditorGUI());
        }
        private bool GetEntity(out Entity entity)
        {
            foreach (Entity Entity in World.Entities)
            {          
                var data = Entity.get<EditorComponent>();
               
                if (data != null)
                {
                    var entityRect = new Rectangle((int)Entity.position.X, (int)Entity.position.Y, data.width, data.height);
                    Logger.Log(entityRect);
                    if (entityRect.Contains(MousePosition))
                    {
                        entity = Entity;
                        return true;
                    }
                }
                else
                    Logger.Log("No!");
            }
            entity = null;
            return false;
        }
        private bool GetEntity()
        {
            foreach (Entity Entity in World.Entities)
            {
                var data = Entity.get<EditorComponent>();

                if (SelectedData != null)
                {
                    var entityRect = new Rectangle((int)Entity.position.X, (int)Entity.position.Y, SelectedData.width, SelectedData.height);
                    Logger.Log(entityRect);
                    if (entityRect.Contains(MousePosition))
                    {
                      
                        return true;
                    }
                }
                else
                    Logger.Log("No!");
            }
            return false;
        }
        protected override void Update()
        {
            CameraPosition.X += Input.Horizontal.GetAxis() * CameraOffset;
            CameraPosition.Y += Input.Vertical.GetAxis() * CameraOffset;
            Camera = Matrix.CreateTranslation(CameraPosition.X, CameraPosition.Y, 0) * Matrix.CreateScale(CameraZoom);
            Engine.Transform = Camera;
            MousePosition = Vector2.Transform(new Vector2(VirtualMouse.State.X, VirtualMouse.State.Y), Matrix.Invert(Camera));
            int x = (int)MousePosition.X / 8 * 8;
            int y = (int)MousePosition.Y / 8 * 8;
            MousePosition.X = x; MousePosition.Y = y;

            if(Bounds.Contains(MousePosition))
            {
                if (VirtualMouse.State.LeftButton == ButtonState.Pressed)
                {
                    if (Selected != null)
                    {
                        Selected.position = MousePosition;
                    }
                    else
                    {
                        GetEntity(out Entity entity);
                        if (entity != null)
                        {
                            Selected = entity;
                            SelectedData = entity.get<EditorComponent>();
                        }
                        else
                        {
                            //Place Entity
                            State = States.Placing;
                            if (VirtualMouse.State.LeftButton == ButtonState.Released && State == States.Placing)
                            {
                                var at = GetEntity();
                                var place_entity = GUI.Get();
                                if (at == false)
                                {
                                    {
                                        entity.position = MousePosition;
                                        World.Add(place_entity);
                                        Selected = place_entity;
                                        SelectedData = place_entity.get<EditorComponent>();
                                        State = States.None;
                                    }
                                }

                            }
                        }
                    }
                }
                else if (State == States.None)
                {
                    Selected = null;
                    SelectedData = null;
                }

                //Ading Entities;
                if (Keyboard.GetState().IsKeyDown(Keys.P))
                    State = States.Placing;

                if (Keyboard.GetState().IsKeyUp(Keys.P) && State == States.Placing)
                {
                    var at = GetEntity();
                    var entity = GUI.Get();
                    if (at == false)
                    {
                        {
                            entity.position = MousePosition;
                            World.Add(entity);
                            Selected = entity;
                            SelectedData = entity.get<EditorComponent>();
                            State = States.None;
                        }
                    }

                }

                //Removing Entities
                if (VirtualMouse.State.RightButton == ButtonState.Pressed)
                    State = States.Removing;

                if (VirtualMouse.State.RightButton == ButtonState.Released && State == States.Removing)
                {
                    var at = GetEntity(out Entity entity);
                    if (at != false)
                    {
                        Logger.Log("Entity at Mouse Position");
                        World.Entities.Remove(entity);
                        Selected = null;
                        SelectedData = null;
                    }
                    State = States.None;
                }
            }

            Logger.Log(World.Entities.Count);
           
        }
        protected override void Render()
        {
            base.Render();

            if (Selected != null)
            {
                var rect = new Rectangle((int)Selected.position.X, (int)Selected.position.Y, SelectedData.width, SelectedData.height);
                rect.Inflate(2, 2);
                Asset.HollowRectangle(rect.X, rect.Y, rect.Width, rect.Height,2, Color.Green);
            }
            else
            {
                var rect = new Rectangle((int)MousePosition.X, (int)MousePosition.Y, 8, 8);
                rect.Inflate(2, 2);
                Asset.HollowRectangle(rect.X, rect.Y, rect.Width, rect.Height, 2, Color.Red);
            }
            Asset.HollowRectangle(Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height, 2, Color.Red);
        }

        protected override void End()
        {
            Engine.Transform = Matrix.CreateTranslation(0, 0, 0);
            ImGuiLayer.remove<EditorGUI>();
            Engine.RenderTarget.Set(Engine.Config.Width, Engine.Config.Height);
            Engine.Size(Engine.Config.Width, Engine.Config.Height);
        }

        public static void Save(string path)
        {
            string fmt = "Assets/Levels/" + path + ".gre";
            for (int i = 0; i < World.Entities.Count; i++)
            {
                var entity = new EntityData();
                entity.Position = World.Entities[i].position;
                entity.Name = World.Entities[i].Name;
                LevelData.EntityData.Add(entity);
            }

            Logger.Log("Saved");
            LevelData.Save(fmt);
            LevelData.EntityData.Clear();
        }



    }
}

