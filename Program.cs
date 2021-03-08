﻿
using Pinecorn;
using Game;
using System;


class Runner
{
	static void Main()
	{
		Engine.Config= new Config("game.exe","Assets",true, 1920, 1080);
		using (var engine = new Engine())
        {

			DebugAction.Game = engine;
			Engine.Scene = new Sandbox();
			engine.Run();
		}
		Logger.Save();
	}
}