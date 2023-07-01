using System;
using Player.StateMachinePattern;
using Player.StateMachinePattern.States;
using UnityEngine;
using UnityEngine.Serialization;

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
        public PlayerJumpState JumpState { get; private set; }
        public PlayerFallState FallState { get; private set; }
        
        public PlayerInputHandler InputHandler { get; private set; }
        public Animator PlayerAnimator { get; private set; }
        public Rigidbody2D PlayerRB2D { get; private set; }

        [SerializeField] private PlayerData playerData;

        [SerializeField] private Transform groundCheckPosition;
        [SerializeField] private float groundCheckRadius;
        [SerializeField] private LayerMask groundCheckMask;

        private void Awake()
        {
            InputHandler = GetComponent<PlayerInputHandler>();
            PlayerAnimator = GetComponent<Animator>();
            PlayerRB2D = GetComponent<Rigidbody2D>();
            
            StateMachine = new PlayerStateMachine();

            IdleState = new PlayerIdleState(StateMachine, this, playerData);
            MoveState = new PlayerMoveState(StateMachine, this, playerData);
            JumpState = new PlayerJumpState(StateMachine, this, playerData);
            FallState = new PlayerFallState(StateMachine, this, playerData);
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

        public void StopMove()
        {
            PlayerRB2D.velocity = Vector2.zero;
        }

        public void Jump(float jumpForce)
        {
            PlayerRB2D.velocity = new Vector2(PlayerRB2D.velocity.y, jumpForce);
        }

        public bool IsGrounded()
        {
            return Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundCheckMask);
        }
    }
}