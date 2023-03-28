using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Animator menuAnimator;
    [SerializeField] private InputActionAsset inputs;
    private VisualElement root;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        Button back = root.Q<Button>("BackButton");
        back.RegisterCallback<ClickEvent>(_ => menuAnimator.SetTrigger("BackToMenu"));

        Toggle alwaysRun = root.Q<Toggle>("AlwaysRun");
        alwaysRun.RegisterValueChangedCallback(e => PlayerManager.Instance.SetAlwaysRun(e.newValue));

        InputAction move = inputs.FindAction("NormalMovement/Move");
        int right = move.bindings.IndexOf(b => b.name == "positive");
        int left = move.bindings.IndexOf(b => b.name == "negative");

        CreateRebind("NormalMovement/Move", "RebindLeft", left);
        CreateRebind("NormalMovement/Move", "RebindRight", right);

        CreateRebind("NormalMovement/Jump", "RebindJump");
        CreateRebind("NormalMovement/Run", "RebindRun");
        CreateRebind("NormalMovement/Sense", "RebindSense");
        CreateRebind("NormalMovement/DropDown", "RebindDropDown");
        CreateRebind("NormalMovement/Attack", "RebindAttack");
        CreateRebind("NormalMovement/Interact", "RebindInteract");

        inputs.actionMaps.First(m => m.name == "NormalMovement").Disable();
    }

    private void CreateRebind(string action, string controlName, int bindingIndex = 0)
    {
        Label overlay = root.Q<Label>("Overlay");
        InputAction iAction = inputs.FindAction(action);
        VisualElement rebind = root.Q<VisualElement>(controlName);
        new RebindControl(rebind, iAction, bindingIndex, overlay);
    }

    private void Start()
    {
        Toggle alwaysRun = root.Q<Toggle>("AlwaysRun");
        alwaysRun.value = PlayerManager.Instance.AlwaysRun;
    }
}
