using Frankenweenie;
using System;

namespace Game
{
    public class Runner
    {
        private static void Main(string[] args)
        {
            Engine.Config = new Config("Game Thing", "Assets", true, 160 * 4, 200 * 4, true);
            Engine.Run(new Level());
        }
    }
}