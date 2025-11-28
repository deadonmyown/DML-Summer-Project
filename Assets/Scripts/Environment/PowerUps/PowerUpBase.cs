using System;
using UnityEngine;

namespace Environment.PowerUps
{
    public abstract class PowerUpBase : MonoBehaviour
    {
        public bool IsActive { get; set; } = true;
        
        public abstract void StartPowerUp(Player.Player player);

        public abstract void EndPowerUp(Player.Player player);
    }
}
