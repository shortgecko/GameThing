namespace Frankenweenie
{
    public class VirtualInputAxis : VirtualAxis
    {
        public VirtualAxis Axis1;
        public VirtualAxis Axis2;

        public override float GetAxis()
        {
            if (Axis1.GetAxis() != 0)
                return Axis1.GetAxis();
            else if (Axis2.GetAxis() != 0)
                return Axis2.GetAxis();
            return 0;
        }
    }
}
