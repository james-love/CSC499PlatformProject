using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject graphics;
    [SerializeField] private Animator openingCutscene;
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
        graphics.SetActive(true);
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

        Hearts.Instance.Visible = true;
        Time.timeScale = 1;
    }

    private bool AnimationFinished(Animator animator, string animation)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animation) &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;
    }
}
