using System;
using System.Collections.Generic;
using System.Linq;

namespace Frankenweenie
{
    public class World
    {
        private static Registry<Entity> EntityRegistry = new Registry<Entity>();
        public static Dictionary<Type, List<Component>> ComponentRegistry = new Dictionary<Type, List<Component>>();
        public static List<Entity> Entities
        {
            get
            {
                return EntityRegistry.ToList();
            }
        }
        public static int Count => EntityRegistry.Count;

        public static List<Component> All<T>() where T : Component
        {
            Type t = typeof(T);
            if(!ComponentRegistry.ContainsKey(t))
            {
                Logger.Log("Type not found in registry");
                ComponentRegistry.Add(t, new List<Component>());
            }
           return ComponentRegistry[t];

        }

        public static void AddToRegistry(Component c)
        {
            Type t = c.GetType();
            if(!ComponentRegistry.ContainsKey(t))
            {
                ComponentRegistry.Add(t, new List<Component>());
            }
            Logger.Log(t.Name);
            ComponentRegistry[t].Add(c);
        }

        public static void RemoveFromRegistry(Component c)
        {
            Type t = c.GetType();
            if(ComponentRegistry.ContainsKey(t))
                ComponentRegistry[t].Remove(c);
        }

        public static void Add(Entity Entity)
        {
            EntityRegistry.Add(Entity);
            for (int i = 0; i < Entity.Components.Adding.Count; i++)
            {
                AddToRegistry(Entity.Components.Adding[i]);
            }


        }

        public static void Remove(Entity Entity)
        {
            Entity.Clear();
            World.EntityRegistry.Remove(Entity);
            Entity = null;
        }

        static void UpdateRegistry(Entity Entity)
        {

            if (Entity.Components.Adding.Count > 0)
            {
                for (int i = 0; i < Entity.Components.Adding.Count; i++)
                {
                    Entity.Components.Adding[i].Initialize();
                }

                for (int i = 0; i < Entity.Components.Adding.Count; i++)
                {
                    AddToRegistry(Entity.Components.Adding[i]);
                }
            }

            if (Entity.Components.Removing.Count > 0)
            {
                for (int i = 0; i < Entity.Components.Removing.Count; i++)
                {
                    Component component = Entity.Components.Removing[i];
                    component.Removed();
                    Pooler.EntityRemoved(component);
                }

                for (int i = 0; i < Entity.Components.Removing.Count; i++)
                {
                    Component component = Entity.Components.Removing[i];
                    RemoveFromRegistry(component);

                }
            }

            Entity.Components.UpdateList();

        }

        public static void Update()
        {
           EntityRegistry.UpdateList();
            foreach (Entity Entity in EntityRegistry)
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
            foreach (Entity entity in EntityRegistry)
            {
                foreach (Component component in entity.Components)
                {
                    component.Render();
                }

            }

        }

        public static void Clear()
        {
            foreach (Entity entity in EntityRegistry)
                Remove(entity);
        }

    }

}