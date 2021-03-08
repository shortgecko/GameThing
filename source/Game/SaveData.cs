using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pinecorn;
using System;
using System.Collections.Generic;
using MonoGame.Framework.Utilities.Deflate;
using System;
using System.IO;
using System.Text;

namespace Game
{
    public class Instance
    {
        public int LevelID;  
    }
    public static class SaveData
    {
        public static readonly string SavePath = "save";
        public static Instance Instance = new Instance();

        public static void Read()
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(SavePath, FileMode.Create)))
            {
                writer.Write(Instance.LevelID);
            }
        }
        public static void Save()
        {
            
        }
    }
}