namespace Pearl.Events
{
    /// <summary>
    /// The father of all the components of the manager
    /// </summary>
    public abstract class LogicalComponent
    {
        #region Unity CallBacks
        /// <summary>
        /// A method called when the manager is destroyed
        /// </summary>
        public virtual void OnDestroy()
        {
        }

        public virtual void Update()
        {
        }
        #endregion
    }
}
