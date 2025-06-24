using System.Collections;
//using System.Numerics;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private enum PlayerState
    {
        Idle,
        Moving
    }
    private PlayerState currentState = PlayerState.Idle;
    private Vector3 _currentPOS;
    private GridManager _gridManager;
    public static event System.Action OnPlayerMove;
    [SerializeField] LayerMask levelExitLayer;
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] float moveDuration;

    void Awake()
    {
        _gridManager = FindFirstObjectByType<GridManager>();
        _currentPOS = transform.position;
    }

    void Update()
    {
        if (currentState == PlayerState.Moving)
        {
            return;
        }
        Vector2Int inputDir = Vector2Int.zero;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) inputDir = Vector2Int.left;
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) inputDir = Vector2Int.right;
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) inputDir = Vector2Int.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) inputDir = Vector2Int.down;

        if (inputDir != Vector2Int.zero) //1:inputDir is reset to 0,0 every frame thanks to update, and then changes to 0,1, 0,-1 whatever when you press a button.
        {
            if (!IsBlocked(inputDir)) //2:silly elky learning moment.  If at any point any part of inputDir is not zero, run this.
            {
                Move(inputDir); //5: If IsBlocked returns false, run move code.
                                //And if there is no interactable, move.
                Debug.Log("");
            }
           /* else
            {
                OnPlayerMove?.Invoke();
                Debug.Log("OnPlayerMove fired from else");
            } */
        }

        // Boundary check
        if (transform.position.x < 0 || transform.position.x > _gridManager.tileArray[_gridManager.arrayCount - 1].transform.position.x
         || transform.position.y < 0 || transform.position.y > _gridManager.tileArray[_gridManager.arrayCount - 1].transform.position.y)
        {
            Destroy(gameObject);
        }
    }

    private void Move(Vector2Int dir)  //Takes the input direction, which it still has time to do because it's part of the same update.
    {
        currentState = PlayerState.Moving;
        Vector3 movement = new Vector3(dir.x, dir.y, 0); //Set up the inputDir to be a vector 3 to relate to the coordinate system Unity uses.
        Vector3 _targetPOS = _currentPOS + movement;  //Add the vectors, pretty sure myx knows more about that than elky.
        StartCoroutine(MoveToSquare(_currentPOS, _targetPOS, moveDuration));
        _currentPOS = _targetPOS;
        OnPlayerMove?.Invoke();
        Debug.Log("OnPlayerMove fired from move");
    }

    private bool IsBlocked(Vector2Int dir) //3: So it just... gets the variable from the freakin' aether.  That's just what happens apparently.
    {
        Vector2 checkPos = (Vector2)transform.position + (Vector2)dir; //the position we intend to move to.  If character is at 0,1 and wants to go down, dir is 0, -1, so checkPOS is 0,0

        Collider2D hit = Physics2D.OverlapPoint(checkPos, obstacleLayer); //4: ze majicks. Will explain further with more space, but long and short checks to see if there's a collider in the space and what kind.
        if (hit != null)
        {
            Interactable interactable = hit.GetComponent<Interactable>(); 
            if (interactable != null)
            {
                bool success = interactable.Interact();
                if (!success)
                {
                    return true;
                }
                return false;
            }
            return true;
        }
        return false;
    }
    private bool IsLevelExit(Vector2 dir)
    {
        Vector2 checkPos = (Vector2)transform.position + (Vector2)dir;
        Collider2D hit = Physics2D.OverlapPoint(checkPos, levelExitLayer);

        if (hit != null && hit.CompareTag("LevelExit")) // Ensure your LevelExit GameObject is tagged "LevelExit"
        {
            return true;
        }
        return false;
    }

    private IEnumerator MoveToSquare(Vector3 _currentPOS, Vector3 _targetPOS, float moveDuration)
    {
        float elapsed = 0;
        while (elapsed < moveDuration)
        {
            transform.position = Vector3.Lerp(_currentPOS, _targetPOS, elapsed / moveDuration);
            _currentPOS = transform.position;
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = _targetPOS;
        currentState = PlayerState.Idle;
    } 
}