using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ZoneWeaponDOTweenAnimaiton : MonoBehaviour
{
    private float currentZAngle = 0f;
    SpriteRenderer sr => GetComponent<SpriteRenderer>();

    // fade animation
    private float currentFade = .3f;
    private float scaleChangeSpeed = .1f;
    private float minFade = .3f;
    private float maxFade = .5f;
    private bool isFadingUp = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform != null)
        {
            //rotation
            currentZAngle += Time.deltaTime * 40f; // Увеличение угла на 20 градусов в секунду
            transform.rotation = Quaternion.Euler(0f, 0f, currentZAngle);

            //fade animation
            if (isFadingUp)
            {
                currentFade += Time.deltaTime * scaleChangeSpeed;

                if (currentFade >= maxFade)
                {
                    currentFade = maxFade;
                    isFadingUp = false;
                }
            }
            else
            {
                currentFade -= Time.deltaTime * scaleChangeSpeed;

                if (currentFade <= minFade)
                {
                    currentFade = minFade;
                    isFadingUp = true;
                }
            }

            sr.color = new Color(255,255, 255, currentFade);
        }
    }
}
