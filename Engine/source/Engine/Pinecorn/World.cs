using System;
using System.Collections.Generic;
using System.Linq;

namespace Frankenweenie
{
    public class World
    {
        public static List<Entity> Entities = new List<Entity>();
        public void Update()
        {
            for (int i = 0; i < Entities.Count; i++)
            {
                for (int j = 0; j < Entities[i].Components.Count; j++)
                {
                    Entities[i].Components[j].Update();
                }
            }
        }

        public void Render()
        {
            for (int i = 0; i < Entities.Count; i++)
            {
                for (int j = 0; j < Entities[i].Components.Count; j++)
                {
                    Entities[i].Components[j].Render();
                }
            }
        }
        public Entity GetEntity(int id)
        {
            return Entities[id];
        }
        public Entity GetEntity(string name)
        {
            return Entities.FirstOrDefault(e => e.Name == name);
        }
        public void DestoryEntity(int id)
        {
            Entities.Remove(Entities[id]);
        }

        public static void Add(Entity entity)
        {
            for (int i = 0; i < entity.Components.Count; i++)
                entity.Components[i].Initialize();
            Entities.Add(entity);
        }
        public void Log()
        {
            Console.WriteLine("Entities: " + Entities.Count);
        }

        public Entity get(string name)
        {
            return Entities.FirstOrDefault(c => c.Name == name);
        }

    }
}