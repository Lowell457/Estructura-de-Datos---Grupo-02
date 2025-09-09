using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Load scene by name
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Load scene by index (as set in Build Settings)
    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    // Load next scene
    public void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }

    // Load previous scene
    public void LoadPreviousScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentIndex > 0)
            SceneManager.LoadScene(currentIndex - 1);
    }
}
