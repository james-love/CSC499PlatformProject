using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float scale;
    [SerializeField] private Transform tfPlayer;
    private float origXPos;
    private float playerOrigXPos;

    private void Start()
    {
        origXPos = transform.position.x;
        playerOrigXPos = tfPlayer.position.x;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(origXPos - ((playerOrigXPos - tfPlayer.position.x) * scale), transform.position.y, transform.position.z);
    }
}
