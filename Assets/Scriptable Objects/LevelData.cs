using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    public List<int> wallTileIndicies = new List<int>();
    public List<int> enemyIndicies = new List<int>();
    public List<int> teleportIndicies = new List<int>();
    public GameObject[] teleporters;
    public GameObject player;
    public int playerSpawnPOS;
    [System.Serializable]
    public class DoorPlacementData
    {
        public string itemNeeded;
        public int tileIndex;
    }

    [System.Serializable]
    public class SignPlacementData
    {
        public int tileIndex;
        [TextArea(3, 5)]
        public string signText;
    }
    [System.Serializable]
    public class ItemPlacementData
    {
        public int tileIndex;
        public int itemIndex;
        public string itemName;
    }
    [System.Serializable]
    public class LevelExitPlacementData
    {
        public int tileIndex;
        public int levelTarget;
    }
    public List<SignPlacementData> signs = new List<SignPlacementData>();
    public List<ItemPlacementData> items = new List<ItemPlacementData>();
    public List<DoorPlacementData> doors = new List<DoorPlacementData>();
    public List<LevelExitPlacementData> levelExits = new List<LevelExitPlacementData>();
     
    public int width;
    public int height;
    
}
