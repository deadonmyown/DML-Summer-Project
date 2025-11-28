using UnityEngine;

namespace Environment.PowerUps
{
    public class PowerUpSpeed : PowerUpBase
    {
        [SerializeField] private float speedBooster;
        
        public override void StartPowerUp(Player.Player player)
        {
            player.PlayerData.moveSpeed += speedBooster;
        }

        public override void EndPowerUp(Player.Player player)
        {
            player.PlayerData.moveSpeed = player.PlayerData.defaultMoveSpeed;
        }
    }
}