using UnityEngine;
using UnityEngine.AI;
using AI;

public class PathFinding : IState
{
    #region Private Fields
    private Transform tr;
    private NavMeshAgent agent;
    private int i = 0;
    #endregion

    #region Constructors
    public PathFinding(Transform tr, NavMeshAgent agent)
    {
        this.tr = tr;
        this.agent = agent;
    }
    #endregion

    #region Override Methods
    public void Enter()
    {
    }

    public bool Execute()
    {
        agent.destination = tr.position;
        if (i < 1)
        {
            i++;
            return false;
        }
        else
            return !agent.hasPath;
    }

    public void Exit()
    {
        i = 0;
    }
    #endregion
}
