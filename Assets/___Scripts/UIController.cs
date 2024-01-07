using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    private void Awake()
    {
        instance = this;
    }

    public Slider expLvlSlider;
    public TextMeshProUGUI expLvlText;

    public LevelUpSelectionButton[] levelUpButtons;

    public GameObject levelPanel;

    public TMP_Text coinText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateExp(int currentExp, int levelExp, int currentLvl)
    {
        expLvlSlider.maxValue = levelExp;
        expLvlSlider.value = currentExp;

        expLvlText.text = "Level: " + currentLvl;
    }

    public void SkipLevelUp()
    {
        levelPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void UpdateCoins()
    {
        coinText.text = "Coins: " + CoinController.instance.currentCoins;
    }
}
