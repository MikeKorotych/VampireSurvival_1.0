using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DamageNumberController : MonoBehaviour
{
    public static DamageNumberController instance;

    private void Awake()
    {
        instance = this;
    }

    public DamageNumber numberToSpawn;
    public Transform numberCanvas;

    private List<DamageNumber> numberPool  = new List<DamageNumber>();

    // Start is called before the first frame update
    void Start()
    {
        float fadeDuratuion = numberToSpawn.lifetime;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnDamage(float damageAmount, Vector3 location) 
    {
        float randomFactor = 0.5f;
        float damageFZ = numberToSpawn.GetComponent<TextMeshProUGUI>().fontSize;

        // spawn location with random factor
        Vector3 newLocation = new Vector3(Random.Range(location.x - randomFactor, location.x + randomFactor), Random.Range(location.y - randomFactor, location.y + randomFactor), location.z);
        int rounded = Mathf.RoundToInt(damageAmount);

        //damage instance
        DamageNumber newDamage =  Instantiate(numberToSpawn, newLocation, Quaternion.identity, numberCanvas);

        // randomize a font size a bit
        newDamage.GetComponent<TextMeshProUGUI>().fontSize = Random.Range(damageFZ - randomFactor * 3, damageFZ + randomFactor * 3);

        // setup
        newDamage.Setup(rounded);
        newDamage.gameObject.SetActive(true);

        // anim duration
        var fadeDuration = 1f;

        var Y = Random.Range(newDamage.transform.position.y + randomFactor, newDamage.transform.position.y + randomFactor * 3);
        var X = newDamage.transform.position.x;
        // move up
        newDamage.transform.DOMoveY(Y, 1);

        // fade animation for text (Alpha to 0)
        newDamage.GetComponent<TextMeshProUGUI>().DOFade(0f, fadeDuration);

        // make it smaller
        newDamage.transform.DOScaleY(0.75f, fadeDuration);
        newDamage.transform.DOScaleX(0.75f, fadeDuration);
        newDamage.transform.DOMoveX(X - 0.25f, fadeDuration);


        // to do
        // than bigger damage than bigger font size or more red color
    }
}
