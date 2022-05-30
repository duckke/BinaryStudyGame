using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISelectGame : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClickEasy()
    {
        AudioManager.Instance.PlayClickSound();
        SceneManager.LoadScene("MiniGame_Card");

    }

    public void OnClickNormal()
    {
        AudioManager.Instance.PlayClickSound();
        SceneManager.LoadScene("MiniGame_Order");

    }

    public void OnClickHard()
    {
        AudioManager.Instance.PlayClickSound();
        SceneManager.LoadScene("MiniGame_QR");
    }
}
