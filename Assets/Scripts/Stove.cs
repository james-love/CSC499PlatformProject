using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{

    private float coolDown = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coolDown = Mathf.Clamp(coolDown + Time.deltaTime, 0f, 5f);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && coolDown == 5f)
        {
            coolDown = 0f;
            collision.gameObject.GetComponentInChildren<SimpleFlash>().Flash();
            PlayerManager.Instance.AdjustHealth(-1);
            print("Player takes damage");
        }
    }
}
