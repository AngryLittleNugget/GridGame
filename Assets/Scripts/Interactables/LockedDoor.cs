using UnityEngine;
using TMPro;
public class LockedDoor : Interactable
{
    private GridManager gridManager;
    public string itemNeeded;
    private Canvas canvas;
    private TextMeshProUGUI displayText;
    private bool textIsOn = false;

    void OnEnable()
    {
        PlayerMove.OnPlayerMove += TurnOffText;
    }

    private void OnDisable()
    {
        PlayerMove.OnPlayerMove -= TurnOffText;
    }
    void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();
        canvas = GameObject.FindWithTag("UICanvas").GetComponentInChildren<Canvas>();
        displayText = FindFirstObjectByType<TextMeshProUGUI>();

        if (canvas != null)
        {

            displayText.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override bool Interact()
    {
        if (InventoryManagement.Instance.HasItem(itemNeeded))
        {
            Destroy(gameObject);
            return true;
        }
        else
        {
            displayText.text = $"You need the {this.itemNeeded}";
              textIsOn = true;
            return false;
        }
    }
    
      private void TurnOffText()
      {
         // Debug.Log("4: TurnOffText firing");
          if (textIsOn == true)
          {
             // Debug.Log("5: Inner block running.");
            //canvas.enabled = false;
            displayText.text = "";
              textIsOn = false;
             // Debug.Log($"5.5: canvas.enabled is {canvas.enabled}");
          }
      }
}
