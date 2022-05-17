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

        // �� ���Ӹ��� ����� ����
        shape = (CardShape)Random.Range(0, 4);

        // ���庰 ���̵�����?
        //// 1����� 1~6����
        //if (roundIdx == 0)
        //    answer = Random.Range(1, 7);
        //// 2����� 7~11����
        //else if (roundIdx == 1)
        //    answer = Random.Range(7, 12);
        //// 3����� 12���� 15����
        //else if (roundIdx == 1)
        //    answer = Random.Range(12, 16);

        // ���� ������� ������ ����
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
        // 0.5�� ���
        yield return new WaitForSeconds(0.5f);

        // ī�� ����
        uiMiniGame_Card.ShuffleCards(answer, SHUFFLE_DURATION);

        // ī�� ���µ��� ���
        yield return new WaitForSeconds(SHUFFLE_DURATION);

        // 0.5�� ���
        yield return new WaitForSeconds(0.5f);

        // ���� ����
        coCardQuestion = StartCoroutine("CardAnswers");
    }

    IEnumerator CardAnswers()
    {
        // 1�� ���
        yield return new WaitForSeconds(0.5f);

        // ��������
        // ������ ����
        uiMiniGame_Card.goQuestionBoard.SetActive(true);

        // ���� �ݺ�
        for (int i = 0; i < uiMiniGame_Card.questions.Length; i++)
        {
            uiMiniGame_Card.UpdateRound();
            uiMiniGame_Card.questions[i].gameObject.SetActive(true);
            uiMiniGame_Card.questions[i].SetAnswer(answer);

            // ���� ���⶧���� ���
            while (uiMiniGame_Card.questions[i].isAnswer == false)
                yield return null;

            score += 10;
            uiMiniGame_Card.UpdateScore();
            uiMiniGame_Card.questions[i].gameObject.SetActive(false);
            questionIdx++;
        }

        // �� ������

        // ��������!

        // ���簡 ������ �����?
        roundIdx++;
        if (roundIdx >= MAX_ROUND)
        {
            // �������尡 ������? �ʱ�ȭ������ �̵�
            ClearGame();
        }
        else
        {
            // �������尡 ������? �������� ����
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
