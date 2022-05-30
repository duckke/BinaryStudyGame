using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController_Base : MonoBehaviour
{
    public AudioManager audioManager;
    public int score = 0;

    private void Awake()
    {
        InitGame();
    }
    
    public virtual void InitGame()
    {
        // 공통. 게임 시작시 초기화(맨처음)
        score = 0;
    }

    public virtual void ResetGame()
    {
    }

    public virtual void StartGame()
    {
        ResetGame();
        
        // 게임 시작

    }

    public virtual void ClearGame()
    {
        // 게임 끝

    }
}
