using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatNPC : Interactable
{
    [SerializeField] private GameObject[] dialog;

    private int dialogStep = 0;
    Color clearColor = Color.clear;
    Color defaultColor = Color.white;

    private List<SpriteRenderer> dialogRenders = new();

    public override void Interact()
    {
        if(dialogStep == dialogRenders.Count)
        {
            print("End of game");
        }

        if(dialogStep == 0)
        {
            dialogRenders[0].enabled = true;
            dialogStep += 1;
        }
        else
        {
            dialogRenders[dialogStep - 1].enabled = false;
            dialogRenders[dialogStep].enabled = true;
            dialogStep += 1;
        }
    }

    void Start()
    {
        foreach (GameObject bubble in dialog)
        {
            dialogRenders.Add(bubble.GetComponent<SpriteRenderer>());
            bubble.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void Update()
    {
        
    }
}
