using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitEffect : MonoBehaviour
{
    public ParticleSystem hitEffect; // Reference to the ParticleSystem

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            hitEffect.Play();
            Debug.Log("coll enter");
        }
    }
}
