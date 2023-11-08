using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ZoneWeapon : Weapon
{
    public EnemyDamager damager;

    private float spawnTime, spawnCounter;

    // scale animation
    private float currentScale = 1f;
    private float scaleChangeSpeed = .1f;
    private float minScale = .9f;
    private float maxScale = 1.1f;
    private bool isScalingUp = true;

    // Start is called before the first frame update
    void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {

        if (statsUpdated)
        {
            statsUpdated = false;

            SetStats();
        }

        spawnCounter -= Time.deltaTime;

        if(spawnCounter <= 0f )
        {
            spawnCounter = spawnTime;

            Instantiate(damager, damager.transform.position, Quaternion.identity, transform).gameObject.SetActive(true);
        }

        //scale animation
        if (isScalingUp)
        {
            currentScale += Time.deltaTime * scaleChangeSpeed;

            if (currentScale >= maxScale)
            {
                currentScale = maxScale;
                isScalingUp = false;
            }
        }
        else
        {
            currentScale -= Time.deltaTime * scaleChangeSpeed;

            if (currentScale <= minScale)
            {
                currentScale = minScale;
                isScalingUp = true;
            }
        }

        transform.localScale = new Vector3(currentScale, currentScale, currentScale);
    }

    void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;
        damager.lifeTime = stats[weaponLevel].duration;
        damager.timeBetweenDamage = stats[weaponLevel].speed;
        damager.transform.localScale = Vector3.one *  stats[weaponLevel].range;

        spawnTime = stats[weaponLevel].timeBetweenAttacks;
        spawnCounter = 0f;
    }
}
