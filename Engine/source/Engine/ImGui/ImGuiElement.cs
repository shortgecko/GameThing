using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frankenweenie
{
    public abstract class ImGuiTheme
    {
        public abstract void Invoke();
    }

    public abstract class ImGuiElement
    {
        public abstract void Draw();
    }
}
