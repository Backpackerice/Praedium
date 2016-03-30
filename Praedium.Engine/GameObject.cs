﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bramble.Core;
using Malison.Core;
using Praedium.Engine.Components;

namespace Praedium.Engine
{
    public abstract class GameObject
    {
        public GameObject()
        {
            Position = Vector2D.Zero;

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
                        instance.AttachTo(this);
                        Components.Add(instance);
                    }
                }
            }
        }

        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// When the game objects initialized, they can begin their custom setup by taking advantage of overriding this method
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

        public Game Game
        {
            get;
            set;
        }

        public Vector2D Position
        {
            get;
            set;
        }

        protected List<Component> Components;

        public void AddComponent(Component component)
        {
            Components.Add(component);
        }

        public Component GetComponentOfType(Type type)
        {
            return Components.Find(x => x.GetType().IsAssignableFrom(type));
        }

        public IList<Component> GetComponentsOfType(Type type)
        {
            return Components.FindAll(x => x.GetType().IsAssignableFrom(type)).ToList();
        }

        public abstract void Render(ITerminal terminal);
    }
}
