using Cinemachine;
using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject graphics;
    //[SerializeField] private CinemachineBrain camBrain;

    private Animator animator;

    public void QuitClicked()
    {
        Application.Quit();
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
        StartCoroutine(LoadGame());
    }

    private void Awake()
    {
        graphics.SetActive(true);
        animator = GetComponent<Animator>();
    }

    private IEnumerator LoadGame()
    {
        animator.SetTrigger("GameClicked");

        yield return new WaitUntil(AnimationFinished);

        //camBrain.enabled = true;
        Time.timeScale = 1;
    }

    private bool AnimationFinished()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("OpenGame") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;
    }
}
