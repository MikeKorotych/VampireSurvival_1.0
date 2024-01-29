using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalRotation : MonoBehaviour
{
    float rotatespeed => FindObjectOfType<SpinWeapon>().rotateSpeed;
    float globalRotateSpeed => FindObjectOfType<SpinWeapon>().globalRotateSpeed;
    SpinWeapon spinWeapon => FindObjectOfType<SpinWeapon>();

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        // stabilize roration
        // -90 magic number)
        transform.rotation = Quaternion.Euler(0f, 0f, -90 - (rotatespeed * Time.deltaTime * globalRotateSpeed));
    }

}
