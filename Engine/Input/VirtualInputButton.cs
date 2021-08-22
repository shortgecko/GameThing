using System.Collections.Generic;

namespace Frankenweenie
{
    public class VirtualInputButton : VirtualButton
    {
        private List<VirtualButton> Buttons = new List<VirtualButton>();

        public void Add(VirtualButton button) => Buttons.Add(button);

        public override void EarlyUpdate()
        {
            foreach (VirtualButton button in Buttons)
            {
                button.EarlyUpdate();
            }
        }

        public override bool Pressed
        {
            get
            {
                foreach (VirtualButton button in Buttons)
                {
                    if (button.Pressed)
                        return true;
                }
                return false;
            }
        }

        public override bool Released
        {
            get
            {
                foreach (VirtualButton button in Buttons)
                {
                    if (button.Released)
                        return true;
                }
                return false;
            }
        }

        public override void LateUpdate()
        {
            foreach (VirtualButton button in Buttons)
            {
                button.LateUpdate();
            }
        }

    }
}