using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] private Animator wipeTransition;
    [SerializeField] private Animator circleTransition;
    public bool Loading { get; private set; }

    public void ReloadMainMenu()
    {
        Destroy(PlayerManager.Instance.gameObject);
        // Set menu music here?

        LoadLevel(0, wipeTransition);
    }

    public void ReloadGame()
    {
        // Need some logic here to skip the main menu

        LoadLevel(0, wipeTransition);
    }

    public void LoadInterior()
    {
        LoadLevel(1, circleTransition);
    }

    public void LoadLevel(int levelIndex, Animator transition)
    {
        StartCoroutine(LoadAsync(levelIndex, transition));
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator LoadAsync(int levelIndex, Animator transition)
    {
        Loading = true;
        Time.timeScale = 0;
        transition.SetTrigger("Start");
        yield return new WaitUntil(() => AnimationFinished(transition, "TransitionStart"));

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        while (!operation.isDone)
            yield return null;

        transition.SetTrigger("Loaded");
        Loading = false;
        Time.timeScale = 1;
    }

    private bool AnimationFinished(Animator animator, string animation)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animation) &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;
    }
}
