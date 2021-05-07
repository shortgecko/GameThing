using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using IO = System.IO;

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
        public static Texture2D CreateTexture(int width, int height, Color color)
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
        #endregion
        #region Texture
        private static Texture2D TexFromFile(string file)
        {
            using (System.IO.Stream titleStream = TitleContainer.OpenStream(file))
            {
                return Texture2D.FromStream(Engine.Device.GraphicsDevice, titleStream);
            }
        }
        public static Texture2D LoadTexture(string assetName)
        {
            var key = Directory(assetName);
            // Check for a previously loaded asset first
            object asset = null;
            if(GetLoaded<Texture2D>(key, out asset))
                return (Texture2D)asset;
            // Load the asset.
            var result = TexFromFile(key);
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

            // Load the asset.
            var result = XMLFromFile(key);
            loadedAssets[key] = result;
            return result;
        }
        private static XmlDocument XMLFromFile(string filepath)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(TitleContainer.OpenStream(filepath));
            return xml;
        }
        #endregion
        #region Ogmo
        public static OgmoLevel LoadOgmo(string assetName)
        {
            var key = Directory(assetName);

            // Check for a previously loaded asset first
            object asset = null;
            if (GetLoaded<OgmoLevel>(key, out asset))
                return (OgmoLevel)asset;

            // Load the asset.
            var result = LoadOgmoLevelFromFile(key);
            loadedAssets[key] = result;
            return result;
        }
        private static OgmoLevel LoadOgmoLevelFromFile(string path)
        {
            Logger.Log(path);
            OgmoLevel level = new OgmoLevel(TitleContainer.OpenStream($"{path}"));
            return level;
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
