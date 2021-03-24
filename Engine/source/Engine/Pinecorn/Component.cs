namespace Frankenweenie
{
    public class Component
    {
        public Entity entity;
        public bool Active = false;

        public virtual void Initialize() { }
        public virtual void Update() { }
        public virtual void Render() { }

    }
}
