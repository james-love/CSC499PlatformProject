using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class RebindControl
{
    private InputActionRebindingExtensions.RebindingOperation rebindOperation;
    private int bindingIndex;
    private InputAction action;
    private Button rebindButton;
    private Label overlay;

    public RebindControl(VisualElement control, InputAction action, int bindingIndex, Label overlay)
    {
        this.bindingIndex = bindingIndex;
        this.action = action;
        this.overlay = overlay;

        rebindButton = control.Q<Button>("Rebind");
        Button resetBtn = control.Q<Button>("Reset");

        rebindButton.clicked += PerformInteractiveRebind;
        resetBtn.clicked += ResetToDefault;

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        InputBinding binding = action.bindings[bindingIndex];
        rebindButton.text = binding.ToDisplayString();
    }

    private void ResetToDefault()
    {
        action.RemoveBindingOverride(bindingIndex);
        UpdateDisplay();
    }

    private bool CheckDuplicateBinding()
    {
        InputBinding newBinding = action.bindings[bindingIndex];
        foreach (InputBinding binding in action.actionMap.bindings)
        {
            if (binding.id == newBinding.id)
                continue;
            if (binding.effectivePath == newBinding.effectivePath)
                return true;
        }

        return false;
    }

    private void Overlay(bool show = true, string text = null)
    {
        if (!string.IsNullOrEmpty(text))
            overlay.text = text;
        if (show)
            overlay.style.display = DisplayStyle.Flex;
        else
            overlay.style.display = DisplayStyle.None;
    }

    private void PerformInteractiveRebind()
    {
        rebindOperation?.Cancel();

        void CleanUp()
        {
            rebindOperation?.Dispose();
            rebindOperation = null;
        }

        rebindOperation = action.PerformInteractiveRebinding(bindingIndex)
            .OnCancel(
                operation =>
                {
                    CleanUp();
                    UpdateDisplay();
                    Overlay(false, "Waiting for input...");
                })
            .OnComplete(
                operation =>
                {
                    if (CheckDuplicateBinding())
                    {
                        Overlay(true, "Key already in use. Try again.");
                        action.RemoveBindingOverride(bindingIndex);
                        CleanUp();
                        PerformInteractiveRebind();
                        return;
                    }

                    CleanUp();
                    UpdateDisplay();
                    Overlay(false, "Waiting for input...");
                });

        Overlay();

        rebindOperation.Start();
    }
}
