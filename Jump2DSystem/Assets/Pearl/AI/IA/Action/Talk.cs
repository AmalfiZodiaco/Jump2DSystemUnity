using UnityEngine;
using AI;

public class Talk : IState
{
    #region Override Methods
    public void Enter()
    {
    }

    public bool Execute()
    {
        Debug.Log("ciao");
        return true;
    }

    public void Exit()
    {
    }
    #endregion
}
