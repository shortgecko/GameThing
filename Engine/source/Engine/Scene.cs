namespace Frankenweenie
{
    public class Scene
    {
        public World World = new World();
        public bool IsRunning { get; set; }
        private bool HasInitialized = false;
        private bool HasLoaded = false;
        protected virtual void Initialize() { }
        protected virtual void Load() { }
        protected virtual void Update()
        {
            World.Update();
        }
        protected virtual void Render()
        {
            World.Render();
        }
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

        public void Leave()
        {
            this.HasInitialized = false;
            this.IsRunning = false;
            End();
        }

    }
}
