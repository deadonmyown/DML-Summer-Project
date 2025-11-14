using System;
using System.Collections;
using System.Collections.Generic;
using Enemies.Abstraction;
using Enemies.StateMachinePattern;
using Enemies.StateMachinePattern.States;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        public EnemyStateMachine StateMachine { get; private set; }
        public IList<EnemyBehaviourState> EnemyBehaviourStates { get; private set; }
        
        [SerializeField] private int damage;
        [SerializeField] private float damageForce;
        [SerializeField] private float yDamageForce;
        [SerializeField] private float stunTime = 0.8f;

        private EnemyBehaviour[] _enemyBehaviours;
        private int _enemyBehavioursCount;
        
        private void Start()
        {
            _enemyBehaviours = GetComponents<EnemyBehaviour>();

            StateMachine = new EnemyStateMachine();

            if (_enemyBehaviours != null)
            {
                _enemyBehavioursCount = _enemyBehaviours.Length;

                EnemyBehaviourStates = new List<EnemyBehaviourState>();

                for (int i = 0; i < _enemyBehavioursCount; i++)
                {
                    EnemyBehaviourStates.Add(new EnemyBehaviourState(this, StateMachine, _enemyBehaviours, _enemyBehaviours[i], i, _enemyBehavioursCount));
                }
                
                StateMachine.Initialize(EnemyBehaviourStates[0]);
            }
        }

        private void Update()
        {
            StateMachine.CurrentState?.Tick();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("PlayerMesh"))
            {
                Debug.Log(other.collider.name);
                var player = other.collider.GetComponentInParent<Player.Player>();
                StartCoroutine(Damage(player));
            }
        }

        private IEnumerator Damage(Player.Player player)
        {
            yield return player.GetDamageForce(transform.position, damageForce, yDamageForce, stunTime);
            
            player.GetDamage(damage);
        }
    }
}