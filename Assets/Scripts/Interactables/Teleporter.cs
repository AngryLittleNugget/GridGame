using Unity.Mathematics;
using UnityEngine;

public class Teleporter : Interactable
{
    private PlayerHealth originalPlayerHealth;
    private PlayerHealth newPlayerHealth;
    private GameObject originalPlayer;
    private GameObject newPlayer;
    private GridManager gridManager;
    private LevelData levelData;
    private Vector3 targetPOS;
    public int teleportArrayIndex;
    void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();
        levelData = gridManager.levelData;
    }

    public override bool Interact()
    {
        originalPlayer = GameObject.FindGameObjectWithTag("Player");
        originalPlayerHealth = originalPlayer.GetComponent<PlayerHealth>();
        GameObject currentTeleporter = levelData.teleporters[(teleportArrayIndex)];
        if (teleportArrayIndex % 2 == 0)
        {
            targetPOS = levelData.teleporters[(teleportArrayIndex + 1) % levelData.teleporters.Length].transform.position;
        }
        else
        {
            targetPOS = levelData.teleporters[(teleportArrayIndex - 1) % levelData.teleporters.Length].transform.position;
        }
        if (levelData.teleporters[(teleportArrayIndex + 1) % levelData.teleporters.Length].transform.position == null)
        {
            Debug.Log("Teleporter is null");
        }
        else
        {
            Debug.Log($"Teleporter is not null and position is {targetPOS}");
        }
        newPlayer = Instantiate(originalPlayer, targetPOS, quaternion.identity);
        newPlayerHealth = newPlayer.GetComponent<PlayerHealth>();
        newPlayerHealth.Health = originalPlayerHealth.Health;
        Destroy(originalPlayer);
        return false;
    }


}
