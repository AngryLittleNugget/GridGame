using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : Interactable
{
    void Update()
    {
  
    }

    public override bool Interact()
    {
        Debug.Log("Suckle heartily on mine phallus, for you have been vanquished biznitch.");
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex + 1);

        return true;
    }
        void OnTriggerEnter2D(Collider2D collision) //Yeah.  It's already in there.
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
        }
    }
}
