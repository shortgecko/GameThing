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
        private static Config Config { get; set; }
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
        public static SceneManager SceneManager;
        private static Color clearColor = Color.Black;

        public static int Width
        {
            get
            {
                return Device.GraphicsDevice.PresentationParameters.BackBufferWidth;
            }
        }

        public static int Height
        {
            get
            {
                return Device.GraphicsDevice.PresentationParameters.BackBufferHeight;
            }
        }
        public static string AssetDirectory
        {
            get
            {
                return Config.AssetDirectory;
            }
        }

        public static string Title
        {
            get
            {
                return Instance.Window.Title;
            }
        }
        public static string Directory
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }
        public Engine()
        {
            Device = new GraphicsDeviceManager(this);
            Device.PreferMultiSampling = true;
            Device.IsFullScreen = false;
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
            new ImGuiLayer(Device, this);
        }


        protected override void Initialize()
        {
            Content.RootDirectory = Config.AssetDirectory;
            IsFixedTimeStep = Config.FixedTimeStep;
            Window.Title = Config.WindowTitle;
            SceneManager = Config.SceneManager;
            Device.PreferredBackBufferWidth = Config.Width;
            Device.PreferredBackBufferHeight = Config.Height;
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
            m_Scene.Dispose();
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

            VirtualMouse.Update();
            foreach (VirtualButton Button in Input.Buttons)
                Button.EarlyUpdate();
            m_Scene.Begin();
            foreach (VirtualButton Button in Input.Buttons)
                Button.LateUpdate();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(RenderTarget.Target);
            GraphicsDevice.Clear(clearColor);
            Drawer.Batch.Begin();
            if(m_Scene.IsRunning)
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

        public static void Run(ref Config config, string scene)
        {
            CreateInstance(config, scene);
            Engine.Instance.Run();
            End();
        }

        public static void RunWithLogging(ref Config config, string scene)
        {
            CreateInstance(config, scene);
            try
            {
                Engine.Instance.Run();
                Logger.Log("[EVENT] APP CLOSED");
            }
            catch (Exception e)
            {
                Logger.Log("[ERROR] APP CRASHED CHECK ERROR LOG FOR MORE DETAILS");
                ErrorLog.Log(e);
            }
            finally
            {
                End();
            }
        }

        private static void CreateInstance(Config config, string scene)
        {
            Config = config;
            Engine.Instance = new Engine();
            Engine.m_Scene = Config.SceneManager[scene];
        }

        private static void End()
        {
            Logger.Save();
            Frankenweenie.Content.Dispose();
        }

        public static void ClearColor(Color color) => clearColor = color;

        public static void Fullscreen(bool value) => Device.IsFullScreen = value;


    }
}
