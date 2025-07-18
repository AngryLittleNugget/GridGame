using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    [SerializeField] GridManager _gridManager;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject wall;
    [SerializeField] GameObject signPrefab;
    [SerializeField] GameObject teleporter;
    [SerializeField] GameObject[] item;
    [SerializeField] GameObject player;
    [SerializeField] GameObject doors;
    private Teleporter teleporterData;
    public int playerSpawnPOS;
    LevelData levelData;
    [SerializeField] GameObject levelExit;
    [SerializeField] GameOverScreen gameOverScreen;
    /*private Vector3[] spawnPoints = new Vector3[6];
    Vector3 spawnPOS; */
    void Awake()
    {
        levelData = FindFirstObjectByType<GridManager>().levelData;
    }

    void Start()
    {
        RoomSpawn();
        EnemySpawn();
        SignSpawn();
        TeleporterSpawn();
        ItemSpawn();
        DoorSpawn();
        // Instantiate(levelExit, _gridManager.tileArray[levelData.levelExitPOS].transform.position, Quaternion.identity);
        LevelExitSpawn();
        PlayerSpawn();
        gameOverScreen.Subscribe();
    }


    void RoomSpawn()
    {
        foreach (int tileIndex in levelData.wallTileIndicies)
        {
            if (tileIndex < 0 || tileIndex > _gridManager.tileArray.Length)
            {
                Debug.LogWarning($"Invalid tile index: {tileIndex}");
                continue;
            }
            Vector3 spawnPOS = _gridManager.tileArray[tileIndex].transform.position;
            Instantiate(wall, spawnPOS, Quaternion.identity);
        }
    }

    void EnemySpawn()
    {
        foreach (int enemyIndex in levelData.enemyIndicies)
        {
            if (enemyIndex < 0 || enemyIndex > _gridManager.tileArray.Length)
            {
                Debug.LogWarning($"Invalid index: {enemy}");
                continue;
            }
            Vector3 spawnPOS = _gridManager.tileArray[enemyIndex].transform.position;
            Instantiate(enemy, spawnPOS, Quaternion.identity);
        }
    }

    void SignSpawn()
    {
        foreach (LevelData.SignPlacementData signData in levelData.signs)
        {
            int tileIndex = signData.tileIndex;
            if (tileIndex < 0 || tileIndex > _gridManager.tileArray.Length)
            {
                Debug.LogWarning($"Invalid index: {tileIndex}");
                continue;
            }
            Vector3 spawnPOS = _gridManager.tileArray[tileIndex].transform.position;
            GameObject spawnedSignGO = Instantiate(signPrefab, spawnPOS, Quaternion.identity);
            spawnedSignGO.name = $"Sign {tileIndex}";
            Sign signComponent = spawnedSignGO.GetComponent<Sign>();
            if (signComponent != null)
            {
                signComponent.textToDisplay = signData.signText;
            }

        }
    }

    private void TeleporterSpawn()
    {
        int thisIndex = 0;
        levelData.teleporters = new GameObject[levelData.teleportIndicies.Count];
        foreach (int teleportIndex in levelData.teleportIndicies)
        {
            if (teleportIndex < 0 || teleportIndex > _gridManager.tileArray.Length)
            {
                Debug.LogWarning($"Invalid index: {teleporter}");
                continue;
            }
            Vector3 spawnPOS = _gridManager.tileArray[teleportIndex].transform.position;
            GameObject spawnedTeleporter = Instantiate(teleporter, spawnPOS, Quaternion.identity);
            Teleporter spawnedTeleporterScript = spawnedTeleporter.GetComponent<Teleporter>();
            spawnedTeleporterScript.teleportArrayIndex = thisIndex;
            levelData.teleporters[thisIndex] = spawnedTeleporter;
            thisIndex += 1;
        }
    }

    void ItemSpawn()
    {
        foreach (LevelData.ItemPlacementData itemData in levelData.items)
        {
            int tileIndex = itemData.tileIndex;
            int specificItemIndex = itemData.itemIndex;
            if (tileIndex < 0 || tileIndex > _gridManager.tileArray.Length)
            {
                Debug.LogWarning($"Invalid index: {tileIndex}");
                continue;
            }
            Vector3 spawnPOS = _gridManager.tileArray[tileIndex].transform.position;
            GameObject spawnedItemGO = Instantiate(item[specificItemIndex], spawnPOS, Quaternion.identity);
            spawnedItemGO.name = $"Item {tileIndex}";
            Item itemComponent = spawnedItemGO.GetComponent<Item>();
            itemComponent.itemHashID = $"{SceneManager.GetActiveScene().name}_{spawnPOS}";
            itemComponent.itemName = itemData.itemName;
            if (InventoryManagement.Instance.itemsToSkip.Contains(itemComponent.itemHashID))
            {
                Debug.Log("Item shud be ded");
                Destroy(spawnedItemGO);
                continue;
            }
            else
            {
                Debug.Log("... AND YET IT LIIIIIIVES");
            }
        }
    }

    void DoorSpawn()
    {
        foreach (LevelData.DoorPlacementData doorData in levelData.doors)
        {
            int tileIndex = doorData.tileIndex;
            if (tileIndex < 0 || tileIndex > _gridManager.tileArray.Length)
            {
                Debug.LogWarning($"Invalid tile index: {tileIndex}");
                continue;
            }
            Vector3 spawnPOS = _gridManager.tileArray[tileIndex].transform.position;
            GameObject spawnedDoorGO = Instantiate(doors, spawnPOS, Quaternion.identity);
            LockedDoor doorComponent = spawnedDoorGO.GetComponent<LockedDoor>();
            doorComponent.itemNeeded = doorData.itemNeeded;

        }
    }

    void LevelExitSpawn()
    {
        foreach (LevelData.LevelExitPlacementData levelExits in levelData.levelExits)
        {
            int tileIndex = levelExits.tileIndex;
            if (tileIndex < 0 || tileIndex > _gridManager.tileArray.Length)
            {
                Debug.LogWarning($"Invalid tile index: {tileIndex}");
                continue;
            }
            Vector3 spawnPOS = _gridManager.tileArray[tileIndex].transform.position;
            GameObject spawnedLEGO = Instantiate(levelExit, spawnPOS, Quaternion.identity);
            LevelExit levelExitComponent = spawnedLEGO.GetComponent<LevelExit>();
            levelExitComponent.levelIndex = levelExits.levelTarget;
            levelExitComponent.playerSpawnPOS = levelExits.playerSpawnPOS;

        }
    }

    private void PlayerSpawn()
    {
        if (PlayerLocationSpawn.Instance.playerSpawnPOS == -1)
        {
            Instantiate(player, _gridManager.tileArray[levelData.playerSpawnPOS].transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(player, _gridManager.tileArray[PlayerLocationSpawn.Instance.playerSpawnPOS].transform.position, Quaternion.identity);
        }
    }

  
}
