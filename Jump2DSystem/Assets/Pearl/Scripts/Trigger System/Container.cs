using Pearl.Trigger;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pearl.Trigger
{
    public abstract class Container : MonoBehaviour
    {
        protected Dictionary<string, Delegate> setDictonary;

        public Dictionary<string, object> Informations { get; private set; }

        private void Awake()
        {
            setDictonary = new Dictionary<string, Delegate>();

            Informations = new Dictionary<string, object>();
            SetDictonary();
        }


        protected abstract void SetDictonary();

        public T Take<T>(string name)
        {
            return (T)Informations[name];
        }

        public Delegate Set(string key)
        {
            return setDictonary[key];
        }
    }
}
