

using TMPro;
using UnityEngine;
using DG.Tweening;

public class Question_CardPlusMinus : Question_Base
{
    public TextMeshProUGUI[] txtValue;

    public override void ResetUI()
    {
        base.ResetUI();

        for (int i = 0; i < txtValue.Length; i++)
        {
            txtValue[i].text = "0";
        }

    }

    protected override void CalcMyAnswer()
    {
        base.CalcMyAnswer();

        int tmp = 0;
        for (int i = 0; i < txtValue.Length; i++)
        {
            int value = int.Parse(txtValue[i].text);
            tmp += value;
        }
        
        curAnswer = tmp;
    }
    
    public override void ShakeWrongTargets()
    {
        base.ShakeWrongTargets();

        // 정답과 나의답이 맞는 자리를 체크
        int tmpAnswer = answer;
        for (int i = txtValue.Length - 1; i >= 0; i--)
        {
            int posValue = (int)Mathf.Pow(2, i);
            int posAnswer = 0;
            if (tmpAnswer >= posValue)
            {
                tmpAnswer -= posValue;
                posAnswer = posValue;
            }
            
            // 정답이 아닌 자리는 쉐이크 
            if (int.Parse(txtValue[i].text) != posAnswer)
            {
                txtValue[i].transform.DOKill();
                txtValue[i].transform.localPosition = Vector3.zero;
                txtValue[i].transform.DOShakePosition(1, 15, 100);
            }
        }
    }

    public void OnClickPlus(int idx)
    {
        txtValue[idx].text = ((int)Mathf.Pow(2, idx)).ToString();

    }

    public void OnClickMinus(int idx)
    {
        txtValue[idx].text = "0";

    }
}
