﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Frankenweenie
{
    public static class VirtualMouse
    {

        private static MouseState m_State;
        private static Vector2 m_Position;

        public static Vector2 Position => m_Position;
        public static MouseState State => m_State;


        public static Vector2 WorldPosition
        {
            get
            {
                Camera camera = (Camera)World.All<Camera>()[0];
                return Vector2.Transform(m_Position, Matrix.Invert(camera.Transform));
            }
        }
        public static void Update()
        {
            m_State = Mouse.GetState();
            m_Position = new Vector2(m_State.X, m_State.Y);
        }
    }
}
