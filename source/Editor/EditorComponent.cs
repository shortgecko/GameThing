using Frankenweenie;

namespace Game.Editor
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
