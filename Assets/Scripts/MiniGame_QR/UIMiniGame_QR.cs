using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMiniGame_QR : UIMiniGame_Base
{
    public Image[] imgMenu;
    public Text[] txtMenuNum;
    public Slider sliderRound;
    public TextMeshProUGUI txtScore;

    GameController_QR gameController_QR;

    private void Awake()
    {
        gameController_QR = gameController as GameController_QR;
    }

    public override void InitUI()
    {
        base.InitUI();
        UpdateScore(0);
        UpdateRound(0);
    }

    public void UpdateRound(int value = -1)
    {
        if (value != -1)
            sliderRound.value = value;
        else
            sliderRound.value = gameController_QR.GetSliderStepValue();
    }

    public void UpdateMenuTarget(int[] answers)
    {
        for (int i = 0; i < imgMenu.Length; i++)
        {
            imgMenu[i].sprite = Resources.Load<Sprite>(string.Format("Sprites/MiniGame_QR/menus/{0}", answers[i]));
            txtMenuNum[i].text = answers[i].ToString();
        }
    }

    public void UpdateScore(int value = -1)
    {
        if (value != -1)
            txtScore.text = value.ToString();
        else
            txtScore.text = gameController_QR.score.ToString();
    }
}
