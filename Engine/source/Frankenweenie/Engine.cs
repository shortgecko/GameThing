using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
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

        public static string AssetDirectory
        {
            get
            {
                return Config.AssetDirectory;
            }
        }
        public static string AssemblyDirectory
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
            Device.PreferredBackBufferWidth = Config.Width;
            Device.PreferredBackBufferHeight = Config.Height;
            Device.ApplyChanges();

            Logger.Initialize();

            RenderTarget = new VirtualRenderTarget();
#if DEBUG || FRANKENWEENIE_IMGUI
            ImGuiLayer.Initialize();
#endif

#if DEBUG
            Logger.Log($"GPU Information {GraphicsAdapter.DefaultAdapter.Description}");
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
            Drawer.Batch.Begin(SpriteSortMode.FrontToBack);
            if(m_Scene.IsRunning)
                m_Scene.Draw();
            Drawer.Batch.End();
            GraphicsDevice.SetRenderTarget(null);

            GraphicsDevice.Clear(Color.Black);
            Drawer.Batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, transformMatrix: Transform);
            RenderTarget.Render();
            Drawer.Batch.End();

#if DEBUG || FRANKENWEENIE_IMGUI
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

        public static void Run(ref Config config, SceneManager sceneManager, string scene = null)
        {
            CreateInstance(ref config, sceneManager, scene);
            Engine.Instance.Run();
            End();
        }

        public static void RunWithLogging(ref Config config, SceneManager sceneManager, string scene = null)
        {
            CreateInstance(ref config, sceneManager, scene);
            try
            {
                Engine.Instance.Run();
                Logger.Log("Engine closing");
            }
            catch (Exception e)
            {
                Logger.Log($"[Error] {e.Message}\n Check Error Log for more details");
                ErrorLog.Log(e);
            }
            finally
            {
                End();
            }
        }

        private static void CreateInstance(ref Config config, SceneManager sceneManager, string scene = null)
        {
            Config = config;
            Engine.Instance = new Engine();
            Engine.SceneManager = sceneManager;
            if(scene != null)
                Engine.m_Scene = Engine.SceneManager[scene];
            else
            {
                string key = Engine.SceneManager.Scenes.First().Key;
                Engine.m_Scene= Engine.SceneManager.Scenes[key];
            }

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
