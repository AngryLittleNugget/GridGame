using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.Controls;

public class Sign : Interactable
{
    private TextMeshProUGUI displayText;
    private Canvas canvas;
    public string textToDisplay;
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
        canvas = FindFirstObjectByType<Canvas>();
        displayText = FindFirstObjectByType<TextMeshProUGUI>();
        if (canvas != null)
        {
            canvas.enabled = false;
        }
    }


    public override bool Interact()
    {
        if (canvas.enabled == false)
        {
            canvas.enabled = true;
            displayText.text = textToDisplay;
            textIsOn = true;
        }

        return false;
    }

    private void TurnOffText()
    {
        if (textIsOn == true)
        {
            canvas.enabled = false;
            textIsOn = false;
        }
    }
}
