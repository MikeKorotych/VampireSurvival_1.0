using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DestroyGameObj()
    {
        Destroy(gameObject);
    }

    private void CanNotMove() {
        GetComponentInParent<CapsuleCollider2D>().enabled = false;
        GetComponentInParent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    private void CanMove()
    {
        GetComponentInParent<CapsuleCollider2D>().enabled = true;
        GetComponentInParent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}
