using System.Collections.Generic;
namespace Frankenweenie
{
    public class VirtualInputAxis : VirtualAxis
    {
        private List<VirtualAxis> Axes = new List<VirtualAxis>();
        public void add(VirtualAxis axis) => Axes.Add(axis);

        protected override float GetAxis
        {
            get
            {
                foreach (VirtualAxis axis in Axes)
                {
                    if (axis != 0)
                        return axis;
                }
                return 0;
            }
        }
    }
}
