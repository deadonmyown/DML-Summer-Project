using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private CanvasRenderer screen;
    [SerializeField] private float duration;

    private void Start()
    {
        StartCoroutine(FadeScreen(1, 0));
    }
    
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(0, 1, SceneManager.GetActiveScene().buildIndex + 1));
    }


    public void LoadAnyLevel(int index)
    {
        StartCoroutine(LoadLevel(0, 1, SceneManager.GetActiveScene().buildIndex + index));
    }


    IEnumerator LoadLevel(float start, float end, int levelIndex)
    {
        yield return FadeScreen(start, end);
        
        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator FadeScreen(float start, float end)
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            
            screen.SetAlpha(Mathf.Lerp(start, end, t));
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        screen.SetAlpha(end);
    }
}
