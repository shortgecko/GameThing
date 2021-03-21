using Frankenweenie;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game.Editor
{
    public class LevelEditor 
    {
        private Texture2D Gizmo = Asset.Texture("Graphics/gizmo.png");
        private Vector2 GizmoPosition;
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
        private float CameraOffset = 8f;
        private Matrix Camera;
        private int CameraScale;

        public void Initialize()
        {
            Engine.RenderTarget.Scale = Vector2.One;
        }
        public void Load()
        {
            
        }
        public void Unload()
        {
            
        }
        public Entity GetEntity()
        {

            foreach (Entity Entity in World.Entities)
            {
                SelectedData = Entity.get<EditorComponent>();
                if (SelectedData != null)
                {
                    if (VirtualMouse.Rect.Intersects(new Rectangle((int)Entity.position.X, (int)Entity.position.Y, SelectedData.width, SelectedData.height)) && Input.Shoot.Pressed())
                    {
                        Logger.Log("Entity Selected");
                        return Entity;
                    }
                }
            }
            return null;
        }
        public void Update()
        {
            if (!Innit)
            {
                Initialize();
                Innit = false;
            }
            
            if (Input.Shoot.Pressed())
            {
                if (Selected != null)
                {
                    Selected.position = VirtualMouse.Position;
                    GizmoPosition = VirtualMouse.Position;
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

            CameraPosition.X = Input.Horizontal.GetAxis() * CameraOffset;
            CameraPosition.Y = Input.Vertical.GetAxis() * CameraOffset;
            Camera = Matrix.CreateTranslation(CameraPosition.X, CameraPosition.Y, 0) * Matrix.CreateScale(4f);
            Engine.Transform = Camera;
            VirtualMouse.Scale = 4f;
            Logger.Log("***");

        }
        public void Render()
        {
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

