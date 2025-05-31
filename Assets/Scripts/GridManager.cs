using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private GameObject mainCamera;
    public GameObject[] tileArray;
    [SerializeField] private int index;
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
                tileArray[index] = spawnedTile.gameObject;
                arrayCount += 1;
            }
        }
    }
    void Start()
    {
        GenerateGrid();
        mainCamera.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
        Debug.Log($"Grid is {_width * _height} squares big.");
    }


    void Update()
    {
        
    }
}
