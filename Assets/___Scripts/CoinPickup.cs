using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinAmount = 1;

    private bool movingToPlayer;
    private float moveSpeed;
    public float initialMoveSpeed;

    public float initialKnockbackSpeed = -5f; // Начальная скорость отбрасывания.
    public float knockbackTime = .25f;
    private float knockbackCounter;

    public float timeBetweenChecks = .2f;
    private float checkCounter;

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealthController.instance.GetComponent<PlayerController>();

        moveSpeed = initialMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingToPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

            // отбрасывание
            knockbackCounter -= Time.deltaTime;
            // Вычисляем процент завершения отбрасывания.
            float knockbackPercent = (knockbackCounter / knockbackTime);


            if (knockbackCounter <= 0)
            {
                moveSpeed += (initialMoveSpeed * .1f + player.moveSpeed * .1f);
            }
            else
            {
                // Применяем уменьшение скорости отбрасывания с течением времени.
                moveSpeed = initialKnockbackSpeed * knockbackPercent;
            }
        }
        else
        {
            checkCounter -= Time.deltaTime;

            if (checkCounter < 0)
            {
                checkCounter = timeBetweenChecks;

                if (Vector3.Distance(transform.position, player.transform.position) < player.pickupRange)
                {
                    movingToPlayer = true;
                    knockbackCounter = knockbackTime;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CoinController.instance.AddCoins(coinAmount);

            Destroy(gameObject);
        }
    }
}
