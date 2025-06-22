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
    public class SignPlacementData
    {
        public int tileIndex;
        [TextArea(3, 5)]
        public string signText;
    }
    public List<SignPlacementData> signs = new List<SignPlacementData>();
     public int levelExitPOS;
    public int width;
    public int height;
    
}
