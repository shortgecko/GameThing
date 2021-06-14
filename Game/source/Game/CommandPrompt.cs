using System;
using System.Collections.Generic;
using System.Text;
using Frankenweenie;
using ImGuiNET;
using System.Numerics;
using Microsoft.Xna.Framework.Input;

namespace Game
{
    public class CommandPrompt : ImGuiElement
    {
        private readonly char Quote = '\u0022';
        private string Input = string.Empty;
        private bool TextChanged = false;
        private bool showConsole;
        private static Dictionary<string, Action<string[]>> Commands = new Dictionary<string, Action<string[]>>();

        public CommandPrompt()
        {
            Commands.Add("help",(string[] args ) =>
            {
                ImGui.Text("Commands");
                foreach(var command in Commands.Keys)
                {
                    ImGui.Text(command);
                }
            });
            Commands.Add("entity_count", (string[] args) => ImGui.Text(World.Count.ToString()));
            Commands.Add("load",(string[] args) => 
            {
                string arguments = args[0];
                try
                {
                LevelLoader.Load(arguments);
                }
                catch
                {
                    ImGui.Text("Level " + arguments + " not found");
                }
            });
        }

        public override void Draw()
        {
            if(Keyboard.GetState().IsKeyDown(Keys.OemTilde))
            {
                showConsole = !showConsole;
            }

            if(showConsole)
            {
                ImGui.Begin("Command Prompt", ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoCollapse);
                ImGui.SetWindowPos(Vector2.Zero);
                ImGui.SetWindowSize(new Vector2(Window.Width, ImGui.GetWindowSize().Y));
                TextChanged = ImGui.InputText("Command", ref Input, 1000);
                ImGui.End();
            }

            if(TextChanged)
            {
               string[] split =  Input.Split(new char[] {' '});
               string commandKey = split[0];
               Logger.Log(split.Length);
                string[] args = null;
               if (split.Length > 1)
               {
                    args = new string[split.Length - 2];
                    for (int i = 1; i < split.Length - 1; i++)
                        args[i] = split[i];
               }
               if(Commands.ContainsKey(commandKey))
               {
                   var command = Commands[commandKey];
                   command.Invoke(args);
               }
               else
               {
                   ImGui.Text($"Command {commandKey} does not exis\n use command {Quote}help{Quote} to see a list of commands.");
               }
            }
        }
    }
}
