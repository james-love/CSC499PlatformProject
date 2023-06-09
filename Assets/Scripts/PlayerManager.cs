using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

// TODO: Split into a player manager and a HUD manager
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    [SerializeField] private int maxHearts;
    [SerializeField] private int currentHearts;
    [HideInInspector] public bool HeartsVisible;

    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private List<Image> hearts = new();
    private Image heartContainer;

    [HideInInspector] public bool AlwaysRun = false;

    [SerializeField] private CanvasGroup interactPopupContainer;
    [SerializeField] private TextMeshProUGUI interactPopupText;

    [SerializeField] private UIDocument deathScreen;


    [SerializeField] private AudioClip death;

    public int AdjustHealth(int adjustment)
    {
        currentHearts = Mathf.Clamp(currentHearts + adjustment, 0, maxHearts);

        if (currentHearts == 0)
            StartCoroutine(DeathAnimation());

        return currentHearts;
    }

    public void SetAlwaysRun(bool newValue)
    {
        AlwaysRun = newValue;
        PlayerPrefs.SetInt("AlwaysRun", AlwaysRun ? 1 : 0);
    }

    public void SetInteractPopupVisibility(bool newValue)
    {
        interactPopupContainer.alpha = newValue ? 1f : 0f;
    }

    public void SetInteractPopupText(string newValue)
    {
        interactPopupText.SetText(newValue);
        LayoutRebuilder.ForceRebuildLayoutImmediate(interactPopupContainer.GetComponent<RectTransform>());
    }

    private IEnumerator DeathAnimation()
    {
        SoundManager.Instance.PlaySound(death);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Animator playerAnim = player.GetComponentInChildren<Animator>();
        player.GetComponentInChildren<PlayerInput>().currentActionMap.Disable();
        playerAnim.SetTrigger("Death");
        yield return new WaitUntil(() => AnimationFinished(playerAnim, "jimmy_dies"));
        Time.timeScale = 0;
        deathScreen.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    private bool AnimationFinished(Animator animator, string animation)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animation) &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            heartContainer = gameObject.GetComponentInChildren<Image>();

            for (int i = 0; i < maxHearts; i++)
            {
                Image temp = Instantiate(heartContainer, gameObject.transform);
                temp.rectTransform.position = new Vector3(temp.rectTransform.position.x + 175 + (125 * i), temp.rectTransform.position.y, temp.rectTransform.position.z);
                hearts.Add(temp);
            }

            HeartsVisible = false;
            SetInteractPopupVisibility(false);
            if (PlayerPrefs.HasKey("AlwaysRun"))
                AlwaysRun = PlayerPrefs.GetInt("AlwaysRun") == 1;
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerInput>().currentActionMap.Disable();
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        hearts.ForEach(heart =>
        {
            if (!HeartsVisible)
            {
                heart.enabled = false;
            }
            else
            {
                heart.enabled = true;
                if (hearts.IndexOf(heart) < currentHearts)
                    heart.sprite = fullHeart;
                else
                    heart.sprite = emptyHeart;
            }
        });
    }
}
