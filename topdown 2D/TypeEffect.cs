using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypeEffect : MonoBehaviour
{
    public int CharPerSeconds;
    public bool isAnimation;
    public GameObject EndCursor;
    string targetMsg;
    AudioSource audioSource;
    TextMeshProUGUI msgText;
    int index;
    float interval;

    private void Awake()
    {
        msgText = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetMsg(string msg)
    {
        if(isAnimation){
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else{
            targetMsg = msg;
            EffectStart();
        }
    }

    void EffectStart()
    {
        EndCursor.SetActive(false);
        msgText.text = "";
        index = 0;

        //Start Animation
        isAnimation = true;
        interval = 1.0f / CharPerSeconds;
        Invoke("Effecting", interval);
    }

    void Effecting()
    {
        if(msgText.text == targetMsg){
            EffectEnd();
            return;
        }
        msgText.text += targetMsg[index];
        //Sound
        if(targetMsg[index]!=' ' || targetMsg[index] != '.')
            audioSource.Play();

        index++;

        //Recursive
        Invoke("Effecting",interval);
    }

    void EffectEnd()
    {
        isAnimation = false; 
        EndCursor.SetActive(true);
    }
}
