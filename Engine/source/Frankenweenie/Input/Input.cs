using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frankenweenie
{
    public class Input
    {
        public static List<VirtualButton> Buttons = new List<VirtualButton>();
        public static List<VirtualAxis> Axes = new List<VirtualAxis>();
        public static void add(VirtualButton button) => Buttons.Add(button);
        public static void Add(VirtualAxis axis) => Axes.Add(axis);


    }
}