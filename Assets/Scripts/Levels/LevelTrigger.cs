using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    [SerializeField] private LevelLoader loadLevel;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            loadLevel.LoadNextLevel();
        }
    }
}
