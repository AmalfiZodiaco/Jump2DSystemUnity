using System.Collections.Generic;
using System;
using UnityEngine;

namespace Pearl.Events
{
    /// <summary>
    /// The abstract father of all the complex Manager (manager with components)
    /// </summary>
    public abstract class LogicalManager : MonoBehaviour
    {
        #region Protected Fields
        protected readonly Dictionary<Type, LogicalComponent> listComponents = new Dictionary<Type, LogicalComponent>();
        #endregion

        #region Unity CallBacks
        protected virtual void Awake()
        {
            CreateComponents();
        }

        protected virtual void OnDestroy()
        {
            foreach (LogicalComponent component in listComponents.Values)
            {
                component.OnDestroy();
            }
        }

        protected virtual void Update()
        {
            foreach (LogicalComponent component in listComponents.Values)
            {
                component.Update();
            }
        }
        #endregion

        #region Protected Methods
        protected  T GetLogicalComponent<T>() where T : LogicalComponent
        {
            return (T)listComponents[typeof(T)];
        }
        #endregion

        #region Abstract Methods
        /// <summary>
        /// Creation of the llstComponents dictionary elements
        /// </summary>
        protected virtual void CreateComponents()
        {

        }
        #endregion
    }
}