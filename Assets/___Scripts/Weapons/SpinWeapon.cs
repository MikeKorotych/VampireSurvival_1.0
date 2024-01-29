using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWeapon : Weapon
{
    public float rotateSpeed;
    public float globalRotateSpeed;

    public Transform holder, fireballToSpawn;
    public float timeBetweenSpawn;
    private float spawnCounter;

    public EnemyDamager damager;

    // Start is called before the first frame update
    void Start()
    {
        SetStats();

        // UIController.instance.levelUpButtons[0].UpdateButtonDisplay(this);
    }

    // Update is called once per frame
    void Update()
    {
        // weapon rotation
        globalRotateSpeed = stats[weaponLevel].speed;
        
        holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime * stats[weaponLevel].speed));

        // spawning
        spawnCounter -= Time.deltaTime;

        if(spawnCounter < 0 )
        {
            spawnCounter = timeBetweenSpawn;

            // Transform clone = Instantiate(fireballToSpawn, fireballToSpawn.position, fireballToSpawn.rotation, holder);
            //clone.gameObject.SetActive(true);

            for (int i = 0; i < stats[weaponLevel].amount; i++)
            {
                float rot = 360f / stats[weaponLevel].amount * i;

                Transform clone = Instantiate(fireballToSpawn, fireballToSpawn.position, Quaternion.Euler(0f, 0f, rot), holder);
                clone.gameObject.SetActive(true);

                SFXManager.instance.PlaySFXPitched(8);
            }
        }

        if(statsUpdated)
        {
            statsUpdated = false;

            SetStats();
        }
    }

    public void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;

        transform.localScale = Vector3.one * stats[weaponLevel].range;

        timeBetweenSpawn = stats[weaponLevel].timeBetweenAttacks;

        damager.lifeTime = stats[weaponLevel].duration;

        spawnCounter = 0f;
    }
}
