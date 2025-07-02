
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private GameObject _highlight;
    [SerializeField] GridManager _gridManager;
    [SerializeField] GameObject _stamp;
    private SpriteRenderer _spriteRenderer;
    private Tile _currentSelection;
    public int Index { get; set; }
    

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(bool isOffset)

    {
        _spriteRenderer.color = isOffset ? _baseColor : _offsetColor;
        
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
        _currentSelection = GetComponent<Tile>();
       Debug.Log($"{_currentSelection.name}, Index: {Index}");
        
    } 

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    private void OnMouseDown()
    {
        Instantiate(_stamp, _currentSelection.transform.position, Quaternion.identity);
    }

}
