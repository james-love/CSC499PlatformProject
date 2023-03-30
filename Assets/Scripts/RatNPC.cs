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
            dialog[0].SetActive(true);
            dialogStep += 1;
        }
        else
        {
            dialog[dialogStep - 1].SetActive(false);
            dialog[dialogStep].SetActive(true);
            dialogStep += 1;
        }
    }

    void Start()
    {
        foreach (GameObject bubble in dialog)
        {
            bubble.SetActive(false);
        }
    }

    void Update()
    {
        
    }
}
