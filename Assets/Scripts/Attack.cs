using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Attack : MonoBehaviour
{
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float attackRange = 1.25f;
    [SerializeField] private float attackCooldown = 1.5f;
    private PlayerMovement playerMovement;
    private float timeSinceLastAttack;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        timeSinceLastAttack = attackCooldown;
    }

    private void Update()
    {
        timeSinceLastAttack = Mathf.Clamp(timeSinceLastAttack + Time.deltaTime, 0f, attackCooldown);
    }

    public void AttackPressed(CallbackContext context)
    {
        if (context.started && timeSinceLastAttack == attackCooldown)
        {
            timeSinceLastAttack = 0f;
            Collider2D hit = Physics2D.OverlapBox(new Vector2(transform.position.x + ((attackRange / 2f) * (playerMovement.IsFacingRight ? 1f : -1f)), transform.position.y), new Vector2(attackRange, 2f), 0, enemyMask);
            if (hit)
            {
                Rat rat = hit.gameObject.GetComponent<Rat>();
                rat.RatHit();
            }
        }
    }
}
