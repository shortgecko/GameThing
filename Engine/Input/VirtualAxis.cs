namespace Frankenweenie
{
    public abstract class VirtualAxis
    {
        public string Name;
        protected abstract float GetAxis { get; }
        public static implicit operator float(VirtualAxis axis) => axis.GetAxis;
    }
}
