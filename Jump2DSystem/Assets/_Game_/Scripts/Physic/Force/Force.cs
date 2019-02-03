using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pearl.Demo
{
    public class Force
    {
        protected Vector2 force;
        private readonly string name;

        public string Name { get { return name; } }

        public Force(Vector2 force, string name)
        {
            this.force = force;
            this.name = name;
        }

        public virtual Vector2 GetForce()
        {
            return force;
        }

        public virtual bool IsTerminated()
        {
            return false;
        }
    }
}
