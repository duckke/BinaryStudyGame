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

        // 매 게임마다 모양은 랜덤
        shape = (CardShape)Random.Range(0, 4);

        // 1라운드는 1~6까지
        if (round == 0)
            answer = Random.Range(1, 7);
        // 2라운드는 7~11까지
        else if (round == 1)
            answer = Random.Range(7, 12);
        // 3라운드는 12에서 15까지
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
