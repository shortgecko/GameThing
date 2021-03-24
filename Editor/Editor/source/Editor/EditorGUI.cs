using ImGuiNET;
using Frankenweenie;
using System.Numerics;

namespace Game.Editor
{

    public class EditorGUI : ImGuiElement
    {
        private int selectedEntityId = 0;
        private string savePath = string.Empty;
        private bool showSaveGUI = false;

        public override void Draw()
        {
            ImGui.Begin("Entities", ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoMove);      
            ImGui.SetWindowSize(new Vector2(400, 1920));
            ImGui.SetWindowPos(Vector2.Zero);
            ImGui.ListBox("Entity List", ref selectedEntityId, EntityManager.Names, EntityManager.Names.Length);
            if(ImGui.Button("Save"))
            {
                showSaveGUI = !showSaveGUI;
            }

            if(showSaveGUI)
            {
                ImGui.InputText("Name", ref savePath, 200);
                if (ImGui.Button("OK"))
                    if (savePath != string.Empty)
                        LevelEditor.Save(savePath);
                    else
                        ImGui.Text("Path cannot be empty");
            }

            ImGui.InputInt("Level Width", ref LevelEditor.Bounds.Width);
            ImGui.InputInt("Level Height", ref LevelEditor.Bounds.Height);
            ImGui.End();       
        }


        public Entity Get()
        {
            return EntityManager.Create(EntityManager.Names[selectedEntityId]);
        }
    }
}
;