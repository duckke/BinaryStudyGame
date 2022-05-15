using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMiniGame_Base : MonoBehaviour
{
    public GameController_Base gameController;

    public GameObject goGameMain;
    public GameObject goInGame;

    public virtual void InitUI()
    {
        goGameMain.SetActive(true);
        goInGame.SetActive(false);
    }

    public virtual void OnClickGameMain()
    {
        goGameMain.SetActive(false);
        goInGame.SetActive(true);
        
        gameController.StartGame();
    }
    
    public virtual void OnClickHome()
    {
        SceneManager.LoadScene("SelectGame");

    }

}
