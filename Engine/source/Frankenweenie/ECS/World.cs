using System;
using System.Collections.Generic;
using System.Linq;

namespace Frankenweenie
{
    public class World
    {
        private static Registry<Entity> Entities = new Registry<Entity>();
        public static Dictionary<Type, List<Component>> ComponentRegistry = new Dictionary<Type, List<Component>>();
        
        public static int Count => Entities.Count;

        public static List<Component> All<T>() where T : Component
        {
            Type t = typeof(T);
            return ComponentRegistry[t];
        }

        public static void AddToComponentRegistry(Component c)
        {
            Type t = c.GetType();
            if(!ComponentRegistry.ContainsKey(t))
            {
                ComponentRegistry.Add(t, new List<Component>());
            }
            ComponentRegistry[t].Add(c);
        }

        public static void RemoveFromComponentRegistry(Component c)
        {
            Type t = c.GetType();
            if(ComponentRegistry.ContainsKey(t))
                ComponentRegistry[t].Remove(c);
        }

        public static void Add(Entity Entity)
        {
            World.Entities.Add(Entity);
        }

        public static void Remove(Entity Entity)
        {          
            World.Entities.Remove(Entity);
        }
        
        static void UpdateRegistry(Entity e)
        {
            if(e.Components.Adding.Count > 1)
            {
                for (int i = 0; i < e.Components.Adding.Count; i++)
                {
                    Component component = e.Components.Adding[i];
                    component.Initialize();
                }
            }

            if (e.Components.Removing.Count > 1)
            {
                Logger.Log("Removing");
                for (int i = 0; i < e.Components.Removing.Count; i++)
                {
                    Component component = e.Components.Removing[i];
                    component.Removed();
                    Pooler.EntityRemoved(component);
                    Logger.Log();
                }
            }

            e.Components.UpdateList();

        }
        public static void Update()
        {
           Entities.UpdateList();

            foreach (Entity Entity in Entities)
            {
                UpdateRegistry(Entity);

                foreach(Component Component in Entity.Components)
                {
                    Component.Update();
                }
                
            }

        }

        public static void Render()
        {
            foreach (Entity entity in Entities)
            {
                foreach (Component component in entity.Components)
                {
                    component.Render();
                }

            }

        }

        public static void Clear()
        {
            
        }
    }

}