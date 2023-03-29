using UnityEngine;
using UnityEngine.UIElements;

public class DeathScreen : MonoBehaviour
{
    private void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        root.style.display = DisplayStyle.None;

        Button mainMenu = root.Q<Button>("MainMenu");
        mainMenu.RegisterCallback<ClickEvent>(_ => LevelManager.Instance.ReloadMainMenu());
    }
}
