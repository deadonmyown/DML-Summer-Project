using UnityEngine;

namespace Player.StateMachinePattern.States
{
    public class PlayerJumpState : PlayerBaseState
    {
        private int _amountOfJumpsLeft;
        
        public PlayerJumpState(PlayerStateMachine stateMachine, Player player, PlayerData playerData) : base(stateMachine, player, playerData)
        {
            
        }

        public override void Enter()
        {
            Debug.Log("Enter Jump State");
            Player.Jump(PlayerData.jumpForce);
            _amountOfJumpsLeft--;
        }

        public override void Tick()
        {
            CheckJumpMultiplier();
            
            var velocity = Player.PlayerRB2D.velocity.y;

            var inputX = Player.InputHandler.MovementInputX;
            
            if (velocity <= 0f)
            {
                StateMachine.SwitchState(Player.FallState);
            }
            
            Player.Move(PlayerData.moveSpeed * inputX);
        }

        public override void Exit()
        {
        }
        
        public bool CanJump() => _amountOfJumpsLeft > 0 ? true : false;

        public void ResetAmountOfJumpsLeft() => _amountOfJumpsLeft = PlayerData.amountOfJumps;

        public void DecreaseAmountOfJumpsLeft() => _amountOfJumpsLeft--;

        private void CheckJumpMultiplier()
        {
            if (Player.InputHandler.JumpInputStop)
            {
                Player.Jump(Player.PlayerRB2D.velocity.y * PlayerData.jumpMultiplier);
            }
        }
    }
}