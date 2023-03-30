using UnityEngine;
using UnityEngine.UIElements;

public class EndScreen : MonoBehaviour
{
    private VisualElement root;

    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        root.style.display = DisplayStyle.None;

        Button mainMenu = root.Query<Button>("MainMenu");
        mainMenu.RegisterCallback<ClickEvent>(_ =>
        {
            root.style.display = DisplayStyle.None;
            LevelManager.Instance.ReloadMainMenu();
        });

        Button quit = root.Query<Button>("Quit");
        quit.RegisterCallback<ClickEvent>(_ => Application.Quit());
    }
}
