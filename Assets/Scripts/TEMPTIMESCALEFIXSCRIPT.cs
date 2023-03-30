using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TEMPTIMESCALEFIXSCRIPT : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerInput>().currentActionMap.Enable();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
