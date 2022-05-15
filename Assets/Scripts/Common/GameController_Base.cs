using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController_Base : MonoBehaviour
{
    public int score = 0;

    private void Awake()
    {
        InitGame();
    }
    
    public virtual void InitGame()
    {
        // ����. ���� ���۽� �ʱ�ȭ(��ó��)
        score = 0;
    }

    public virtual void ResetGame()
    {
    }

    public virtual void StartGame()
    {
        ResetGame();
        
        // ���� ����

    }
}