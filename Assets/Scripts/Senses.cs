using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Senses : MonoBehaviour
{
    public GameObject playerObject;
    private Renderer player;
    public AudioClip meow;

    Color platformColor = Color.yellow;
    Color enemyColor = Color.red;
    Color decorationColor = Color.gray;
    Color iceColor = Color.cyan;
    Color onewayColor = Color.magenta;
    Color defaultColor = Color.white;
    List<SpriteRenderer> platformRenders = new();
    List<SpriteRenderer> enemyRenders = new();
    List<SpriteRenderer> decorationRenders = new();
    List<SpriteRenderer> oneWayRenders = new();
    List<SpriteRenderer> iceRenders = new();

    private void Start()
    {
        player = playerObject.GetComponent<SpriteRenderer>();
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        foreach (GameObject platform in platforms)
        {
            platformRenders.Add(platform.GetComponent<SpriteRenderer>());
        }
        GameObject[] oneways = GameObject.FindGameObjectsWithTag("OneWay");
        foreach (GameObject one in oneways)
        {
            oneWayRenders.Add(one.GetComponent<SpriteRenderer>());
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemyRenders.Add(enemy.GetComponent<SpriteRenderer>());
        }
        GameObject[] decorations = GameObject.FindGameObjectsWithTag("Decoration");
        foreach (GameObject decoration in decorations)
        {
            decorationRenders.Add(decoration.GetComponent<SpriteRenderer>());
        }
        GameObject[] iceSpills = GameObject.FindGameObjectsWithTag("Ice");
        foreach (GameObject ice in iceSpills)
        {
            iceRenders.Add(ice.GetComponent<SpriteRenderer>());
        }


    }

    private void SetColor(List<SpriteRenderer> renderers, Color color)
    {
        renderers.ForEach(r =>
        {
            if (r != null)
                r.material.color = color;
        });
    }

    public void JimmySense(CallbackContext context)
    {
        if (context.started)
        {
            SoundManager.Instance.PlaySound(meow);
            SetColor(platformRenders, platformColor);
            SetColor(enemyRenders, enemyColor);
            SetColor(decorationRenders, decorationColor);
            SetColor(oneWayRenders, onewayColor);
            SetColor(iceRenders, iceColor);
            player.material.color = decorationColor;
        }
        else if (context.canceled)
        {
            SetColor(platformRenders, defaultColor);
            SetColor(enemyRenders, defaultColor);
            SetColor(decorationRenders, defaultColor);
            SetColor(oneWayRenders, defaultColor);
            SetColor(iceRenders, defaultColor);
            player.material.color = defaultColor;
        }
    }
}
