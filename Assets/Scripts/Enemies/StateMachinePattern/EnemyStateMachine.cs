using StateMachinePattern;

namespace Enemies.StateMachinePattern
{
    public class EnemyStateMachine : StateMachine
    {
        public void Initialize(State startState)
        {
            SwitchState(startState);
        }
    }
}