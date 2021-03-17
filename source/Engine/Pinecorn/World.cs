using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pinecorn
{
    public class World
    {
        public List<Entity> Entities = new List<Entity>();
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
        public void DestoryEntity(int id)
        {
            Entities.Remove(Entities[id]);
        }

        public void Add(Entity entity)
        {
            entity.World = this;
            for (int i = 0; i < entity.Components.Count; i++)
                entity.Components[i].Initialize();
            this.Entities.Add(entity);
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