using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frankenweenie;

namespace Game
{
    public class EditorComponent : Component
    {
        public int width;
        public int height;

        public EditorComponent(int m_width, int m_height)
        {
            width = m_width;
            height = m_height;
        }
    }
}
