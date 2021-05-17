using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frankenweenie;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using SpriteFontPlus;

namespace Game
{
    public class GUI : Scene
    {
        string text = string.Empty;

        SpriteFont Font;
        private static float delayTime;
        private static bool input;
        private static bool IsLetter(string c)
        {
            switch (c)
            {
                case "A":
                case "B":
                case "C":
                case "D":
                case "E":
                case "F":
                case "G":
                case "H":
                case "I":
                case "J":
                case "K":
                case "L":
                case "M":
                case "N":
                case "O":
                case "P":
                case "Q":
                case "R":
                case "S":
                case "T":
                case "U":
                case "V":
                case "W":
                case "X":
                case "Y":
                case "Z":
                    return true;
                default: return false;
            }
        }
        private static float delayTimer = 0;


        protected override void Load()
        {

            var fontBakeResult = TtfFontBaker.Bake(File.ReadAllBytes(@"C:\\Windows\\Fonts\arial.ttf"),
                25,
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

            Font = fontBakeResult.CreateSpriteFont(Engine.Device.GraphicsDevice);
        }

        private static void startDelay()
        {
            delayTimer = 0.15f;
            input = false;
        }

        public static void Text(ref string output)
        {
            var keyboardState = Keyboard.GetState();
            var keys = keyboardState.GetPressedKeys();


            if (input)
            {

                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    output += "\n";
                    startDelay();
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    output += " ";
                    startDelay();
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Back))
                {
                    if (output.Length > 0)
                    {
                        output = output.Remove(output.Length - 1);
                        startDelay();
                    }
                }

                if (keys.Length > 0)
                {
                    var keyValue = keys[0].ToString();
                    if (IsLetter(keyValue))
                        output += keyValue;
                    startDelay();
                }
            }

            delayTimer -= Engine.Delta;

            if (delayTimer < 0)
                input = true;


        }

        int len = 0;

        protected override void Update()
        {
            Text(ref text);

            if (len != text.Length)
            {
                Console.Clear();
                Console.Write(text);
                len = text.Length;
            }

            base.Update();
        }

        protected override void Render()
        {
            Drawer.Batch.DrawString(Font, text, Vector2.Zero, Color.Red);
            Drawer.Rect(new Rectangle((text.Length - 2) * 25, 0, 2, 25), Color.White);
        }
    }
}
