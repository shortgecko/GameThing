using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Frankenweenie
{

    public class Particle
    {
        public Texture2D Texture { get; set; }        // The texture that will be drawn to represent the particle
        public Vector2 Position { get; set; }        // The current position of the particle        
        public Vector2 Velocity { get; set; }        // The speed of the particle at the current instance
        public float Angle { get; set; }            // The current angle of rotation of the particle
        public float AngularVelocity { get; set; }    // The speed that the angle is changing
        public Color Color { get; set; }            // The color of the particle
        public float Size { get; set; }              // The size of the particle
        public int Lifetime { get; set; }                // The 'time to live' of the partic

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, float angle, float angularVelocity, Color color, float size, int ttl)
        {
            Texture = texture;
            Position = position;
            Velocity = velocity;
            Angle = angle;
            AngularVelocity = angularVelocity;
            Color = color;
            Size = size;
            Lifetime = ttl;
        }

        public void Update()
        {
            Lifetime--;
            Position += Velocity;
            Angle += AngularVelocity;
        }

        public void Render()
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            Drawer.Batch.Draw(Texture, Position, sourceRectangle, Color, Angle, origin, Size, SpriteEffects.None, 0f);
        }
    }
    public class ParticleSystem : Component
    {
        private Random Random = new Random();
        public Vector2 EmitterLocation { get; set; }
        private List<Particle> Particles = new List<Particle>();
        private List<Texture2D> Textures;

        public ParticleSystem(List<Texture2D> textures, Vector2 location)
        {
            EmitterLocation = location;
            Textures = textures;
        }

        private Particle GenerateNewParticle()
        {
            Texture2D texture = Textures[Random.Next(Textures.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(1f * (float)(Random.NextDouble() * 2 - 1), 1f * (float)(Random.NextDouble() * 2 - 1));
            float angle = 0;
            float angularVelocity = 0.1f * (float)(Random.NextDouble() * 2 - 1);
            Color color = new Color((float)Random.NextDouble(), (float)Random.NextDouble(), (float)Random.NextDouble());
            float size = (float)Random.NextDouble();
            int ttl = 20 + Random.Next(40);
            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }

        public override void Update()
        {
            //Max particles at a time
            int total = 10;

            for (int i = 0; i < total; i++)
            {
                Particles.Add(GenerateNewParticle());
            }

            for (int particle = 0; particle < Particles.Count; particle++)
            {
                Particles[particle].Update();
                //Remove particles
                if (Particles[particle].Lifetime <= 0)
                {
                    Particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        public override void Render()
        {
            for (int index = 0; index < Particles.Count; index++)
            {
                Particles[index].Render();
            }
        }


    }
}
