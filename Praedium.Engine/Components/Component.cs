﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praedium.Engine.Components
{
    public abstract class Component
    {
        public bool Enabled
        {
            get;
            set;
        }

        public Component()
        {
            Enabled = true;
        }

        public void AttachTo(GameObject obj)
        {
            GameObject = obj;

            Attribute[] attrs = Attribute.GetCustomAttributes(this.GetType());

            foreach (RequireComponent item in attrs)
            {
                foreach (Type componentType in item.ComponentTypes)
                {
                    // Add new component only when it's needed
                    if (GameObject.GetComponentOfType(componentType) != null)
                    {
                        Component instance = Activator.CreateInstance(componentType) as Component;
                        GameObject.AddComponent(instance);
                    }
                }
            }

            OnAttached();
        }

        public virtual void OnAttached()
        { }

        public GameObject GameObject
        {
            get;
            private set;
        }

        public Game Game
        {
            get
            {
                return GameObject.Game;
            }
        }

        /// <summary>
        /// When the  components are initialized, they can begin their custom setup by taking advantage of overriding this method
        /// </summary>
        protected virtual void OnStart()
        { }

        public void Start()
        {
            OnStart();
        }

        public abstract void Update();
    }
}
