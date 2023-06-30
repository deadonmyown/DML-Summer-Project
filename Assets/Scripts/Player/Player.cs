using System;
using Player.StateMachinePattern;
using Player.StateMachinePattern.States;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerInputHandler))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
        
        public PlayerInputHandler InputHandler { get; private set; }
        public Animator PlayerAnimator { get; private set; }
        public Rigidbody2D PlayerRB2D { get; private set; }
        
        //TODO: Better to make it in another script
        public float MoveSpeed;
        //

        private void Awake()
        {
            InputHandler = GetComponent<PlayerInputHandler>();
            PlayerAnimator = GetComponent<Animator>();
            PlayerRB2D = GetComponent<Rigidbody2D>();
            
            StateMachine = new PlayerStateMachine();

            IdleState = new PlayerIdleState(StateMachine, this);
            MoveState = new PlayerMoveState(StateMachine, this);
        }

        private void Start()
        {
            StateMachine.Initialize(IdleState);
        }

        private void Update()
        {
            StateMachine.CurrentState?.Tick();
        }

        public void Move(float velocity)
        {
            PlayerRB2D.velocity = new Vector2(velocity, PlayerRB2D.velocity.y);
        }
    }
}