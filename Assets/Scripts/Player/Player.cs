using Player.StateMachinePattern;
using Player.StateMachinePattern.States;
using UnityEngine;
using UnityEngine.Rendering;

namespace Player
{
    [RequireComponent(typeof(PlayerInputHandler))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
        public PlayerJumpState JumpState { get; private set; }
        public PlayerInAirUpState InAirUpState { get; private set; }
        public PlayerInAirFallState InAirFallState { get; private set; }
        public PlayerFallState FallState { get; private set; }

        public PlayerInputHandler InputHandler { get; private set; }
        public Animator PlayerAnimator { get; private set; }
        public Rigidbody PlayerRB { get; private set; }
        public Interactor Interactor { get; private set; }
        public Vector3 Velocity { get; private set; }

        private Vector3 _temporaryVelocity;

        public int FacingDirection { get; private set; }

        
        [SerializeField] private PlayerData playerData;

        [SerializeField] private Transform groundCheckPosition;
        [SerializeField] private float groundCheckRadius;
        [SerializeField] private LayerMask groundCheckMask;

        [SerializeField] private SpriteRenderer playerSprite;
        [SerializeField] private Collider playerCollider;

        [SerializeField] private PhysicMaterial defaultMaterial;
        [SerializeField] private PhysicMaterial slipperMaterial;

        public PhysicMaterial DefaultMaterial => defaultMaterial;
        public PhysicMaterial SlipperMaterial => slipperMaterial;

        private void Awake()
        {
            InputHandler = GetComponent<PlayerInputHandler>();
            PlayerAnimator = GetComponent<Animator>();
            PlayerRB = GetComponent<Rigidbody>();
            Interactor = GetComponent<Interactor>();

            StateMachine = new PlayerStateMachine();

            IdleState = new PlayerIdleState(StateMachine, this, playerData);
            MoveState = new PlayerMoveState(StateMachine, this, playerData);
            JumpState = new PlayerJumpState(StateMachine, this, playerData);
            InAirUpState = new PlayerInAirUpState(StateMachine, this, playerData);
            InAirFallState = new PlayerInAirFallState(StateMachine, this, playerData);
            FallState = new PlayerFallState(StateMachine, this, playerData);
        }

        private void Start()
        {
            StateMachine.Initialize(IdleState);
            playerSprite.shadowCastingMode = ShadowCastingMode.On;
            playerSprite.receiveShadows = true;
            FacingDirection = 1;
        }

        private void Update()
        {
            Velocity = PlayerRB.velocity;
            StateMachine.CurrentState?.Tick();
            if (InputHandler.IsInteract)
            {
                Interactor.TryInteract();
            }
        }

        public void ChangePhysicMaterial(PhysicMaterial physicMaterial)
        {
            playerCollider.material = physicMaterial;
        }
        
        public void SetVelocityXZ(Vector2 input, float speed)
        {
            _temporaryVelocity = new Vector3(input.x * speed, Velocity.y, input.y * speed);
            SetCalculatedVelocity();
        }

        public void SetVelocityXZRaw(int inputX, int inputY, float speed)
        {
            if (inputX != 0 && inputY != 0)
            {
                var input = new Vector2(inputX, inputY);
                SetVelocityXZ(input / input.magnitude, speed);
                return;
            }
            _temporaryVelocity = new Vector3(inputX * speed, Velocity.y, inputY * speed);
            SetCalculatedVelocity();
        }

        public void ResetVelocity()
        {
            _temporaryVelocity = Vector3.zero;
            SetCalculatedVelocity();
        }

        public void SetVelocityY(float velocityY)
        {
            _temporaryVelocity = new Vector3(Velocity.x, velocityY, Velocity.z);
            SetCalculatedVelocity();
        }

        public void AddVelocityY(float velocityY)
        {
            _temporaryVelocity = new Vector3(Velocity.x, Velocity.y + velocityY, Velocity.z);
            SetCalculatedVelocity();
        }
        
        public void SetCalculatedVelocity()
        {
            PlayerRB.velocity = _temporaryVelocity;
            Velocity = _temporaryVelocity;
        }

        public bool IsGrounded()
        {
            return Physics.CheckSphere(groundCheckPosition.position, groundCheckRadius, groundCheckMask);
        }

        public void CheckFlip(int xInput)
        {
            if (xInput * FacingDirection == -1)
                Flip();
        }

        private void Flip()
        {
            FacingDirection *= -1;
            playerSprite.flipX = !playerSprite.flipX;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(groundCheckPosition.position, groundCheckRadius);
        }
        
    }
}