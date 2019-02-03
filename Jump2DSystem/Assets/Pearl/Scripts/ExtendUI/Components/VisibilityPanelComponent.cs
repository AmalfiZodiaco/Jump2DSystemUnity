using UnityEngine;
using Pearl.Multitags;
using Pearl.Events;

namespace Pearl.UI
{
    /// <summary>
    /// This component is responsible for activating or deactivating panels
    /// </summary>
    public class VisibilityPanelComponent : LogicalComponent
    {
        #region Private Fields
        private GameObject currentPanel;
        #endregion

        #region Auxiliar Panels
        private GameObject auxPanel;
        #endregion

        #region Obey Methods
        /// <summary>
        /// Check if the object panel is the same as the active panel.
        /// </summary>
        /// /// <param name = "obj"> The object to check</param>
        public bool ObeyIsSamePanel(GameObject obj)
        {
            auxPanel = FindPanelForSpecificUIObj(obj.transform).gameObject;
            return currentPanel.GetInstanceID() == auxPanel.GetInstanceID();
        }

        /// <summary>
        /// Check if the object panel is the same as the active panel.
        /// </summary>
        /// /// <param name = "obj"> The object to check</param>
        public void ObeyShow(GameObject obj)
        {
            currentPanel = FindPanelForSpecificUIObj(obj.transform)?.gameObject;
            ObeyOpenOrCloseAllPanels(false, currentPanel.transform.parent);

            UnityEngine.Debug.Assert(currentPanel, "There isn't panel");

            currentPanel.SetActive(true);
        }

        /// <summary>
        /// Open or close all panels
        /// </summary>
        /// /// <param name = "obj"> The object to check</param>
        public void ObeyOpenOrCloseAllPanels(bool open, Transform transform)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(open);
            }
        }
        #endregion

        #region Private Methods
        private Transform FindPanelForSpecificUIObj(Transform transform)
        {
            if (transform.gameObject.HasTags(Tags.Panel))
                return transform;
            else if (transform.parent != null)
                return FindPanelForSpecificUIObj(transform.parent);
            else
                return null;
        }
        #endregion
    }
}
