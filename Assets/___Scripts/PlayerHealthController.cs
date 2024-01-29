using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public float currentHealth, maxHealth;

    public Slider healthSlider;

    public float hitCooldown = .5f;

    EnemyController enemyController;

    public ParticleSystem deathVFX;

    public HealPotion healPotion;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyController = FindObjectOfType<EnemyController>();

        maxHealth = PlayerStatController.instance.health[0].value;
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void PlayDeathVFX()
    {
        Instantiate(deathVFX, transform.position, transform.rotation);
    }

    public void SpawnHealPotion(Vector3 position)
    {
        Instantiate(healPotion, position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //public IEnumerator TakeDamage(float damageToTake, float hitWaitTime)
    //{

    //    Debug.Log("take damage before while loop");

    //    while (enemyController.isColliding)
    //    {
    //        Debug.Log("take damage while loop");
    //        currentHealth -= damageToTake;

    //        if (currentHealth <= 0)
    //        {
    //            gameObject.SetActive(false);
    //        }

    //        healthSlider.value = currentHealth;

    //        yield return new WaitForSeconds(hitWaitTime);
    //    }
    //}
}
