using Pinecorn;
using ImMonoGame;
using ImGuiNET;
using Microsoft.Xna.Framework.Input;
namespace Game
{
    public static class DebugAction
    {
        public static ImguiComponent ImGui;
        public static Microsoft.Xna.Framework.Game Game;

        public static void Initialize()
        {
           ImGui = new ImguiComponent(Engine.Device, Game);
           ImGui.Elements.Add(new ImGuiDemo(ImGui._imGuiTexture).Main);
           ImGui.Initialize();
        }

        public static void Render(Microsoft.Xna.Framework.GameTime gameTime)
        {
            ImGui.Draw(gameTime);
        }
        public static void Gui()
        {                           
            
        }
    }
}