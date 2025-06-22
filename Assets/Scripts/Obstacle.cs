using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] GridManager _gridManager;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject wall;
    [SerializeField] GameObject signPrefab;
    [SerializeField] GameObject teleporter;
    [SerializeField] GameObject player;
    private Teleporter teleporterData;
    LevelData levelData;
    [SerializeField] GameObject levelExit;
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
        Instantiate(levelExit, _gridManager.tileArray[levelData.levelExitPOS].transform.position, Quaternion.identity);
        Instantiate(player, _gridManager.tileArray[levelData.playerSpawnPOS].transform.position, Quaternion.identity);
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
    
}
