using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject graphics;

    [SerializeField] private GameObject settingsMenu;

    private Animator animator;

    public void QuitClicked()
    {
        print("test");
        ScreenCapture.CaptureScreenshot("C:\\Users\\vdy\\Desktop\\Ref.png");
        //Application.Quit();
    }

    public void SettingsClicked()
    {
        animator.SetTrigger("SettingsClicked");
    }

    public void BackClicked()
    {
        animator.SetTrigger("BackToMenu");
    }

    public void GameClicked()
    {
        animator.SetTrigger("GameClicked");
    }

    private void Awake()
    {
        graphics.SetActive(true);
        animator = GetComponent<Animator>();
    }
}
