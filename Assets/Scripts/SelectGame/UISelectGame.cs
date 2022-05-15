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
        SceneManager.LoadScene("MiniGame_Card");

    }

    public void OnClickNormal()
    {
        SceneManager.LoadScene("MiniGame_Order");

    }

    public void OnClickHard()
    {
        SceneManager.LoadScene("MiniGame_QR");
    }
}
