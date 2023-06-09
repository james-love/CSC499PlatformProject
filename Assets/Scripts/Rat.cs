using UnityEngine;

public class Rat : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed;
    [SerializeField] private AudioClip squeak;
    [SerializeField] private AudioClip hurt;

    private Rigidbody2D rb;
    private int waypointIndex = 0;
    private float coolDown = 3f;
    private SpriteRenderer graphics;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        graphics = GetComponentInChildren<SpriteRenderer>();
        if (Random.value < 0.5)
            graphics.GetComponent<Animator>().SetTrigger("PlayAltWalk");
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, waypoints[waypointIndex].position) < 0.5f)
            waypointIndex++;
        if (waypointIndex == waypoints.Length)
            waypointIndex = 0;

        rb.velocity = new Vector2(speed * (transform.position.x < waypoints[waypointIndex].position.x ? 1f : -1f), 0f);
        coolDown = Mathf.Clamp(coolDown + Time.deltaTime, 0f, 5f);

        if (!(rb.velocity.x >= 0f))
            graphics.flipX = true;
        else
            graphics.flipX = false;
    }

    public void RatHit()
    {
        SoundManager.Instance.PlaySound(squeak);
        print("Rat Killed!");
        //TODO: Death Animation
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && coolDown == 5f)
        {
            SoundManager.Instance.PlaySound(hurt);
            coolDown = 0f;
            collision.gameObject.GetComponentInChildren<SimpleFlash>().Flash();
            PlayerManager.Instance.AdjustHealth(-1);
        }
    }
}
