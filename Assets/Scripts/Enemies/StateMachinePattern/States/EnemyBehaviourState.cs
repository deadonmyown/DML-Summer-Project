using Enemies.Abstraction;

namespace Enemies.StateMachinePattern.States
{
    public class EnemyBehaviourState : EnemyBaseState
    {
        public EnemyBehaviourState(Enemy enemy, EnemyStateMachine stateMachine, EnemyBehaviour[] enemyBehaviours, EnemyBehaviour currentBehaviour, int currentIndex, int enemyBehavioursCount) : base(enemy, stateMachine, enemyBehaviours, currentBehaviour, currentIndex, enemyBehavioursCount)
        {
        }
        
        public override void Enter()
        {
            CurrentBehaviour.StartBehaviour();
        }

        public override void Tick()
        {
            for (int i = 0; i < EnemyBehavioursCount; i++)
            {
                if (i != CurrentIndex)
                {
                    if (EnemyBehaviours[i].CheckActive())
                    {
                        if (!CurrentBehaviour.IsActive || EnemyBehaviours[i].OrderIndex > CurrentBehaviour.OrderIndex)
                        {
                            StateMachine.SwitchState(Enemy.EnemyBehaviourStates[i]);
                        }
                    }
                }
            }
        }

        public override void Exit()
        {
            CurrentBehaviour.StopCoroutines();
        }
    }
}