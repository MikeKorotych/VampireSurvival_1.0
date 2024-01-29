using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            int healAmount = Random.Range(10, 30);
            PlayerHealthController.instance.currentHealth += healAmount;
            PlayerController.instance.HealVFX.Play();

            // play SFX
            SFXManager.instance.PlaySFXPitched(2);

            if (PlayerHealthController.instance.currentHealth > PlayerHealthController.instance.maxHealth)
            {
                PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
                PlayerHealthController.instance.healthSlider.value = PlayerHealthController.instance.currentHealth;
            }

            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
