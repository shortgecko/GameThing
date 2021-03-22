using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Frankenweenie;
using System;
using System.Collections.Generic;
using MonoGame.Framework.Utilities.Deflate;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Game
{
    public class Instance
    {
        public int Health;
        public float FileTime;
        public int LevelID;
        
        public bool Paused = false;

    }


    public static class GameState
    {
        public static readonly string SavePath = "save1.save";
        private static Instance Instance = new Instance();
        public static bool Paused => Instance.Paused;

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