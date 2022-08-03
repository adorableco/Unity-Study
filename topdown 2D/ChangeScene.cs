using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public GameManager gm;

    void Awake()
    {
        gm = GetComponent<GameManager>();
    }

    public void ChangeScenebtn()
    {
        if(this.gameObject.name=="Continue")
            gm.GameReset();
        
        SceneManager.LoadScene("SampleScene");
    }
}
