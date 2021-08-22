using System.Collections.Generic;
namespace Frankenweenie
{
    public class Scene
    {
        public string Name = "Scene";
        public bool IsRunning { get; set; }
        private bool Initialized = false;
        protected virtual void Initialize()
        {
            World.CreateCamera();
        }
        protected virtual void Load() { }
        protected virtual void Update() => World.Update();
        protected virtual void Render() => World.Render();
        protected virtual void Unload() { }
        protected virtual void End() { }

        public void Begin()
        {
            if (!Initialized)
            {
                this.Initialize();
                this.Load();
                this.Initialized = true;
                this.IsRunning = true;
            }
            Update();

        }

        public void BeginDraw() => Render();

        public void Dispose()
        {
            this.Initialized = false;
            this.IsRunning = false;
            End();
            World.Clear();
        }

        

    }
}
