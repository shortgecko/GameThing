using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using IO = System.IO;
using SpriteFontPlus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Frankenweenie
{
    public class Content
    {
        private static Dictionary<string, object> loadedAssets = new Dictionary<string, object>();
        public static Texture2D Empty
        {
            get
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
        }
        public static Texture2D Rectangle(int width, int height, Color color)
        {
            //initialize a texture
            var texture = new Texture2D(Engine.Device.GraphicsDevice, width, height);
            //the array holds the color for each pixel in the texture
            Color[] data = new Color[width * height];
            for (int i = 0; i < data.Length; i++)
                data[i] = color;
            //set the color
            texture.SetData(data);
            return texture;
        }
        private static string Directory(string input)
        {
            var filepath = IO.Path.Combine(Engine.AssetDirectory + "/" + input);
            if (string.IsNullOrEmpty(filepath))
            {
                throw new ArgumentNullException("Asset Name cannot be null or empty");
            }

            return filepath.Replace('\\', '/');
        }

        public static void Dispose()
        {
            Logger.Log("[Content] Disposing assets..");
            foreach(var assetName in loadedAssets.Keys)
            {
                var input = loadedAssets[assetName];
                if(input is IDisposable)
                {
                    var asset = (IDisposable)input;
                    asset.Dispose();
                    asset = null;
                }
                input = null;
            }
            loadedAssets.Clear();
            Logger.Log("[CONTENT] Assets disposed");

        }

        public static Texture2D Texture(string assetName)
        {
            var key = Directory(assetName);
            // Check for a previously loaded asset first
            object asset = null;
            if (loadedAssets.TryGetValue(key, out asset))
            {
                if (asset is Texture2D)
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
            loadedAssets[key] = result;
            Logger.Log("[CONTENT]" + "[TEXTURE] " + key);
            return result;
        }
        public static string Path(string path)
        {
            return IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "/" + path.Replace('\\', '/'));
        }
        public static string LoadFile(string assetName)
        {
            var key = Directory(assetName);

            object asset = null;
            if (loadedAssets.TryGetValue(key, out asset))
            {
                if (asset is string)
                {
                    return (string)asset;
                }
            }

            // Load the asset.
            var result = FromFile(key);
            loadedAssets[key] = result;
            return result;
        }
        public static XmlDocument LoadXml(string assetName)
        {
            var key = Directory(assetName);

            // Check for a previously loaded asset first
            object asset = null;
            if (loadedAssets.TryGetValue(key, out asset))
            {
                if (asset is XmlDocument)
                {
                    return (XmlDocument)asset;
                }
            }

            // Load the asset.
            var result = XMLFromFile(key);
            loadedAssets[key] = result;
            return result;
        }
        private static Texture2D TexFromFile(string file)
        {
            using (System.IO.Stream titleStream = TitleContainer.OpenStream(file))
            {
                return Texture2D.FromStream(Engine.Device.GraphicsDevice, titleStream);
            }
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
        public static string TitlePath(string path)
        {
            return Engine.AssetDirectory + "/" + path;
        }

        public static SpriteFont Font(string assetName, float size)
        {
            var key = Directory(assetName);
            object asset = null;
            if (loadedAssets.TryGetValue(key, out asset))
            {
                if (asset is SpriteFont)
                {
                    return (SpriteFont)asset;
                }
            }

            // Load the asset.
            var result = LoadFont(assetName, size);
            loadedAssets[key] = result;
            return result;
        }

        private static SpriteFont LoadFont(string file, float size)
        {
            var fontBakeResult = TtfFontBaker.Bake(File.ReadAllBytes(@"C:\\Windows\\Fonts\arial.ttf"),
            size,
            1024,
            1024,
            new[]
            {
                CharacterRange.BasicLatin,
                CharacterRange.Latin1Supplement,
                CharacterRange.LatinExtendedA,
                CharacterRange.Cyrillic
            }
        );

            return fontBakeResult.CreateSpriteFont(Engine.Device.GraphicsDevice);
        }


        //private static Song LoadSong(string file)
        //{
        //   //var uri = new Uri(file);
        //   // Song.FromUri("song", uri);
        //}

    }
}
