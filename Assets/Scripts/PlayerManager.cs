using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

    [SerializeField] private SimpleFlash flashEffect;

    public int AdjustHealth(int adjustment)
    {
        if(adjustment < 0)
        {
            flashEffect.Flash();
        }
        return currentHearts = Mathf.Clamp(currentHearts + adjustment, 0, maxHearts);
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
