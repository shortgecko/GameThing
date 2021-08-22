namespace Frankenweenie
{
    public abstract class VirtualAxis
    {
        protected abstract float GetAxis { get; }
        public static implicit operator float(VirtualAxis axis) => axis.GetAxis;
    }
}
