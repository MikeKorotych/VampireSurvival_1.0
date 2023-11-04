using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyDamager : MonoBehaviour
{

    public float damageAmount;
    public float lifeTime, growSpeed = 3f;
    private Vector3 targetSize;

    public bool shouldKnockback;
    public float critChanse;

    public bool damageOverTime;
    public float timeBetweenDamage;
    private float damageCounter;

    private List<EnemyController> enemiesInRange = new List<EnemyController>();

    public bool destroyOnImpact;

    // Start is called before the first frame update
    void Start()
    {
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
    }
    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);

        lifeTime -= Time.deltaTime;
        if(lifeTime < 0)
        {
            targetSize = Vector3.zero;
            if (transform.localScale.x == 0f)
            {
                Destroy(gameObject);
            }
        }

        if (damageOverTime == true)
        {
            damageCounter -= Time.deltaTime;

            if(damageCounter <= 0)
            {
                damageCounter = timeBetweenDamage;

                for (int i = 0; i < enemiesInRange.Count; i++)
                {
                    if (enemiesInRange[i] != null)
                    {
                        enemiesInRange[i].TakeDamage(damageAmount, shouldKnockback);
                    } 
                    else
                    {
                        enemiesInRange.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (damageOverTime == false)
        {
            if (collision.CompareTag("Enemy"))
            {
                if(Random.Range(0, 100) > critChanse)
                {
                    collision.GetComponent<EnemyController>().TakeDamage(damageAmount, shouldKnockback);
                } 
                else
                {
                    collision.GetComponent<EnemyController>().TakeDamage(damageAmount * 1.5f, shouldKnockback);
                }

                if(destroyOnImpact)
                {
                    Destroy(gameObject);
                }
            }
        } 
        else
        {
            if(collision.tag == "Enemy")
            {
                enemiesInRange.Add(collision.GetComponent<EnemyController>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (damageOverTime == true)
        {
            if(collision.tag == "Enemy")
            {
                enemiesInRange.Remove(collision.GetComponent<EnemyController>());
            }
        }
    }
}
