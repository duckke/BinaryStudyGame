using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController_Card : GameController_Base
{
    public const int MAX_ROUND = 3;
    public const int QUESTION_COUNT = 2;

    bool[] questionSolved = new bool[QUESTION_COUNT];
    float SHUFFLE_DURATION = 1f;
    public UIMiniGame_Card uiMiniGame_Card;
    public int answer = 0;
    public int roundIdx = 0;
    public int questionIdx = 0;
    CardShape shape = CardShape.Spade;

    Coroutine coCardShuffle = null;
    Coroutine coCardQuestion = null;

    public override void InitGame()
    {
        base.InitGame();
        
        roundIdx = 0;
        answer = 0;
        score = 0;
        uiMiniGame_Card.InitUI();
    }

    private void ClearCoroutines()
    {
        if (coCardShuffle != null)
            StopCoroutine(coCardShuffle);
        if (coCardQuestion != null)
            StopCoroutine(coCardQuestion);

    }

    public override void ResetGame()
    {
        base.ResetGame();

        uiMiniGame_Card.ResetQuestionBoard();
        ClearCoroutines();

        answer = 0;
        questionIdx = 0;
        for (int i = 0; i < questionSolved.Length; i++)
            questionSolved[i] = false;

        // 매 게임마다 모양은 랜덤
        shape = (CardShape)Random.Range(0, 4);

        // 라운드별 난이도조절?
        //// 1라운드는 1~6까지
        //if (roundIdx == 0)
        //    answer = Random.Range(1, 7);
        //// 2라운드는 7~11까지
        //else if (roundIdx == 1)
        //    answer = Random.Range(7, 12);
        //// 3라운드는 12에서 15까지
        //else if (roundIdx == 1)
        //    answer = Random.Range(12, 16);

        // 라운드 상관없이 정답은 랜덤
        answer = Random.Range(0, 16);

        Debug.Log("round " + (roundIdx + 1) + " - answer is " + shape.ToString() + " " + answer);
        

        uiMiniGame_Card.ResetCards();
        uiMiniGame_Card.SetValues(shape);
    }

    public override void StartGame()
    {
        base.StartGame();

        coCardShuffle = StartCoroutine("ShuffleCard");
    }

    IEnumerator ShuffleCard()
    {
        // 0.5초 대기
        yield return new WaitForSeconds(0.5f);

        // 카드 섞기
        uiMiniGame_Card.ShuffleCards(answer, SHUFFLE_DURATION);

        // 카드 섞는동안 대기
        yield return new WaitForSeconds(SHUFFLE_DURATION);

        // 0.5초 대기
        yield return new WaitForSeconds(0.5f);

        // 문제 시작
        coCardQuestion = StartCoroutine("CardAnswers");
    }

    IEnumerator CardAnswers()
    {
        // 1초 대기
        yield return new WaitForSeconds(0.5f);

        // 문제시작
        // 문제판 열기
        uiMiniGame_Card.goQuestionBoard.SetActive(true);

        // 문제 반복
        for (int i = 0; i < uiMiniGame_Card.questions.Length; i++)
        {
            uiMiniGame_Card.UpdateRound();
            uiMiniGame_Card.questions[i].gameObject.SetActive(true);
            uiMiniGame_Card.questions[i].SetAnswer(answer);

            // 문제 맞출때까지 대기
            while (uiMiniGame_Card.questions[i].isAnswer == false)
                yield return null;

            score += 10;
            uiMiniGame_Card.UpdateScore();
            uiMiniGame_Card.questions[i].gameObject.SetActive(false);
            questionIdx++;
        }

        // 다 맞췄음

        // 다음라운드!

        // 현재가 마지막 라운드면?
        roundIdx++;
        if (roundIdx >= MAX_ROUND)
        {
            // 다음라운드가 없으면? 초기화면으로 이동
            ClearGame();
        }
        else
        {
            // 다음라운드가 잇으면? 다음라운드 시작
            StartGame();
        }
    }

    public override void ClearGame()
    {
        base.ClearGame();
        uiMiniGame_Card.ClearGame();
    }

    public int GetSliderStepValue()
    {
        return roundIdx * QUESTION_COUNT + questionIdx + 1;
    }
}
