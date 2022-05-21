using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMiniGame_Base : MonoBehaviour
{
    public GameController_Base gameController;

    public GameObject goGameMain;
    public GameObject goGameDescription;
    public GameObject goInGame;
    public GameObject goClearGame;
    public GameObject goHome;

    // ¹®Á¦ÆÇ
    public GameObject goQuestionBoard;
    public Question_Base[] questions;

    public virtual void InitUI()
    {
        goGameMain.SetActive(true);
        goGameDescription.SetActive(false);
        goInGame.SetActive(false);
        goClearGame.SetActive(false);
        goHome.SetActive(true);
        ResetQuestionBoard();
    }

    public virtual void OnClickGameMain()
    {
        goGameMain.SetActive(false);
        goGameDescription.SetActive(true);
        goInGame.SetActive(false);
        goClearGame.SetActive(false);
        goHome.SetActive(true);

    }

    public virtual void OnClickGameDescription()
    {
        goGameMain.SetActive(false);
        goGameDescription.SetActive(false);
        goInGame.SetActive(true);
        goClearGame.SetActive(false);
        goHome.SetActive(true);

        gameController.StartGame();
    }

    public virtual void ResetQuestionBoard()
    {
        goQuestionBoard.SetActive(false);
        for (int i = 0; i < questions.Length; i++)
        {
            questions[i].gameObject.SetActive(false);
            questions[i].ResetUI();
        }
        
    }

    public virtual void OnClickHome()
    {
        SceneManager.LoadScene("SelectGame");

    }

    public virtual void ClearGame()
    {
        SceneManager.LoadScene("SelectGame");

        //goGameMain.SetActive(false);
        //goGameDescription.SetActive(false);
        //goInGame.SetActive(false);
        //goClearGame.SetActive(true);
        //goHome.SetActive(false);
    }

}
