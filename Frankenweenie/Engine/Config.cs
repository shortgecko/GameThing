namespace Frankenweenie
{
    public struct Config
    {
        public string WindowTitle { get; set; }
        public bool FixedTimeStep { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsMouseVisible { get; set; }

        public Config(string windowTitle, bool fixedTimestep, int width, int height, bool isMouseVisible = false)
        {
            WindowTitle = windowTitle;
            FixedTimeStep = fixedTimestep;
            Width = width;
            Height = height;
            IsMouseVisible = isMouseVisible;
        }

    }
}
