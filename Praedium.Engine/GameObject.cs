﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bramble.Core;
using Malison.Core;
using Praedium.Engine.Components;
using SFML.Graphics;

namespace Praedium.Engine
{
    public abstract class GameObject
    {
        public GameObject()
        {
            Components = new List<Component>();

            AttachComponents();
        }

        private void AttachComponents()
        {
            Attribute[] attrs = Attribute.GetCustomAttributes(this.GetType());

            foreach (Attribute item in attrs)
            {
                RequireComponent componentAttribute = item as RequireComponent;

                if (componentAttribute == null)
                    continue;

                foreach (Type componentType in componentAttribute.ComponentTypes)
                {
                    // Add new component only when it's needed
                    if (GetComponentOfType(componentType) == null)
                    {
                        Component instance = Activator.CreateInstance(componentType) as Component;
                        AddComponent(instance);
                    }
                }
            }
        }

        protected virtual void OnDestroy() { }

        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// When the game objects get initialized, they can begin their custom setup by taking advantage of overriding this method
        /// </summary>
        protected abstract void OnStart();
        
        public void Start()
        {
            OnStart();

            foreach (var component in Components)
            {
                component.Start();
            }
        }

        public void Update()
        {
            foreach (var component in Components)
            {
                component.Update();                
            }
        }

        public void Render()
        {
            foreach (Renderer component in Components.OfType<Renderer>().Where(x => x.Enabled))
            {
                component.Render();
            }
        }

        public Game Game
        {
            get;
            set;
        }

        public virtual Vector2D Position
        {
            get;
            set;
        }

        protected List<Component> Components;

        public void AddComponent(Component component)
        {
            Components.Add(component);
            component.AttachTo(this);
        }

        public Component GetComponentOfType(Type type)
        {
            return Components.Find(x => x.GetType().IsSubclassOf(type) || x.GetType() == type);
        }

        public Component GetComponent<T>()
        {
            return GetComponentOfType(typeof(T));
        }

        public IEnumerable<Component> GetComponentsOfType(Type type)
        {
            return Components.FindAll(x => x.GetType().IsSubclassOf(type) || x.GetType() == type);
        }

        public IEnumerable<Component> GetComponents<T>()
        {
            return GetComponentsOfType(typeof(T));
        }

        public void Destroy()
        {
            OnDestroy();
            Game.Destroy(this);
        }
    }
}
