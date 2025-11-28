using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "NewPlayerData", menuName = "Data/Player Data", order = 0)]
    public class PlayerData : ScriptableObject
    {
        [Header("Common Data")] 
        public int health = 1;
        public int maxHealth = 2;
        
        [Header("Move State")] 
        public float moveSpeed = 8f;
        public float defaultMoveSpeed = 8f;

        [Header("Jump/Fall State")]
        public float jumpForce = 14f;
        public float defaultJumpForce = 14f;
        public int amountOfJumps = 1;
        public int defaultAmountOfJumps = 1;
        public float coyoteTime = 0.25f;
        public float jumpMultiplier = 0.5f;
        public float softJumpMultiplier = 0.6f;
        public float jumpMoveSpeed = 4;
        public float fallingGravityScale = -10;
        
    }
}