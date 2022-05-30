using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMiniGame_Card : UIMiniGame_Base
{
    public Card[] cards;
    public Slider sliderRound;
    public TextMeshProUGUI txtScore;

    GameController_Card gameController_Card;


    private void Awake()
    {
        gameController_Card = gameController as GameController_Card;
    }

    public override void InitUI()
    {
        base.InitUI();
        UpdateScore(0);
        UpdateRound(0);
    }
    
    public void ResetCards()
    {
        // �ݺ����� �̿��� ī�� ���� �ʱ�ȭ
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].ResetCard();
        }
    }

    public void SetValues(CardShape shape)
    {
        // �ݺ����� �̿��� ī�� ���� �ڸ��� �ش��ϴ� ���ҽ� ����
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].SetValue(shape, (int)Mathf.Pow(2, i));
        }
    }

    public void ShuffleCards(int answer, float shuffleDuration)
    {
        // �ݺ����� �̿��� ī�� ���� ������
        for (int i = cards.Length - 1; i >= 0; i--)
        {
            CardDirection direction = CardDirection.Back;
            int posValue = (int)Mathf.Pow(2, i);
            if (answer >= posValue)
            {
                answer -= posValue;
                direction = CardDirection.Front;
            }
            cards[i].Shuffle(direction, shuffleDuration, i * 0.1f);
        }
    }

    public void OnClickRestart()
    {
        AudioManager.Instance.PlayClickSound();
        gameController_Card.StartGame();
    }

    public void UpdateRound(int value = -1)
    {
        if (value != -1)
            sliderRound.value = value;
        else
            sliderRound.value = gameController_Card.GetSliderStepValue();
    }

    public void UpdateScore(int value = -1)
    {
        if (value != -1)
            txtScore.text = value.ToString();
        else
            txtScore.text = gameController_Card.score.ToString();
    }
}
