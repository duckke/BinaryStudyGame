using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController_Card : GameController_Base
{
    const int MAX_ROUND = 3;
    float SHUFFLE_DURATION = 5f;
    public UIMiniGame_Card uiMiniGame_Card;
    public int round = 0;
    public int answer = 0;
    CardShape shape = CardShape.Spade;

    public override void InitGame()
    {
        base.InitGame();

        uiMiniGame_Card.InitUI();
        round = 0;
    }

    public override void ResetGame()
    {
        base.ResetGame();

        // �� ���Ӹ��� ����� ����
        shape = (CardShape)Random.Range(0, 4);

        // 1����� 1~6����
        if (round == 0)
            answer = Random.Range(1, 7);
        // 2����� 7~11����
        else if (round == 1)
            answer = Random.Range(7, 12);
        // 3����� 12���� 15����
        else if (round == 1)
            answer = Random.Range(12, 16);

        Debug.Log("round " + (round + 1) + " - answer is " + shape.ToString() + " " + answer);
        

        uiMiniGame_Card.ResetCards();
        uiMiniGame_Card.SetValues(shape);
    }

    public override void StartGame()
    {
        base.StartGame();


        //StartCoroutine("ShuffleCard");
    }

    IEnumerator ShuffleCard()
    {
        while (true)
        {            
            uiMiniGame_Card.ShuffleCards(answer, SHUFFLE_DURATION);
            yield return new WaitForSeconds(SHUFFLE_DURATION);
        }
    }
}
