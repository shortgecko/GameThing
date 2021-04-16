namespace Frankenweenie
{
    public class Scene
    {
        public string Name = "Scene";
        public bool IsRunning { get; set; }
        private bool HasInitialized = false;
        private bool HasLoaded = false;
        protected virtual void Initialize() { }
        protected virtual void Load() { }
        protected virtual void Update() => World.Update();
        protected virtual void Render() => World.Render();
        protected virtual void Unload() { }
        protected virtual void End() { }

        public void Begin()
        {
            if (HasInitialized == false)
            {
                this.Initialize();
                this.Load();
                this.HasInitialized = true;
                this.IsRunning = true;
            }
            Update();

        }

        public void Draw()
        {
            Render();
        }

        public void Dispose()
        {
            this.HasInitialized = false;
            this.IsRunning = false;
            End();
            World.Clear();
        }

        

    }
}
