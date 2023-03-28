using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Animator menuAnimator;
    [SerializeField] private InputActionAsset inputs;
    private VisualElement root;

    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        Toggle alwaysRun = root.Q<Toggle>("AlwaysRun");
        alwaysRun.RegisterValueChangedCallback(e => PlayerManager.Instance.SetAlwaysRun(e.newValue));
        alwaysRun.value = PlayerManager.Instance.AlwaysRun;

        Button back = root.Q<Button>("BackButton");
        back.RegisterCallback<ClickEvent>(_ => menuAnimator.SetTrigger("BackToMenu"));

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

    private void OnEnable()
    {
        string rebinds = PlayerPrefs.GetString("rebinds");
        if (!string.IsNullOrEmpty(rebinds))
            inputs.LoadBindingOverridesFromJson(rebinds);
    }

    private void OnDisable()
    {
        string rebinds = inputs.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
    }

    private void CreateRebind(string action, string controlName, int bindingIndex = 0)
    {
        Label overlay = root.Q<Label>("Overlay");
        InputAction iAction = inputs.FindAction(action);
        VisualElement rebind = root.Q<VisualElement>(controlName);
        new RebindControl(rebind, iAction, bindingIndex, overlay);
    }
}
