using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }

    public float moveSpeed = 2;
    public Animator anim;
    private bool isMoving;

    public Transform spriteTransform;

    public float pickupRange = 1.5f;

    // public Weapon activeWeapon;

    public List<Weapon> unassignedWeapons, assignedWeapons;
    public int maxWeapons = 3;

    [HideInInspector]
    public List<Weapon> fullyLeveledWeapons = new List<Weapon>();

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<PlayerController>();

        if (assignedWeapons.Count == 0)
        {
            AddWeapon(Random.Range(0, unassignedWeapons.Count));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = new Vector3(0,0,0);
        moveInput.x = Input.GetAxisRaw("Horizontal");

        // flip char

        if(moveInput.x < 0)
            spriteTransform.localScale = new Vector3(-1f, 1f,1f); 
        else if(moveInput.x > 0)
            spriteTransform.localScale = Vector3.one;

        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        transform.position += moveInput * Time.deltaTime * moveSpeed;

        if(moveInput != Vector3.zero)
        {
            isMoving = true;
            anim.SetBool("isMoving", isMoving);
        } else
        {
            isMoving = false;
            anim.SetBool("isMoving", isMoving);
        }
    }

    public void AddWeapon(int weaponNumber)
    {
        if (weaponNumber < unassignedWeapons.Count) 
        {
            assignedWeapons.Add(unassignedWeapons[weaponNumber]);

            unassignedWeapons[weaponNumber].gameObject.SetActive(true);
            unassignedWeapons.RemoveAt(weaponNumber);
        }
    }

    public void AddWeapon(Weapon weaponToAdd)
    {
        weaponToAdd.gameObject.SetActive(true);

        assignedWeapons.Add(weaponToAdd);
        unassignedWeapons.Remove(weaponToAdd);
    }
}
