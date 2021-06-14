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

        private TimeSpan counterElapsed = TimeSpan.Zero;
        private int fpsCounter = 0;
        public static GamePadState[] GamePads = new GamePadState[4];
        public static GameTime GameTime;

        public static VirtualRenderTarget RenderTarget;
        public static Matrix Transform = Matrix.CreateTranslation(0, 0, 0);
        public static SceneManager SceneManager;
        private static Color clearColor = Microsoft.Xna.Framework.Color.Black;

        public static float fpsValue;
        public static float FPS => fpsValue;
        public static float MaxRunTime = 0f;

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

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            foreach (Action action in Frankenweenie.Window.ResizeActions)
            {
                action.Invoke();
            }
        }

        public Engine()
        {       
            Device = new GraphicsDeviceManager(this);
            Device.PreferMultiSampling = true;
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
            Window.ClientSizeChanged += Window_ClientSizeChanged;
            new ImGuiLayer(Device, this);
        }

        protected override void Initialize()
        {
            Content.RootDirectory = Config.AssetDirectory;
            if(!Config.FixedTimeStep)
            {
                Device.SynchronizeWithVerticalRetrace = false;
                IsFixedTimeStep = false;
            }
            else
            {
                IsFixedTimeStep = true;
            }
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
            //base.InactiveSleepTime = TimeSpan.FromSeconds(0);
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


            if(MaxRunTime != 0)
            {
                if (Timer > MaxRunTime)
                    Exit();
            }    


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

            Profiler.Update();
        }



        protected override void Draw(GameTime gameTime)
        {
           
           
            GraphicsDevice.SetRenderTarget(RenderTarget.Target);
            GraphicsDevice.Clear(clearColor);



            Drawer.Batch.Begin(SpriteSortMode.FrontToBack);
            if(m_Scene.IsRunning)
                m_Scene.BeginDraw();
            Drawer.Batch.End();
            GraphicsDevice.SetRenderTarget(null);

            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Black);
            Drawer.Batch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, transformMatrix: Transform);
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
                fpsValue = fpsCounter;
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
            Profiler.End();
        }

        public static void Color(Color color)
        {
            clearColor = color;
        }

        public static void Fullscreen(bool value)
        {
            Device.IsFullScreen = value;
        }

        public static void Quit()
        {
            End();
            Instance.Exit();
        }

    }
}
