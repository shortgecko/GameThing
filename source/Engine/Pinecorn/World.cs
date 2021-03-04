using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pinecorn
{
    public class World
    {
        public List<Entity> Entities = new List<Entity>();
        public void Initialize()
        {
            for(int i = 0; i < Entities.Count; i++)
            {
                for (int j = 0; j < Entities[i].Components.Count; j++)
                {
                    Entities[i].Components[j].Initialize();
                }
            }
                        
        }

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

        public void AddEntity(Entity entity)
        {
            entity.World = this;
            this.Entities.Add(entity);
        }

        public void Log()
        {
            Console.WriteLine("Entities: " + Entities.Count);
        }

    }
}