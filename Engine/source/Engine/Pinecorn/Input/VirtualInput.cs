namespace Frankenweenie
{
    public class VirtualInputButton : VirtualButton
    {
        public VirtualButton Input1;
        public VirtualButton Input2;

        public override bool Pressed()
        {
            if (Input1.Pressed())
                return true;
            else if (Input2.Pressed())
                return true;
            return false;
        }

        public override bool Released()
        {
            if (Input1.Released())
                return true;
            else if (Input2.Released())
                return true;
            return false;
        }

    }
}