using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frankenweenie
{
    public class Scene
    {
        public World World = new World();
        public bool IsRunning { get; set; }
        private bool HasInitialized = false;
        private bool HasLoaded = false;        
        protected virtual void Initialize() {  }
        protected virtual void Load() { }
        protected virtual void Update() 
        {
            World.Update();         
        }
        protected virtual void Render() 
        { 
            World.Render();
        }

        private void Begin()
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

        public void Run()
        {
            Begin();
            Update();
        }

        public void Draw()
        {
            Render();
        }
    }
}
