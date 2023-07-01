using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "Data/Player", order = 0)]
    public class PlayerData : ScriptableObject
    {
        [Header("Move State")]
        public float moveSpeed = 8f;

        [Header("Jump/Fall State")]
        public float jumpForce = 14f;
        public int amountOfJumps = 1;
        public float coyoteTime = 0.25f;
        public float jumpMultiplier = 0.5f;

    }
}