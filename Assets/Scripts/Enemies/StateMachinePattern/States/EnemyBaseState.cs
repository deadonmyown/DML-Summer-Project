using Enemies.Abstraction;
using StateMachinePattern;

namespace Enemies.StateMachinePattern.States
{
    public abstract class EnemyBaseState : State
    {
        protected readonly Enemy Enemy;
        protected readonly EnemyBehaviour[] EnemyBehaviours;
        protected readonly EnemyBehaviour CurrentBehaviour;
        protected readonly int CurrentIndex;
        protected readonly int EnemyBehavioursCount;
        protected readonly EnemyStateMachine StateMachine;

        protected EnemyBaseState(Enemy enemy, EnemyStateMachine stateMachine, EnemyBehaviour[] enemyBehaviours, EnemyBehaviour currentBehaviour, int currentIndex, int enemyBehavioursCount)
        {
            Enemy = enemy;
            StateMachine = stateMachine;
            EnemyBehaviours = enemyBehaviours;
            CurrentBehaviour = currentBehaviour;
            CurrentIndex = currentIndex;
            EnemyBehavioursCount = enemyBehavioursCount;
        }
    }
}