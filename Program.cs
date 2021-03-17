using Pinecorn;

namespace Game
{
    class Runner
    {
        static void Main(string[] args)
        {
            Engine.Config = new Config("Game Thing", "Assets", true, 160 * 4, 200 * 4);
            Engine.Run(new Level());
        }
    }
}