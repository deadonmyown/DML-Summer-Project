using UnityEngine;

namespace Environment.PowerUps
{
    public class PowerUpJumpsCount : PowerUpBase
    {
        [SerializeField] private int jumpsCount;
        
        public override void StartPowerUp(Player.Player player)
        {
            player.PlayerData.amountOfJumps = jumpsCount;
            if(player.IsGrounded())
                player.JumpState.ResetAmountOfJumpsLeft();
        }

        public override void EndPowerUp(Player.Player player)
        {
            player.PlayerData.amountOfJumps = player.PlayerData.defaultAmountOfJumps;
            if(player.IsGrounded())
                player.JumpState.ResetAmountOfJumpsLeft();
        }
    }
}