using Frankenweenie;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game.Editor
{
    public class LevelEditor : Scene
    {
        private enum States
        {
            Placing,
            Removing
        };
        private Entity Selected;
        private EditorComponent SelectedData;
        public LevelData LevelData = new LevelData();
        private bool Innit = false;
        private Vector2 CameraPosition;
        private float CameraOffset = 2f;
        private Matrix Camera;
        private int CameraScale;
        private Rectangle MouseRect;
        private Vector2 MousePosition;

        protected override void Initialize()
        {
            Engine.RenderTarget.Scale = Vector2.One;
            ImGuiLayer.add<ImGuiDemo>();
        }
        protected override  void Load()
        {
            
        }
        private Entity GetEntity()
        {
            foreach (Entity Entity in World.Entities)
            {          
                SelectedData = Entity.get<EditorComponent>();
                if (SelectedData != null)
                {
                    var entityRect = new Rectangle((int)Entity.position.X, (int)Entity.position.Y, SelectedData.width, SelectedData.height);
                    Logger.Log(entityRect);
                    if (MouseRect.Intersects(entityRect) && Keyboard.GetState().IsKeyDown(Keys.Z))
                    {
                        Logger.Log("Entity Selected");
                        return Entity;
                    }
                }
                else
                    Logger.Log("No!");
            }
            return null;
        }
        protected override void Update()
        {
            MousePosition = Vector2.Transform(new Vector2(VirtualMouse.State.X, VirtualMouse.State.Y), Matrix.Invert(Matrix.CreateTranslation(Vector3.Zero) * Matrix.CreateScale(4f)));
            MouseRect = new Rectangle((int)MousePosition.X, (int)MousePosition.Y, 1, 1);
            
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                if (Selected != null)
                {
                    Selected.position = MousePosition;
                }
                else
                {
                    GetEntity();
                    if (GetEntity() != null)
                        Selected = GetEntity();
                }
            }
            else
                Selected = null;

            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl) && Keyboard.GetState().IsKeyDown(Keys.S))
            {
                for(int i = 0; i < World.Entities.Count; i++)
                {
                    var entity = new EntityData();
                    entity.Position = World.Entities[i].position;
                    entity.Name = World.Entities[i].Name;
                    LevelData.EntityData.Add(entity);
                }

                Logger.Log("Saved");
                LevelData.Save("Assets/Levels/test.gre");
                LevelData.EntityData.Clear();
            }

            CameraPosition.X += Input.Horizontal.GetAxis() * CameraOffset;
            CameraPosition.Y += Input.Vertical.GetAxis() * CameraOffset;
            Camera = Matrix.CreateTranslation(CameraPosition.X, CameraPosition.Y, 0) * Matrix.CreateScale(4f);
            Engine.Transform = Camera;
        }
        protected override void Render()
        {
            base.Render();

            if (Selected != null)
            {
                var rect = new Rectangle((int)Selected.position.X, (int)Selected.position.Y, SelectedData.width, SelectedData.height);
                rect.Inflate(2, 2);
                Asset.HollowRectangle(rect.X, rect.Y, rect.Width, rect.Height,2, Color.Red);
            }

                
        }

        public void End() { Innit = false;  }

        #region GUI

        #endregion

    }
}

