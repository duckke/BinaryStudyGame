

using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Question_OrderZeroOne : Question_Base
{
    public GameObject[] goGridObj;
    public Button[] btns;
    public Image[] imgSelected;
    public Text txtAnswer;

    public override void ResetUI()
    {
        base.ResetUI();

        for (int i = 0; i < imgSelected.Length; i++)
        {
            imgSelected[i].gameObject.SetActive(false);
        }

    }

    public override void SetGridCount(int gridCount)
    {
        base.SetGridCount(gridCount);

        for (int i = 0; i < goGridObj.Length; i++)
        {
            if (i < gridCount)
                goGridObj[i].SetActive(true);
            else
                goGridObj[i].SetActive(false);
        }
    }

    public override void SetAnswerMent(string ment)
    {
        base.SetAnswerMent(ment);
        txtAnswer.text = ment;
    }

    protected override void CalcMyAnswer()
    {
        base.CalcMyAnswer();

        int tmp = 0;
        for (int i = 0; i < imgSelected.Length; i++)
        {
            int value = 0;
            if (imgSelected[i].gameObject.activeSelf)
                value = 1;
            if (value == 1)
                tmp += (int)Mathf.Pow(2, i * value);
        }
        
        curAnswer = tmp;
    }
    
    public override void ShakeWrongTargets()
    {
        base.ShakeWrongTargets();

        // 정답과 나의답이 맞는 자리를 체크
        int tmpAnswer = answer;
        for (int i = btns.Length - 1; i >= 0; i--)
        {
            int posValue = (int)Mathf.Pow(2, i);
            bool posAnswer = false;
            if (tmpAnswer >= posValue)
            {
                tmpAnswer -= posValue;
                posAnswer = true;
            }
            
            // 정답이 아닌 자리는 쉐이크 
            if (imgSelected[i].gameObject.activeSelf != posAnswer)
            {
                btns[i].transform.DOKill();
                btns[i].transform.localPosition = Vector3.zero;
                btns[i].transform.DOShakePosition(1, 15, 100);
            }
        }
    }

    public void OnClickButton(int idx)
    {
        AudioManager.Instance.PlayClickSound();
        imgSelected[idx].gameObject.SetActive(!imgSelected[idx].gameObject.activeSelf);
    }
}
