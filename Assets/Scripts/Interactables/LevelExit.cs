using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : Interactable
{
    public int levelIndex;
    public int playerSpawnPOS;

    public override bool Interact()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(levelIndex);
        PlayerLocationSpawn.Instance.playerSpawnPOS = playerSpawnPOS;
        return true;
    }
        void OnTriggerEnter2D(Collider2D collision) //Yeah.  It's already in there.
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
        }
    }
}
