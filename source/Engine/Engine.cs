﻿
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Blah = Pinecorn;
using Microsoft.Xna.Framework.Input;
using Pinecorn;

namespace Pinecorn
{
    public class Engine : Microsoft.Xna.Framework.Game
    {
		public static GraphicsDeviceManager Device;
		private static Scene m_Scene;
		public static Config Config { get; set; }

		public static float RawDeltaTime;
		public static float Delta;
		public static float Timer;
		public static float TimeRate = 1f;

		public static int FPS;
		private TimeSpan counterElapsed = TimeSpan.Zero;
		private int fpsCounter = 0;
		public static GamePadState[] GamePads = new GamePadState[4];
		public static GameTime GameTime;


		public static Pinecorn.RenderTarget RenderTarget;

		public Engine()
		{
			Device = new GraphicsDeviceManager(this);
			Device.PreferMultiSampling = true;
			Device.IsFullScreen = false;
			Window.AllowUserResizing = true;
			IsMouseVisible = true;
		}

		protected override void Initialize()
        {
			Content.RootDirectory = Config.AssetDirectory;
			if(Config.FixedTimeStep)
				this.IsFixedTimeStep = true;
			else
				throw new Exception("no");

			Window.Title = Config.WindowTitle;
			Device.PreferredBackBufferWidth = Config.Width;
			Device.PreferredBackBufferHeight = Config.Height;
			Device.ApplyChanges();


			Logger.Initialize();

			RenderTarget = new RenderTarget(Vector2.Zero, Config.Width, Config.Height, 1f);

			Asset.Initialize();
			LoadContent();
		}

        protected override void LoadContent()
		{
			Blah.Drawer.Batch = new SpriteBatch(Device.GraphicsDevice);
		}

		protected override void UnloadContent()
		{
			base.UnloadContent();
		}

		public static Scene Scene
		{
			get
            {
				return m_Scene;
            }
		}

		public static void Size(Point size)
        {
			Device.PreferredBackBufferWidth = size.X;
			Device.PreferredBackBufferHeight = size.Y;
			Device.ApplyChanges();
		}

		public static void Set(Scene scene)
        {
			m_Scene = scene;
        }

		protected override void Update(GameTime gameTime)
        {
			RawDeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
			Delta = RawDeltaTime * TimeRate;
			Timer += RawDeltaTime;

			MouseInput.Update();

			GamePads[0] = GamePad.GetState(PlayerIndex.One);
			GamePads[1] = GamePad.GetState(PlayerIndex.Two);
			GamePads[2] = GamePad.GetState(PlayerIndex.Three);
			GamePads[3] = GamePad.GetState(PlayerIndex.Four);

			m_Scene.Begin();
			m_Scene.Update();
			Engine.GameTime = gameTime;

			
		}
		protected override void Draw(GameTime gameTime)
		{

			GraphicsDevice.SetRenderTarget(RenderTarget.Target);
			GraphicsDevice.Clear(Color.Black);
            Drawer.Batch.Begin(/*transformMatrix: map.Camera.Transform*/);
			m_Scene.Render();
            Drawer.Batch.End();
            GraphicsDevice.SetRenderTarget(null);
			
			GraphicsDevice.Clear(Color.Black);
			Blah.Drawer.Batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, transformMatrix: m_Scene.Camera.Transform);
			Blah.Drawer.Batch.Draw(RenderTarget.Target, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, RenderTarget.Scale, SpriteEffects.None, 0f);
			Blah.Drawer.Batch.End();

			fpsCounter++;
			counterElapsed += gameTime.ElapsedGameTime;
			if (counterElapsed >= TimeSpan.FromSeconds(1))
			{
#if DEBUG
				Window.Title = Config.WindowTitle + " " + fpsCounter.ToString() + " fps - " + (GC.GetTotalMemory(false) / 1048576f).ToString("F") + " MB";
#endif
				FPS = fpsCounter;
				fpsCounter = 0;
				counterElapsed -= TimeSpan.FromSeconds(1);

			}
		}

		public static void Run(Scene scene)
        {
			Engine engine = new Engine();
			Engine.m_Scene = scene;
			engine.Run();
			Logger.Save();
        }

		public static void RunWithLogging(Scene scene)
		{
			Engine engine = new Engine();
			Engine.m_Scene = scene;
			try
			{
				engine.Run();
				Logger.Log("[EVENT] APP CLOSED");
			}
			catch (Exception e)
			{
				Logger.Log("[ERROR] APP CRASHED CHECK ERROR LOG FOR MORE DETAILS");
				Logger.Save();
				ErrorLog.Log(e);
			}
		}


		public static void Collect()
		{ 
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

	}
}
