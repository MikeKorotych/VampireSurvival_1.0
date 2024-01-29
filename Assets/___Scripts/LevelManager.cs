using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }

    private bool gameActive;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        gameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameActive)
        {
            timer += Time.deltaTime;
            UIController.instance.UpdateTimer(timer);
        }
    }

    IEnumerator ShowEndLevelScreen()
    {
        yield return new WaitForSeconds(1f);

        UIController.instance.levelEndScreen.SetActive(true);
    }

   public void EndLevel()
    {
        gameActive = false;

        float minutes = Mathf.FloorToInt(timer / 60f);
        float seconds = Mathf.FloorToInt(timer % 60);

        UIController.instance.endTimeText.text = minutes.ToString() + " min " + seconds.ToString("00") + " sec";

        StartCoroutine(ShowEndLevelScreen());
    }
}
