using UnityEngine;

public class CircleMove : MonoBehaviour
{
    private Vector3 _currentPOS;
    GridManager _gridManager;
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
            Vector3 _targetPOS = new Vector3(transform.position.x + movementHoriz, transform.position.y, transform.position.z);
            _currentPOS = _targetPOS;
            transform.position = new Vector3(_currentPOS.x, _currentPOS.y, _currentPOS.z);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            Vector3 _targetPOS = new Vector3(transform.position.x + movementHoriz, transform.position.y, transform.position.z);
            _currentPOS = _targetPOS;
            transform.position = new Vector3(_currentPOS.x, _currentPOS.y, _currentPOS.z);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Vector3 _targetPOS = new Vector3(transform.position.x, transform.position.y + movementVert, transform.position.z);
            _currentPOS = _targetPOS;
            transform.position = new Vector3(_currentPOS.x, _currentPOS.y, _currentPOS.z);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            Vector3 _targetPOS = new Vector3(transform.position.x, transform.position.y + movementVert, transform.position.z);
            _currentPOS = _targetPOS;
            transform.position = new Vector3(_currentPOS.x, _currentPOS.y, _currentPOS.z);
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
    
    
}
