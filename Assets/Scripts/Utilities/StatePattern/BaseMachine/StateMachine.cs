using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State currentState;
    public abstract void ChangeState(State state);
}
