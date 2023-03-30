using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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



    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {

    }

    public void JimmySense(CallbackContext context)
    {
        if (context.started)
        {
            SoundManager.Instance.PlaySound(meow);
            foreach (Renderer r in platformRenders)
            {
                r.material.color = platformColor;
            }
            foreach (Renderer r in enemyRenders)
            {
                r.material.color = enemyColor;
            }
            foreach (Renderer r in decorationRenders)
            {
                r.material.color = decorationColor;
            }
            foreach (Renderer r in oneWayRenders)
            {
                r.material.color = onewayColor;
            }
            foreach (Renderer r in iceRenders)
            {
                r.material.color = iceColor;
            }
            player.material.color = decorationColor;

        }

        else if (context.canceled)
        {
            foreach (Renderer r in platformRenders)
            {
                r.material.color = defaultColor;
            }
            foreach (Renderer r in enemyRenders)
            {
                r.material.color = defaultColor;
            }
            foreach (Renderer r in decorationRenders)
            {
                r.material.color = defaultColor;
            }
            foreach (Renderer r in oneWayRenders)
            {
                r.material.color = defaultColor;
            }
            foreach (Renderer r in iceRenders)
            {
                r.material.color = defaultColor;
            }
            player.material.color = defaultColor;
        }
    }
}
