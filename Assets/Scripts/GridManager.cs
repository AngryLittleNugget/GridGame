using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private int _width, _height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] public LevelData levelData;
    [SerializeField] private GameObject mainCamera;
    public GameObject[] tileArray;
    
    public int arrayCount;

    private void GenerateGrid()
    {
        tileArray = new GameObject[_width * _height];
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = ($"Tile {x}, {y}");
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);

                spawnedTile.Init(isOffset);
                int index = x + y * _width;
                spawnedTile.Index = index;
                tileArray[index] = spawnedTile.gameObject;
                arrayCount += 1;
            }
        }
    }
    void Awake()
    {
        _width = levelData.width;
        _height = levelData.height;
        GenerateGrid();
    }
    void Start()
    {
        mainCamera.transform.position = new Vector3(_width / 2 -.5f, (float) _height / 2 -.5f, -10);
    }

}
