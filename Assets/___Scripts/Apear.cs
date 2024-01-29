using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Apear : MonoBehaviour
{

    public TextMeshProUGUI textMeshPro;
    public float duration = 5f;

    public GameObject startBtn;
    public GameObject quitBtn;

    public TextMeshProUGUI button1;
    public TextMeshProUGUI button2;

    public GameObject gradient;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeInText());

        startBtn.SetActive(false);
        quitBtn.SetActive(false);
    }

    IEnumerator FadeInText()
    {
        float elapsedTime = 0f;
        float start = 1f;
        float end = 0f;

        while (elapsedTime < duration)
        {
            textMeshPro.outlineWidth = Mathf.Lerp(start, end, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textMeshPro.outlineWidth = end;
        
        elapsedTime = 0f;

        startBtn.SetActive(true);
        quitBtn.SetActive(true);

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(end, start, elapsedTime);

            Color Btn1Color = button1.color;
            Btn1Color.a = alpha;
            button1.color = Btn1Color;

            Color Btn2Color = button2.color;
            Btn2Color.a = alpha;
            button2.color = Btn2Color;

            Color gradientColor = gradient.GetComponent<SpriteRenderer>().color;
            gradientColor.a = alpha;
            gradient.GetComponent<SpriteRenderer>().color = gradientColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
