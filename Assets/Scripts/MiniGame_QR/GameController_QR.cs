using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController_QR : GameController_Base
{
    public const int MAX_ROUND = 1;
    public const int QUESTION_COUNT = 3;

    bool[] questionSolved = new bool[QUESTION_COUNT];

    public UIMiniGame_QR uiMiniGame_QR;

    public int[] answers = new int[4];
    public int roundIdx = 0;
    public int questionIdx = 0;

    Coroutine coQrQuestion = null;

    public override void InitGame()
    {
        base.InitGame();

        roundIdx = 0;
        for (int i = 0; i < answers.Length; i++)
            answers[i] = 0;

        score = 0;
        uiMiniGame_QR.InitUI();
    }

    public override void ResetGame()
    {
        base.ResetGame();

        uiMiniGame_QR.ResetQuestionBoard();
        ClearCoroutines();

        for (int i = 0; i < answers.Length; i++)
            answers[i] = 0;
        questionIdx = 0;
        for (int i = 0; i < questionSolved.Length; i++)
            questionSolved[i] = false;

    }

    public override void StartGame()
    {
        base.StartGame();

        // ���� ����
        coQrQuestion = StartCoroutine("QRQuestion");
    }


    IEnumerator QRQuestion()
    {
        // 1�� ���
        yield return new WaitForSeconds(0.3f);




        //answer = Random.Range(0, 16);
        //Debug.Log("round " + (roundIdx + 1) + " - answer is " + shape.ToString() + " " + answer);


        // ���� �ݺ�
        for (int i = 0; i < QUESTION_COUNT; i++)
        {
            for (int j = 0; j < answers.Length; j++)
                answers[j] = 0;

            for (int j = 0; j < 4; j++)
            {
                while (answers[j] == 0)
                {
                    int tmpAnswer = Random.Range(1, 32); // 1~31���� ����
                    if (j == 0 || answers[j - 1] != tmpAnswer)
                    {
                        answers[j] = tmpAnswer;
                    }
                }
            }

            Debug.Log("question " + (questionIdx + 1) + " - answer is " + 
                answers[0] + ", " +
                answers[1] + ", " +
                answers[2] + ", " +
                answers[3]);


            // �� ��������
            // �޴�����
            // �� ����
            // ���� ����
            // ��������
            // ������ ����

            uiMiniGame_QR.goQuestionBoard.SetActive(true);

            uiMiniGame_QR.UpdateRound();
            uiMiniGame_QR.UpdateMenuTarget(answers);
            uiMiniGame_QR.questions[0].gameObject.SetActive(true);
            uiMiniGame_QR.questions[0].ResetUI();
            uiMiniGame_QR.questions[0].SetAnswers(answers);


            // ���� ���⶧���� ���
            while (uiMiniGame_QR.questions[0].isAnswer == false)
                yield return null;

            score += 10;
            uiMiniGame_QR.UpdateScore();
            questionIdx++;
            AudioManager.Instance.PlaySound(SoundEnum.Success, 0.6f);
        }

        // �� ������

        // ��������!

        // ���簡 ������ �����?
        roundIdx++;
        if (roundIdx >= MAX_ROUND)
        {
            AudioManager.Instance.PlaySound(SoundEnum.GameClear, 0.8f);
            // �������尡 ������? �ʱ�ȭ������ �̵�
            yield return new WaitForSeconds(0.5f);
            ClearGame();
        }
        else
        {
            // �������尡 ������? �������� ����
            StartGame();
        }
    }

    private void ClearCoroutines()
    {
        if (coQrQuestion != null)
            StopCoroutine(coQrQuestion);

    }

    public override void ClearGame()
    {
        base.ClearGame();
        uiMiniGame_QR.ClearGame();
    }

    public int GetSliderStepValue()
    {
        return roundIdx * QUESTION_COUNT + questionIdx + 1;
    }
}
