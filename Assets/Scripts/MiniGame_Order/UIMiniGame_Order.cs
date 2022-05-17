using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMiniGame_Order : UIMiniGame_Base
{
    public Image[] imgMenu;
    public Slider sliderRound;
    public TextMeshProUGUI txtScore;

    GameController_Order gameController_Order;

    public void UpdateRound(int value = -1)
    {
        if (value != -1)
            sliderRound.value = value;
        else
            sliderRound.value = gameController_Order.GetSliderStepValue();
    }

    public void UpdateScore(int value = -1)
    {
        if (value != -1)
            txtScore.text = value.ToString();
        else
            txtScore.text = gameController_Order.score.ToString();
    }
}
