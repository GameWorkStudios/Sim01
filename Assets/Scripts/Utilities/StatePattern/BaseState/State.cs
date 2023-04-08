public abstract class State
{
    public abstract void StartState(StateMachine machine);
    public abstract void UpdateState(StateMachine machine);
    public abstract void LeaveState(StateMachine machine);
}
