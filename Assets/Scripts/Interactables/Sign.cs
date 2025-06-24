using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.Controls;

public class Sign : Interactable
{
    private TextMeshProUGUI displayText;
    private Canvas canvas;
    public string textToDisplay;
    private GridManager gridManager;
    private bool textIsOn = false;
    private void OnEnable()
    {
        PlayerMove.OnPlayerMove += TurnOffText;
    }

    private void OnDisable()
    {
        PlayerMove.OnPlayerMove -= TurnOffText;
    }

    void Start()
    {
        canvas = GameObject.FindWithTag("UICanvas").GetComponentInChildren<Canvas>();
        displayText = FindFirstObjectByType<TextMeshProUGUI>();
        gridManager = FindFirstObjectByType<GridManager>();
        if (canvas != null)
        {
            // canvas.enabled = false;
            displayText.text = "";
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            canvas.enabled = true;
            displayText.text = "Beebldee blur";
            Debug.Log($"{textToDisplay}");
        }
    }


     public override bool Interact()
      {
        /*  Debug.Log("1: Player makes contact.");
          if (canvas.enabled == false)
          { */
              Debug.Log("2: Canvas confirmed false");
             // canvas.enabled = true;
              displayText.text = this.textToDisplay;
              textIsOn = true;
             /* if (textIsOn == true)
              {
                  Debug.Log($"2.5 text is on is {textIsOn}");
              }
          }
          Debug.Log("3: Text should be on"); */
          return false; 
      } 

      private void TurnOffText()
      {
          Debug.Log("4: TurnOffText firing");
          if (textIsOn == true)
          {
              Debug.Log("5: Inner block running.");
            //canvas.enabled = false;
            displayText.text = "";
              textIsOn = false;
              Debug.Log($"5.5: canvas.enabled is {canvas.enabled}");
          }
      }
}
