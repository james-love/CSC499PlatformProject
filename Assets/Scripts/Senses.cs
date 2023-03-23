
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Senses : MonoBehaviour
{
    public GameObject playerObject;
    private Renderer player;

    Color platformColor = Color.yellow;
    Color enemyColor = Color.red;
    Color decorationColor = Color.gray;
    Color defaultColor = Color.white;
    List<SpriteRenderer> platformRenders = new();
    List<SpriteRenderer> enemyRenders = new();
    List<SpriteRenderer> decorationRenders = new();



    // Start is called before the first frame update
    void Start()
    {
        player = playerObject.GetComponent<SpriteRenderer>();
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        GameObject[] oneWays = GameObject.FindGameObjectsWithTag("OneWay");
        foreach (GameObject platform in platforms)
        {
            platformRenders.Add(platform.GetComponent<SpriteRenderer>());
        }
        foreach (GameObject oneWay in oneWays)
        {
            platformRenders.Add(oneWay.GetComponent<SpriteRenderer>());
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemyRenders.Add(enemy.GetComponent<SpriteRenderer>());
        }
        GameObject[] decorations = GameObject.FindGameObjectsWithTag("Decor");
        foreach (GameObject decoration in decorations)
        {
            decorationRenders.Add(decoration.GetComponent<SpriteRenderer>());
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
            player.material.color = defaultColor;
        }
    }
}
