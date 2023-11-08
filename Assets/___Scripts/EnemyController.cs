using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator anim;


    public Rigidbody2D theRB;
    private float moveSpeed;
    public float initialMoveSpeed;
    private Transform target;
    private SpriteRenderer childSR;

    public float damage;

    public float hitWaitTime = .5f;
    public bool isColliding = false;

    public float health = 5;
    public bool isDead;

    public float initialKnockbackSpeed = -10f; // Начальная скорость отбрасывания.
    public float knockbackTime = .25f;
    private float knockbackCounter;

    public float fadeDuration = 1f;

    public int expToGive = 1;

    [SerializeField] int expSpawnChance = 70;
    PlayerHealthController playerHC => PlayerHealthController.instance;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = initialMoveSpeed;
        target = PlayerHealthController.instance.transform;
        childSR = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(knockbackCounter > 0)
        {
            GetComponent<CapsuleCollider2D>().isTrigger = true;

            // Вычисляем процент завершения отбрасывания.
            float knockbackPercent = (knockbackCounter / knockbackTime);

            // Применяем уменьшение скорости отбрасывания с течением времени.
            moveSpeed = initialKnockbackSpeed * knockbackPercent;

            knockbackCounter -= Time.deltaTime;

            if(knockbackCounter <= 0)
            {
                moveSpeed = initialMoveSpeed;
                GetComponent<CapsuleCollider2D>().isTrigger = false;
                //moveSpeed = Mathf.Abs(moveSpeed * .5f);
            }
        }

        if(!isDead)
        {
            theRB.velocity = (target.position - transform.position).normalized * moveSpeed;

            //face enemy to the player
            if (transform.position.x < playerHC.transform.position.x) 
            {
                transform.localScale = new Vector3(-1, 1, 1);
            } else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isColliding = true;
            StartCoroutine(DealDamage(damage, hitWaitTime));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isColliding = false;
        }
    }


    public IEnumerator DealDamage(float damageToTake, float hitWaitTime)
    {

        while (isColliding)
        {
            playerHC.currentHealth -= damageToTake;

            if (playerHC.currentHealth <= 0)
            {
                playerHC.gameObject.SetActive(false);
            }

            playerHC.healthSlider.value = playerHC.currentHealth;

            yield return new WaitForSeconds(hitWaitTime);
        }
    }

    public void TakeDamage(float damageToTake)
    {
        health -= damageToTake;

        if (health <= 0)
        {
            Death();
        }

        DamageNumberController.instance.SpawnDamage(damageToTake, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z));
    }

    public void Death()
    {
        isDead = true;
        GetComponent<CapsuleCollider2D>().enabled = false;
        theRB.velocity = Vector2.zero;

        // xp
        if(Random.Range(0,100) < expSpawnChance)
        {
            ExperienceLevelController.instance.SpawnExp(transform.position, expToGive);
        }    

        // if monsted don't have death animation in spritesheet we don't assign animator on the enemy controller in the inspector
        // and doind animation manually in the dotween instead;
        if (anim == null)
        {
            DefaultDeathAnim();
            Destroy(gameObject, 1f);
        }
        else
        {
            anim.SetTrigger("Dead");
            Destroy(gameObject, 1.7f);
        }
    }

    public void DefaultDeathAnim()
    {
        float duration = 1f;

        // make it smaller
        transform.DOScale(0.5f, duration);
        transform.DOMoveY(2, duration).SetRelative();

        //fade
        GetComponentInChildren<Animator>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().DOFade(0f, duration);
    }

    public void TakeDamage(float damageToTake, bool shouldKnockback)
    {
        TakeDamage(damageToTake);

        if(shouldKnockback)
        {
            knockbackCounter = knockbackTime;
            moveSpeed = initialKnockbackSpeed;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
