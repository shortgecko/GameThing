using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Blah = Frankenweenie;

namespace Frankenweenie
{
    public class Engine : Microsoft.Xna.Framework.Game
    {
        public static Engine Instance;
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

        public static VirtualRenderTarget RenderTarget;
        public static Matrix Transform = Matrix.CreateTranslation(0, 0, 0);
        public static int Width;
        public static int Height;

        public Engine()
        {
            Device = new GraphicsDeviceManager(this);
            Device.PreferMultiSampling = true;
            Device.IsFullScreen = false;
            Window.AllowUserResizing = true;
            IsMouseVisible = true;

#if DEBUG
            new ImGuiLayer(Device, this);
#endif

        }


        protected override void Initialize()
        {
            Content.RootDirectory = Config.AssetDirectory;
            IsFixedTimeStep = Config.FixedTimeStep;
            Window.Title = Config.WindowTitle;
            Device.PreferredBackBufferWidth = Config.Width;
            Device.PreferredBackBufferHeight = Config.Height;
            Width = Config.Width;
            Height = Config.Height;
            Device.ApplyChanges();

            Logger.Initialize();

            RenderTarget = new VirtualRenderTarget();
#if DEBUG
            ImGuiLayer.Initialize();
#endif
            LoadContent();
        }

        protected override void LoadContent()
        {
            new Drawer();
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

        public static void Size(int width, int height)
        {
            Device.PreferredBackBufferWidth = width;
            Device.PreferredBackBufferHeight = height;
            Device.ApplyChanges();
        }

        public static void Set(Scene scene)
        {
            m_Scene.Leave();
            m_Scene = scene;
        }

        protected override void Update(GameTime gameTime)
        {
            RawDeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Delta = RawDeltaTime * TimeRate;
            Timer += RawDeltaTime;


            GamePads[0] = GamePad.GetState(PlayerIndex.One);
            GamePads[1] = GamePad.GetState(PlayerIndex.Two);
            GamePads[2] = GamePad.GetState(PlayerIndex.Three);
            GamePads[3] = GamePad.GetState(PlayerIndex.Four);
            
            Engine.GameTime = gameTime;
            Width = Device.GraphicsDevice.PresentationParameters.BackBufferWidth;
            Height = Device.GraphicsDevice.PresentationParameters.BackBufferHeight;

            VirtualMouse.Update();
            m_Scene.Begin();    
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(RenderTarget.Target);
            GraphicsDevice.Clear(Color.Black);
            Drawer.Batch.Begin();
            m_Scene.Draw();
            Drawer.Batch.End();
            GraphicsDevice.SetRenderTarget(null);

            GraphicsDevice.Clear(Color.Black);
            Drawer.Batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, transformMatrix: Transform);
            RenderTarget.Render();
            Drawer.Batch.End();

#if DEBUG
            ImGuiLayer.Draw();
#endif


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
            Engine.Instance = new Engine();
            Engine.m_Scene = scene;
            Engine.Instance.Run();
            Logger.Save();
        }

        public static void RunWithLogging(Scene scene)
        {
            Engine.Instance = new Engine();
            Engine.m_Scene = scene;
            try
            {
                Engine.Instance.Run();
                Logger.Log("[EVENT] APP CLOSED");
            }
            catch (Exception e)
            {
                Logger.Log("[ERROR] APP CRASHED CHECK ERROR LOG FOR MORE DETAILS");
                Logger.Save();
                ErrorLog.Log(e);
            }
            Logger.Save();
        }

    }
}
