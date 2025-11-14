using UnityEngine;

namespace Environment.PowerUps
{
    public class PowerUpJumpForce : PowerUpBase
    {
        [SerializeField] private float jumpForceBooster;
        
        public override void StartPowerUp(Player.Player player)
        {
            player.PlayerData.jumpForce += jumpForceBooster;
        }

        public override void EndPowerUp(Player.Player player)
        {
            player.PlayerData.jumpForce = player.PlayerData.defaultJumpForce;
        }
    }
}