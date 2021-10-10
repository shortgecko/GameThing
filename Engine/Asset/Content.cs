using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using IO = System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace Frankenweenie
{
    public class Content
    {
        private static Dictionary<string, object> loadedAssets = new Dictionary<string, object>();
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
            Logger.Log("[Content] Assets disposed");

        }
        public static string Path
        {
            get
            {
                return Engine.AssetDirectory;
            }
        }       

        #region TextureFunc
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
        public static VTexture CreateTexture(int width, int height, Color color)
        {
            //initialize a texture
            var texture = new Texture2D(Engine.Device.GraphicsDevice, width, height);
            texture.Name = "Blank";
            //the array holds the color for each pixel in the texture
            Color[] data = new Color[width * height];
            for (int i = 0; i < data.Length; i++)
                data[i] = color;
            //set the color
            texture.SetData(data);
            return new VTexture(texture);
        }
        #endregion
        #region Texture
        public static  VTexture LoadTexture(string assetName)
        {
            var key = Directory(assetName);
            // Check for a previously loaded asset first
            object asset = null;
            if(GetLoaded<VTexture>(key, out asset))
                return (VTexture)asset;
            // Load the asset.

            Texture2D texture;
            using (System.IO.Stream titleStream = TitleContainer.OpenStream(key))
            {
                texture = Texture2D.FromStream(Engine.Device.GraphicsDevice, titleStream);
            }
            var result = new VTexture(texture);
            loadedAssets[key] = result;
            Logger.Log($"Loaded {key}");
            return result;
        }
        #endregion
        #region XML
        public static XmlDocument LoadXml(string assetName)
        {
            var key = Directory(assetName);

            // Check for a previously loaded asset first
            object asset = null;
            if (GetLoaded<XmlDataDocument>(key, out asset))
                return (XmlDocument)asset;

            XmlDocument xml = new XmlDocument();
            xml.Load(TitleContainer.OpenStream(key));
            // Load the asset.
            var result = xml;
            loadedAssets[key] = result;
            return result;
        }

        #endregion
        #region Ogmo
        public static OgmoLevel LoadLevel(string assetName)
        {
            var key = Directory(assetName);

            // Check for a previously loaded asset first
            object asset = null;
            if (GetLoaded<OgmoLevel>(key, out asset))
                return (OgmoLevel)asset;

            // Load the asset.
             //Console.WriteLine(Engine.AssetDirectory);
            var result = new OgmoLevel(TitleContainer.OpenStream($"{key}")); 
            loadedAssets[key] = result;
            return result;
        }
        #endregion
        #region Song
        public static AudioFile LoadSong(string assetName)
        {
            var key = Directory(assetName);
            object asset = null;
            if (GetLoaded<AudioFile>(key, out asset))
                return (AudioFile)asset;

            if(!File.Exists(key))
            {
                Console.WriteLine($"File {key} does not exist");
            }
            AudioFile vSong = new AudioFile(key);
            // Load the asset.
            var result = vSong;
            loadedAssets[key] = result;
            return result;
        }
        #endregion
        #region SpriteFont
        //public static SpriteFont Font(string assetName, float size)
        //{
        //    var key = Directory(assetName);
        //    object asset = null;

        //    if (GetLoaded<SpriteFont>(key, out asset))
        //        return (SpriteFont)asset;

        //    // Load the asset.
        //    var result = LoadFont(assetName, size);
        //    loadedAssets[key] = result;
        //    return result;
        //}
        #endregion
        #region FileLoading
        private static string ReadFile(string filepath)
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
            var result = ReadFile(key);
            loadedAssets[key] = result;
            return result;
        }
        #endregion
        #region Helper
        private static bool GetLoaded<T>(string key, out object asset)
        {
            if (loadedAssets.TryGetValue(key, out asset))
            {
                if (asset is T)
                    return true;
                else
                    return false;
            }
            return false;
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
        #endregion
    }
}
