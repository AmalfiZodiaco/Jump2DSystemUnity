namespace AI
{
    public interface IState : INode
    {
        void Enter();
        bool Execute();
        void Exit();
    }
}

