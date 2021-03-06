﻿namespace Pearl.Graph
{
    public struct Arch<T>
    {
        #region Private fields
        private Vertex<T> vertex;
        private int weight;
        #endregion

        #region Constructors
        public Arch(Vertex<T> vertex, int weight)
        {
            this.vertex = vertex;
            this.weight = weight;
        }
        #endregion
    }
}
