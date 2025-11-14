using StateMachinePattern;

namespace Player.StateMachinePattern
{
    public class PlayerStateMachine : StateMachine
    {
        public void Initialize(State startState)
        {
            SwitchState(startState);
        }
    }
}