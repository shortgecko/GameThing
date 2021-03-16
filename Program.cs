using Pinecorn;

namespace Game
{
    class Runner
    {
        static void Main(string[] args)
        {
            Engine.Config = new Config("Game Thing", "Assets", true, 1920, 1080);
            Engine.Run(new Sandbox());
        }
    }
}