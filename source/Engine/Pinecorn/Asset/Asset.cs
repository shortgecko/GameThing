﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;
using System.IO;
using Newtonsoft.Json;

namespace Pinecorn
{
    public class Asset
    {
        private static Dictionary<string, object> Assets = new Dictionary<string, object>();

        public static Texture2D Empty()
		{
			//initialize a texture
			var texture = new Texture2D(Engine.Device.GraphicsDevice, 1, 1);
			//the array holds the color for each pixel in the texture
			Color[] data = new Color[1];
            data[0] = Color.White;
			//set the color
            texture.SetData(data);
			return texture;
		}

        private static Texture2D EmptyTexture = Empty();
        public static void DrawRectangle(Rectangle rect, Color c)
        {
            Drawer.Batch.Draw(EmptyTexture, rect,c);
        }
        public static Texture2D Texture(string assetName)
        {
            var filepath = Engine.Config.AssetDirectory + "/" + assetName;
            if (string.IsNullOrEmpty(filepath))
            {
                throw new ArgumentNullException("Asset Name cannot be null or empty");
            }

            // On some platforms, name and slash direction matter.
            // We store the asset by a /-seperating key rather than how the
            // path to the file was passed to us to avoid
            // loading "content/asset1.xnb" and "content\\ASSET1.xnb" as if they were two 
            // different files. This matches stock XNA behavior.
            // The dictionary will ignore case differences
            //Coments Taken from MonoGame Content Pipeline
            var key = filepath.Replace('\\', '/');

            // Check for a previously loaded asset first
            object asset = null;
            if (Assets.TryGetValue(key, out asset))
            {
                if(asset is Texture2D)
                {
                    return (Texture2D)asset;
                }
                else
                {
                    return null;
                }
            }

            // Load the asset.
            var result = TexFromFile(key);
            Assets[key] = result;
            Logger.Log("[ASSET]" + "[TEXTURE] " + key);
            return result;
        }

        public static string Path(string path)
        {
            return AppDomain.CurrentDomain.BaseDirectory + "/" + path.Replace('\\', '/');        
        }


        public static string LoadFile(string assetName)
        {
            var filepath = Engine.Config.AssetDirectory + "/" + assetName;
            if (string.IsNullOrEmpty(filepath))
            {
                throw new ArgumentNullException("Asset Name cannot be null or empty");
            }

            // On some platforms, name and slash direction matter.
            // We store the asset by a /-seperating key rather than how the
            // path to the file was passed to us to avoid
            // loading "content/asset1.xnb" and "content\\ASSET1.xnb" as if they were two 
            // different files. This matches stock XNA behavior.
            // The dictionary will ignore case differences
            //Coments Taken from MonoGame Content Pipeline
            var key = filepath.Replace('\\', '/');

            // Check for a previously loaded asset first
            object asset = null;
            if (Assets.TryGetValue(key, out asset))
            {
                if(asset is string)
                {
                    return (string)asset;
                }
            }

            // Load the asset.
            var result = FromFile(key);
            Assets[key] = result;
            return result;
        }

        public static XmlDocument LoadXml(string assetName)
        {
            var filepath = Engine.Config.AssetDirectory + "/" + assetName;
            if (string.IsNullOrEmpty(filepath))
            {
                throw new ArgumentNullException("Asset Name cannot be null or empty");
            }

            // On some platforms, name and slash direction matter.
            // We store the asset by a /-seperating key rather than how the
            // path to the file was passed to us to avoid
            // loading "content/asset1.xnb" and "content\\ASSET1.xnb" as if they were two 
            // different files. This matches stock XNA behavior.
            // The dictionary will ignore case differences
            //Coments Taken from MonoGame Content Pipeline
            var key = filepath.Replace('\\', '/');

            // Check for a previously loaded asset first
            object asset = null;
            if (Assets.TryGetValue(key, out asset))
            {
                if(asset is XmlDocument)
                {
                    return (XmlDocument)asset;
                }
            }

            // Load the asset.
            var result = XMLFromFile(key);
            Assets[key] = result;
            return result;
        }

        private static Texture2D TexFromFile(string file)
        {
            using (System.IO.Stream titleStream = TitleContainer.OpenStream(file))
            {
                return Texture2D.FromStream(Engine.Device.GraphicsDevice, titleStream);
            }
        }

        public static void Debug()
        {
            Console.WriteLine("Assets :" + Assets.Count);
        }

        private static XmlDocument XMLFromFile(string filepath)
        {
          XmlDocument xml = new XmlDocument();
          xml.Load(TitleContainer.OpenStream(filepath));
          return xml;
        }

        private static string FromFile(string filepath)
        {
            using (var stream = TitleContainer.OpenStream(filepath))
            {
                using (var reader = new StreamReader(stream))
                {
                    string text = reader.ReadToEnd();
                    return text;
                }
            }
        }

    }
}
