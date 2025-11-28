using System.Collections;
using UnityEngine;

namespace Environment.PowerUps
{
    public class PowerUpPlatform : MonoBehaviour
    {
        [SerializeField] private PowerUpBase powerUp;
        [SerializeField] private float powerUpDuration;
        
        private void OnTriggerEnter(Collider other)
        {
            if (powerUp.IsActive && other.CompareTag("PlayerMesh"))
            {
                Debug.Log("Player enter");
                var player = other.GetComponentInParent<Player.Player>();
                powerUp.StartPowerUp(player);
                powerUp.IsActive = false;
                powerUp.gameObject.SetActive(false);
                StartCoroutine(StartPowerUpCooldown(player));
            }
        }

        private IEnumerator StartPowerUpCooldown(Player.Player player)
        {
            yield return new WaitForSeconds(powerUpDuration);

            powerUp.gameObject.SetActive(true);
            powerUp.IsActive = true;
            powerUp.EndPowerUp(player);
        }
    }
}
