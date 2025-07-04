using UnityEngine;
using System.Collections;

public enum Directions
{
    Right,
    Up,
    Left,
    Down
}

public class Enemy : Interactable
{
    private Vector3 _currentPOS;
    [SerializeField] private int currentDirection;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] float moveDuration;
    [SerializeField] Directions moveDirection;
    int moveSpeed = 1;
    private GridManager _gridManager;

    private void Awake()
    {
        _gridManager = FindFirstObjectByType<GridManager>();
        _currentPOS = gameObject.transform.position;
        if (_gridManager == null)
        {
            Debug.Log("Grid Manager null in Enemy");
        }
    }
    private void Update()
    {
        if (transform.position.x < 0 || transform.position.x > _gridManager.tileArray[_gridManager.arrayCount - 1].transform.position.x
         || transform.position.y < 0 || transform.position.y > _gridManager.tileArray[_gridManager.arrayCount - 1].transform.position.y)
        {
            Destroy(gameObject);
        }

    }

    private void OnEnable()
    {
        PlayerMove.OnPlayerMove += Move;
    }

    private void OnDisable()
    {
        PlayerMove.OnPlayerMove -= Move;
    }
    public override bool Interact()
    {
        PlayerHealth pHealth = FindFirstObjectByType<PlayerHealth>();
        pHealth.TakeDamage(1);
        return false;
    }

    private void Move()
    {
        
        Debug.Log("Move is firing");
        Vector3 movement = new Vector3 (0,0,0);
        switch (moveDirection)
        {
            case Directions.Right:
                movement = new Vector3(moveSpeed, 0, 0);
                break;
            case Directions.Up:
                movement = new Vector3(0, moveSpeed, 0);
                break;
            case Directions.Left:
                movement = new Vector3(-moveSpeed, 0, 0);
                break;
            case Directions.Down:
                movement = new Vector3(0, -moveSpeed, 0);
                break;
        }
        Vector2 checkPos = (Vector2)transform.position + (Vector2)movement;
        Collider2D hit = Physics2D.OverlapPoint(checkPos, obstacleLayer);
        if (hit != null)
        {
            Interactable isInteractable = hit.GetComponent<Interactable>();
            if (isInteractable != null)
            {
                moveDirection = (Directions) (((int) moveDirection + 1) % 4);
                return;
            }
            else if (hit.CompareTag("Player"))
            {
                PlayerHealth pHealth = FindFirstObjectByType<PlayerHealth>();
                pHealth.TakeDamage(1);
            }
        }
        else
        {
            Vector3 _targetPOS = _currentPOS + movement;
            StartCoroutine(MoveToSquare(_currentPOS, _targetPOS, moveDuration));
            _currentPOS = _targetPOS;
        }
    }

    private IEnumerator MoveToSquare(Vector3 _currentPOS, Vector3 _targetPOS, float moveDuration)
    {
        float elapsed = 0;
        while (_currentPOS != _targetPOS)
        {
            transform.position = Vector3.Lerp(_currentPOS, _targetPOS, elapsed / moveDuration);
            _currentPOS = transform.position;
            elapsed += Time.deltaTime;
            yield return null;
        }

    }
}
