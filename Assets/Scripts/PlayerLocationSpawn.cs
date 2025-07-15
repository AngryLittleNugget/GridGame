using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLocationSpawn : MonoBehaviour
{
    public static PlayerLocationSpawn Instance;
    public GridManager gridManager;
    public int playerSpawnPOS;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        gridManager = FindFirstObjectByType<GridManager>();
        playerSpawnPOS = -1;

    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += UpdateLevelSO;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= UpdateLevelSO;
    }


    public void UpdateLevelSO(Scene scene, LoadSceneMode mode)
    {
        gridManager = FindFirstObjectByType<GridManager>();
    }
}
