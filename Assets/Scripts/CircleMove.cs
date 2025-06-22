using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

public class CircleMove : MonoBehaviour
{
    private Vector3 _currentPOS;
    GridManager _gridManager;
    Vector3 _targetPOS;
    private bool blocked;
    private Vector2 direction;
    void Awake()
    {
        _gridManager = FindFirstObjectByType<GridManager>();
        _currentPOS = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        float movementHoriz = Input.GetAxisRaw("Horizontal");
        float movementVert = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            CheckTile(Vector2.left);
            if (blocked == false)
            {
                Move();
            }
            else
            {
                blocked = false;
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            Move();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Move();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            Move();
        }

        if (transform.position.x < 0 || transform.position.x > _gridManager.tileArray[_gridManager.arrayCount - 1].transform.position.x)
        {
            Destroy(gameObject);
        }

        if (transform.position.y < 0 || transform.position.y > _gridManager.tileArray[_gridManager.arrayCount - 1].transform.position.y)
        {
            Destroy(gameObject);
        }
    }
    private void Move()
    {
        float movementHoriz = Input.GetAxisRaw("Horizontal");
        float movementVert = Input.GetAxisRaw("Vertical");
        Vector3 _targetPOS = new Vector2(transform.position.x + movementHoriz, transform.position.y + movementVert);
            _currentPOS = _targetPOS;
            transform.position = new Vector3(_currentPOS.x, _currentPOS.y, _currentPOS.z);
    }

    private bool CheckTile(Vector2 direction)
    {
        RaycastHit2D ray = Physics2D.Raycast(_currentPOS, direction, 2f);
        Debug.DrawRay(_currentPOS, direction * 2f, Color.red, 2f);
        if (ray.collider != null)
        {
            if (ray.collider.CompareTag("Wall"))
            {
                Debug.Log("Ray hit");
                return blocked = true;
            }
            else
            {
                Debug.Log("Ray missed");
                return blocked = false;
            }
        }
        else
        {
            Debug.Log("Nothing.");
            return blocked = false;

        }
    }
}
