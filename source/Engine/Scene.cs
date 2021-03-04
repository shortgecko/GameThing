using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pinecorn
{
    public class Scene
    {
        public World World = new World();
        public bool IsRunning { get; set; }
        private bool HasInitialized = false;
        private bool HasLoaded = false;
        public Camera Camera = new Camera();
        public virtual void Initialize() 
        {
            World.Initialize();
        }
        public virtual void Load() { }
        public virtual void Update() 
        {
            World.Update();
            Camera.Update();          
        }
        public virtual void Render() 
        { 
            World.Render();
        }
        public virtual void Unload() { }

        public void Begin()
        {
            if(HasInitialized == false)
            {
                this.Initialize();
                this.HasInitialized = true;
            }
            if (HasLoaded == false)
            {
                this.Load();
                this.HasLoaded = true;
                IsRunning = true;
            }

        }
    }
}
