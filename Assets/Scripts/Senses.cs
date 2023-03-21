using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Senses : MonoBehaviour
{
    public GameObject playerObject;
    private Renderer player;

    Color platformColor = Color.yellow;
    Color enemyColor = Color.red;
    Color decorationColor = Color.gray;
    Color defaultColor = Color.white;
    List<Renderer> platformRenders = new List<Renderer>();
    List<Renderer> enemyRenders = new List<Renderer>();
    List<Renderer> decorationRenders = new List<Renderer>();



    // Start is called before the first frame update
    void Start()
    {
        player = playerObject.GetComponent<Renderer>();
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        foreach (GameObject platform in platforms)
        {
            platformRenders.Add(platform.GetComponent<Renderer>());
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemyRenders.Add(enemy.GetComponent<Renderer>());
        }
        GameObject[] decorations = GameObject.FindGameObjectsWithTag("Decoration");
        foreach (GameObject decoration in decorations)
        {
            decorationRenders.Add(decoration.GetComponent<Renderer>());
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
