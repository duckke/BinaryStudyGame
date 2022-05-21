using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Question_Base : MonoBehaviour
{
    protected int answer = 0;
    protected int[] answers;
    protected int curAnswer = 0;
    protected int[] curAnswers;
    public bool isAnswer = false;

    public virtual void ResetUI()
    {
        answer = 0;
        curAnswer = 0;

    }

    public virtual void SetAnswer(int answer)
    {
        this.answer = answer;
        this.curAnswer = 0;
        this.isAnswer = false;
    }

    public virtual void SetAnswers(int[] answers)
    {

    }

    public virtual void SetAnswerMent(string ment)
    {

    }

    public virtual void SetGridCount(int gridCount)
    {

    }

    public virtual void OnClickCheckAnswer()
    {
        CalcMyAnswer();

        if (answer == curAnswer)
        {
            isAnswer = true;
        }

        if (isAnswer == false)
            ShakeWrongTargets();

    }

    protected virtual void CalcMyAnswer()
    {

    }

    public virtual void ShakeWrongTargets()
    {
        
    }
}
