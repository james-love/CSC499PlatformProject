using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class RatNPC : Interactable
{
    [SerializeField] private GameObject[] dialog;
    [SerializeField] private PlayerInput playerInput;

    private int dialogStep = 0;
    private VisualElement root;

    public override void Interact()
    {
        print(dialogStep);
        if (dialogStep == dialog.Length)
        {
            Time.timeScale = 0;
            playerInput.currentActionMap.Disable();
            root.style.display = DisplayStyle.Flex;
        }
        else if (dialogStep == 0)
        {
            playerInput.currentActionMap.FindAction("Move").Disable();
            playerInput.currentActionMap.FindAction("Jump").Disable();
            playerInput.currentActionMap.FindAction("Run").Disable();
            playerInput.currentActionMap.FindAction("Sense").Disable();
            playerInput.currentActionMap.FindAction("DropDown").Disable();
            playerInput.currentActionMap.FindAction("Attack").Disable();

            dialog[0].SetActive(true);
            dialogStep += 1;
        }
        else
        {
            dialog[dialogStep - 1].SetActive(false);
            dialog[dialogStep].SetActive(true);
            dialogStep += 1;
        }
    }

    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        foreach (GameObject bubble in dialog)
        {
            bubble.SetActive(false);
        }
    }
}
