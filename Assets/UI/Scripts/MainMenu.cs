using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator openingCutscene;
    [SerializeField] private InputActionAsset inputs;
    [SerializeField] private AudioClip music;
    private Animator mainMenuAnimator;

    public void QuitClicked()
    {
        Application.Quit();
    }

    public void SettingsClicked()
    {
        mainMenuAnimator.SetTrigger("SettingsClicked");
    }

    public void BackClicked()
    {
        mainMenuAnimator.SetTrigger("BackToMenu");
    }

    public void GameClicked()
    {
        StartCoroutine(LoadGame());
    }

    private void Awake()
    {
        SoundManager.Instance.PlayMusic(music);
        mainMenuAnimator = GetComponent<Animator>();
    }

    private IEnumerator LoadGame()
    {
        mainMenuAnimator.SetTrigger("GameClicked");

        yield return new WaitUntil(() => AnimationFinished(mainMenuAnimator, "OpenGame"));

        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        openingCutscene.SetTrigger("StartCutscene");

        yield return new WaitUntil(() => AnimationFinished(openingCutscene, "OpeningCutscene"));

        PlayerManager.Instance.HeartsVisible = true;
        PlayerManager.Instance.SetInteractPopupText(inputs.FindAction("NormalMovement/Interact").bindings[0].ToDisplayString());
        Time.timeScale = 1;
        inputs.actionMaps.First(m => m.name == "NormalMovement").Enable();
    }

    private bool AnimationFinished(Animator animator, string animation)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animation) &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;
    }
}
