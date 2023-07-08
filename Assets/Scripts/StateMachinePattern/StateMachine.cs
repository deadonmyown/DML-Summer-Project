public abstract class StateMachine
{
    public State CurrentState { get; private set; }

    public void SwitchState(State newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
