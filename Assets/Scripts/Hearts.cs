using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    public static Hearts Instance;

    [SerializeField] private int maxHearts;
    [SerializeField] private int currentHearts;
    [HideInInspector] public bool Visible;


    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private List<Image> hearts = new();
    private Image heartContainer;

    public int AdjustHealth(int adjustment)
    {
        return currentHearts = Mathf.Clamp(currentHearts + adjustment, 0, maxHearts);
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
            Visible = false;
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
            if (!Visible)
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
