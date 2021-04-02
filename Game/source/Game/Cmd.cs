using System;
using System.Collections.Generic;
using System.Text;
using Frankenweenie;
using ImGuiNET;
using System.Numerics;

namespace Game
{
    //very bad, especially when dealing with agruments
    public class Cmd : ImGuiElement
    {
        private static Dictionary<string, Action> Commands = new Dictionary<string, Action>();
        private string command = string.Empty;
        private string arguments = string.Empty;
        private void LoadLevel_CMD()
        {
            try
            {
                Game.Load(arguments);
            }
            catch
            {
                ImGui.Text("Level " + arguments + " not found");
            }
        }
        private void Help_CMD() 
        { 
            ImGui.Text("Commands: "); foreach (var cmd in Commands.Keys) ImGui.Text(cmd);
        }

        public Cmd()
        {
            Commands.Add("help", Help_CMD);
            Commands.Add("entity_count", () => ImGui.Text(World.Entities.Count.ToString()));
            Commands.Add("load", LoadLevel_CMD);
        }

        public override void Draw()
        {
            ImGui.Begin("Command Prompt", ImGuiWindowFlags.NoMove);
            ImGui.SetWindowPos(Vector2.Zero);
            ImGui.SetWindowSize(new Vector2(Engine.Width, ImGui.GetWindowSize().Y));
            ImGui.InputText("Command", ref command, 1000);
            Action cmd;
            if (Commands.TryGetValue(command, out cmd))
                cmd.Invoke();
            else if (Commands.TryGetValue(command.Split(new char[] { ' ' })[0], out cmd))
            {
                arguments = command.Split(new char[] { ' ' })[1];
                cmd.Invoke();
            }
            else
                ImGui.Text("Command " + command + " not found");
            command = string.Empty;
            arguments = string.Empty;
            ImGui.End();
        }
    }
}
