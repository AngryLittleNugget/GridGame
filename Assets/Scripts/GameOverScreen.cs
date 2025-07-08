using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverObject;

    void OnDisable()
    {
        PlayerHealth.GameOver -= GameOverScreenOn;
    }
    void Start()
    {
        //  gameOverObject = gameObject;
        if (gameOverObject.activeInHierarchy)
        {
            gameOverObject.SetActive(false);
        }
    }



    private void GameOverScreenOn()
    {
        gameOverObject.SetActive(true);
    }

    public void Subscribe()
    {
        PlayerHealth.GameOver += GameOverScreenOn;
    }

    public void RestartLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
}

