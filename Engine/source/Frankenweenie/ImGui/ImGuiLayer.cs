using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
namespace Frankenweenie
{
    public class ImGuiLayer
    {
        private static GraphicsDeviceManager _graphics;
        private static ImGuiRenderer _imGuiRenderer;
        private static Texture2D _xnaTexture;
        public static IntPtr ImGuiTexture;
        private static Microsoft.Xna.Framework.Game Game;
        public static float fontSize = 14f;
        public static string Font = "";
        private static List<ImGuiElement> Elements;
        public static ImGuiTheme ImGuiTheme;

        public ImGuiLayer(GraphicsDeviceManager graphics, Microsoft.Xna.Framework.Game game)
        {
            _graphics = graphics;
            Game = game;
            Elements = new List<ImGuiElement>();
        }

        public static void Initialize()
        {
            _imGuiRenderer = new ImGuiRenderer(Game);
            if (ImGuiTheme != null)
                ImGuiTheme.Invoke();
            var io = ImGui.GetIO();
            io.Fonts.AddFontFromFileTTF(Font, fontSize);
            _imGuiRenderer.RebuildFontAtlas();

            LoadContent();
        }

        private static void LoadContent()
        {
            // Texture loading example

            // First, load the texture as a Texture2D (can also be done using the XNA/FNA content pipeline)
            _xnaTexture = CreateTexture(_graphics.GraphicsDevice, 300, 150, pixel =>
            {
                var red = (pixel % 300) / 2;
                return new Color(red, 1, 1);
            });

            // Then, bind it to an ImGui-friendly pointer, that we can use during regular ImGui.** calls (see below)
            ImGuiTexture = _imGuiRenderer.BindTexture(_xnaTexture);


        }

        public static void Draw()
        {
            // Call BeforeLayout first to set things up
            _imGuiRenderer.BeforeLayout(Engine.GameTime);
            // Draw our UI
            for (int i = 0; i < Elements.Count; i++)
                Elements[i].Draw();
            // Call AfterLayout now to finish up and draw all the things
            _imGuiRenderer.AfterLayout();


        }

        private static Texture2D CreateTexture(GraphicsDevice device, int width, int height, Func<int, Color> paint)
        {
            //initialize the texture
            Texture2D texture = new Texture2D(device, width, height);

            //the array holds the color for each pixel in the texture
            Color[] data = new Color[width * height];
            for (int pixel = 0; pixel < data.Length; pixel++)
            {
                //the function applies the color according to the specified pixel
                data[pixel] = paint(pixel);
            }

            //set the color
            texture.SetData(data);
            return texture;
        }

        //Utils
        public static IntPtr TexToIntPtr(Texture2D texture)
        {
            var _imGuiTexture = _imGuiRenderer.BindTexture(texture);
            return _imGuiTexture;
        }

        public static void add<T>() where T : ImGuiElement, new()
        {
            Elements.Add(new T());
        }

        public static void add(ImGuiElement e)
        {
            Elements.Add(e);
        }

        public static void remove(ImGuiElement e)
        {
            Elements.Remove(e);
        }

        public static void remove<T>() where T : ImGuiElement
        {
            var element = Elements.Find(c => c.GetType() == typeof(T));
            Elements.Remove(element);
        }

        public static ImGuiElement get<T>() where T : ImGuiElement
        {
            var element = Elements.Find(c => c.GetType() == typeof(T));
            return element;
        }
    }

}
