

using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Question_QRMenuSelect : Question_Base
{
    const int row = 4;
    const int col = 5;
    public Text[] txtAnswerTargetNum = new Text[row];
    public Image[] imgAnswerTargetIcon = new Image[row];
    public GameObject[,] goGridObj = new GameObject[row, col];
    public Button[,] btns = new Button[row, col];
    public Image[,] imgSelected = new Image[row, col];
    //public Text txtAnswer;

    private void Awake()
    {
        LinkComponents();
    }

    private void LinkComponents()
    {
        // answer Target
        for (int i = 0; i < row; i++)
        {
            imgAnswerTargetIcon[i] = transform.Find(string.Format("GridMenuTarget/ImageTargetMenu ({0})", i)).GetComponent<Image>();
            txtAnswerTargetNum[i] = transform.Find(string.Format("GridMenuTarget/ImageTargetMenu ({0})/imgMenuNumBg/txtMenuNum", i)).GetComponent<Text>();
        }

        // answer Board
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                goGridObj[i, j] = transform.Find(string.Format("GridAnswerBoard/GameObject ({0})", (i * row + j))).gameObject;
                btns[i, j] = transform.Find(string.Format("GridAnswerBoard/GameObject ({0})/btnSquare ({0})", (i * row + j))).GetComponent<Button>();
                imgSelected[i, j] = transform.Find(string.Format("GridAnswerBoard/GameObject ({0})/btnSquare ({0})/Image ({0})", (i * row + j))).GetComponent<Image>();
            }
        }
    }
    
    public override void ResetUI()
    {
        base.ResetUI();

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                imgSelected[i, j].gameObject.SetActive(false);
            }
        }
    }

    public override void SetAnswers(int[] answers)
    {
        base.SetAnswers(answers);

        this.answers = answers;
        curAnswers = new int[answers.Length];
        this.isAnswer = false;

    }

    public override void OnClickCheckAnswer()
    {
        //base.OnClickCheckAnswer();

        CalcMyAnswer();

        isAnswer = true;
        for (int i = 0; i < answers.Length; i++)
        {
            if (answers[i] != curAnswers[i])
            {
                isAnswer = false;
                break;
            }
        }

        if (isAnswer == false)
            ShakeWrongTargets();
    }

    protected override void CalcMyAnswer()
    {
        base.CalcMyAnswer();

        int tmp = 0;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {                
                int value = 0;
                if (imgSelected[i, j].gameObject.activeSelf)
                    value = 1;
                if (value == 1)
                    tmp += (int)Mathf.Pow(2, i * value);
            }
            curAnswers[i] = tmp;
        }
    }

    public override void ShakeWrongTargets()
    {
        base.ShakeWrongTargets();

        // 정답과 나의답이 맞는 자리를 체크
        for (int i = 0; i < row; i++)
        {
            int tmpAnswer = answers[i];
            for (int j = col - 1; j >= 0; j++)
            {
                int posValue = (int)Mathf.Pow(2, j);
                bool posAnswer = false;
                if (tmpAnswer >= posValue)
                {
                    tmpAnswer -= posValue;
                    posAnswer = true;
                }

                // 정답이 아닌 자리는 쉐이크 
                if (imgSelected[i, j].gameObject.activeSelf != posAnswer)
                {
                    btns[i, j].transform.DOKill();
                    btns[i, j].transform.localPosition = Vector3.zero;
                    btns[i, j].transform.DOShakePosition(1, 15, 100);
                }
            }
        }

    }

    //public void OnClickButton(int idx)
    //{
    //    imgSelected[idx].gameObject.SetActive(!imgSelected[idx].gameObject.activeSelf);
    //}
}
