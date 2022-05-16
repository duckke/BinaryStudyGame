using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMiniGame_Card : UIMiniGame_Base
{
    public Card[] cards;
    GameController_Card gameController_Card;

    private void Awake()
    {
        gameController_Card = gameController as GameController_Card;
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
        gameController_Card.StartGame();
    }

}
