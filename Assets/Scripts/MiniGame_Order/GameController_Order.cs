using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController_Order : GameController_Base
{
    public const int MAX_ROUND = 1;
    public const int QUESTION_COUNT = 5;

    bool[] questionSolved = new bool[QUESTION_COUNT];

    public UIMiniGame_Order uiMiniGame_Order;
    public int answer = 0;
    public int roundIdx = 0;
    public int questionIdx = 0;

    Coroutine coOrderQuestion = null;

    public override void InitGame()
    {
        base.InitGame();

        roundIdx = 0;
        answer = 0;
        score = 0;
        uiMiniGame_Order.InitUI();
    }

    public override void ResetGame()
    {
        base.ResetGame();

        uiMiniGame_Order.ResetQuestionBoard();
        ClearCoroutines();

        answer = 0;
        questionIdx = 0;
        for (int i = 0; i < questionSolved.Length; i++)
            questionSolved[i] = false;

    }

    public override void StartGame()
    {
        base.StartGame();

        // 문제 시작
        coOrderQuestion = StartCoroutine("OrderAnswers");
    }


    IEnumerator CardAnswers()
    {
        // 1초 대기
        yield return new WaitForSeconds(0.5f);

        // 문제시작
        // 문제판 열기
        uiMiniGame_Order.goQuestionBoard.SetActive(true);



        //answer = Random.Range(0, 16);
        //Debug.Log("round " + (roundIdx + 1) + " - answer is " + shape.ToString() + " " + answer);

        
        // 문제 반복
        for (int i = 0; i < uiMiniGame_Order.questions.Length; i++)
        {
        // 매 문제마다
        // 메뉴리셋
        // 답 리셋
        // 문제 리셋

            uiMiniGame_Order.UpdateRound();
            uiMiniGame_Order.questions[i].gameObject.SetActive(true);
            uiMiniGame_Order.questions[i].SetAnswer(answer);

            // 문제 맞출때까지 대기
            while (uiMiniGame_Order.questions[i].isAnswer == false)
                yield return null;

            score += 10;
            uiMiniGame_Order.UpdateScore();
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

    private void ClearCoroutines()
    {
        if (coOrderQuestion != null)
            StopCoroutine(coOrderQuestion);

    }

    public override void ClearGame()
    {
        base.ClearGame();
        uiMiniGame_Order.ClearGame();
    }

    public int GetSliderStepValue()
    {
        return roundIdx * QUESTION_COUNT + questionIdx + 1;
    }
}
