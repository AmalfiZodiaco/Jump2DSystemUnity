using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Pearl.Events
{
    /// <summary>
    /// This static class manages the communication between different gameobjects in a centralized way. 
    /// For example, the gameobject "A" must receive instructions from the gamebject "B": 
    /// A subscribes to an event "eV" of the "EventsManager" class. When "B" wants to call "A", it 
    /// invokes the "eV" event of the "EventsManager" class with the necessary parameters.
    /// Event "eV" invokes object "A" and all other objects subscribed to "eV".
    /// The events are distinguished by an enumerator (EventAction). 
    /// Moreover the class manages singleton.
    /// </summary>
    public static class EventsManager
    {
        #region Readonly fields
        private static readonly Dictionary<EventAction, Delegate> dictionaryEvent;
        private static readonly Dictionary<EventFastAction, Delegate> dictionaryFastEvent;
        private static readonly Dictionary<Type, MonoBehaviour> dictionarySingleton;
        private static readonly Dictionary<ElementDictionaryDelegate, MonoBehaviour> dictionaryDelegate;
        #endregion

        #region Auxiliar Fields
        private static Delegate[] auxArrayDelegate;
        private static ElementDictionaryDelegate auxElement;
        #endregion

        #region Constructors
        static EventsManager()
        {
            dictionarySingleton = new Dictionary<Type, MonoBehaviour>();
            dictionaryDelegate = new Dictionary<ElementDictionaryDelegate, MonoBehaviour>();
            dictionaryEvent = new Dictionary<EventAction, Delegate>();
            dictionaryFastEvent = new Dictionary<EventFastAction, Delegate>();

            CreateDictonary();
        }
        #endregion

        #region Init Methods
        private static void CreateDictonary()
        {
            int lenght = EnumExtend.Length<EventAction>();

            for (int i = 0; i < lenght; i++)
                dictionaryEvent.Add((EventAction)i, null);

            lenght = EnumExtend.Length<EventFastAction>();

            for (int i = 0; i < lenght; i++)
                dictionaryFastEvent.Add((EventFastAction)i, null);
        }
        #endregion

        #region Singleton manager
        private static void AddSingleton<T>(MonoBehaviour[] istance) where T : MonoBehaviour
        {
            UnityEngine.Debug.Assert(istance.Length == 1, "There isn't Singleton or there too istances of singleton");
            dictionarySingleton.Update(typeof(T), istance[0]);
        }

        private static bool IsntThereSingleton<T>()
        {
            return !dictionarySingleton.ContainsKey(typeof(T)) || dictionarySingleton[typeof(T)] == null;
        }

        /// <summary>
        ///  Returns the T singleton istance
        /// </summary>
        public static T GetIstance<T>() where T : MonoBehaviour, ISingleton
        {
            if (IsntThereSingleton<T>())
                AddSingleton<T>(GameObject.FindObjectsOfType<T>());
            return (T) dictionarySingleton[typeof(T)];
        }
        #endregion

        #region Add method in event
        /// <summary>
        /// The function adds a fast action (method that returns void) with zero parameters to a specific event
        /// </summary>
        /// <param name = "eventAction">The delegate  associated with the event</param>
        /// <param name = "action">The action</param>
        public static void AddMethod<T>(EventFastAction eventAction, T action) where T : Delegate
        {
            dictionaryFastEvent[eventAction] = Delegate.Combine(dictionaryFastEvent[eventAction], action);
        }

        /// <summary>
        /// The function adds a action (method that returns void) with zero parameters to a specific event
        /// </summary>
        /// <param name = "eventAction">The delegate  associated with the event</param>
        /// <param name = "action">The action</param>
        /// <param name = "mono">The eventsmanager will check each event call if the object has been destroyed(warning, this option will slow down the process).</param>
        public static void AddMethod<T>(EventAction eventAction, T action, MonoBehaviour mono) where T : Delegate
        {
            AddMonoBheaviour(eventAction, action, mono);
            dictionaryEvent[eventAction] = Delegate.Combine(dictionaryEvent[eventAction], action);
        }
        #endregion

        #region Remove method in event
        /// <summary>
        /// The function remove a fast action (method that returns void) with two parameters to a specific event
        /// </summary>
        /// <param name = "eventAction">The delegate associated with the event</param>
        /// <param name = "action">The action</param>
        public static void RemoveMethod<T>(EventFastAction eventAction, T action) where T : Delegate
        {
            dictionaryFastEvent[eventAction] = Delegate.Remove(dictionaryFastEvent[eventAction], action);
        }

        /// <summary>
        /// The function remove a action (method that returns void) with two parameters to a specific event
        /// </summary>
        /// <param name = "eventAction">The delegate associated with the event</param>
        /// <param name = "action">The action</param>
        public static void RemoveMethod<T>(EventAction eventAction, T action) where T : Delegate
        {
            dictionaryEvent[eventAction] = Delegate.Remove(dictionaryEvent[eventAction], action);
        }

        #endregion

        #region Call events
        /// <summary>
        /// This function calls the specific fast event to activate all its subscribed methods.
        /// Beware of fast events, since they do not check if their MonoBehaviour has been destroyed, 
        /// the user must remove the method manually (using the RemoveMethod function).
        /// <param name = "eventAction">The dictionary key associated with the event</param>
        /// /// <param name = "objects">The parameters for invoke</param>
        /// </summary>
        public static void CallEvent(EventFastAction action, params System.Object[] objects)
        {
            dictionaryFastEvent[action]?.DynamicInvoke(objects);
        }

        /// <summary>
        /// This function calls the specific event to activate all its subscribed methods.
        /// The method also checks to see if the gameobject of the method is destroyed, 
        /// and if so, deletes the method in its invocation list.
        /// <param name = "eventAction">The dictionary key associated with the event</param>
        /// /// <param name = "objects">The parameters for invoke</param>
        /// </summary>
        public static void CallEvent(EventAction action, params System.Object[] objects)
        {
            if (dictionaryEvent[action] == null)
                return;

            ControlObjectDestroyed(action);
            dictionaryEvent[action].DynamicInvoke(objects);
        }
        #endregion

        #region Private Methods
        private static void ControlObjectDestroyed(EventAction action)
        {
            auxArrayDelegate = dictionaryEvent[action].GetInvocationList();
            for (int i = 0; i < auxArrayDelegate.Length; i++)
            {
                auxElement.Set(action, auxArrayDelegate[i]);
                if (dictionaryDelegate[auxElement] == null)
                    DeleteElement(action, auxElement, auxArrayDelegate[i]);
            }
        }

        private static void DeleteElement(EventAction action, ElementDictionaryDelegate el, Delegate del)
        {
            dictionaryDelegate.Remove(el);
            RemoveMethod(action, del);
        }

        private static void AddMonoBheaviour(EventAction eventAction,Delegate action, MonoBehaviour mono)
        {
            auxElement.Set(eventAction, action);
            dictionaryDelegate.Add(auxElement, mono);
        }

        private struct ElementDictionaryDelegate
        {
            private EventAction ev;
            private Delegate del;

            public void Set(EventAction ev, Delegate del)
            {
                this.ev = ev;
                this.del = del;
            }

            public override bool Equals(object obj)
            {
                if (obj is ElementDictionaryDelegate element)
                    return element.ev == ev && element.del == del;
                else
                    return false;
            }

            public override int GetHashCode()
            {
                var hashCode = 465903548;
                hashCode = hashCode * -1521134295 + ev.GetHashCode();
                hashCode = hashCode * -1521134295 + EqualityComparer<Delegate>.Default.GetHashCode(del);
                return hashCode;
            }
        }
        #endregion
    }
}
