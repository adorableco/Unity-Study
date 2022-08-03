using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public QuestManager questManager;
    public Image portraitImg;
    public Animator portraitAnim;
    public Sprite prevPortrait;
    public TalkManager talkManager;
    public Animator talkPanel;
    public TypeEffect talk;
    public GameObject scanObject;
    public GameObject menuSet;
    public GameObject player;
    public bool isAction;
    public int talkIndex;
    public TextMeshProUGUI charNameText;
    public TextMeshProUGUI questText;
    public ObjData obj;

    private void Start()
    {
        if(!PlayerPrefs.HasKey("PlayerX"))
            return;
        GameLoad();
        questText.text = questManager.CheckQuest();
    }

    void Update()
    {
        //Submenu
        if(Input.GetButtonDown("Cancel"))
        {
            if(menuSet.activeSelf)
            menuSet.SetActive(false);
            else
            menuSet.SetActive(true);
        }
    }

    // Update is called once per frame
    public void Action(GameObject scanObj)
    {
        isAction = true;
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id,objData.isNpc);
        talkPanel.SetBool("isShow",isAction);
    }

    public void Talk(int id, bool isNpc)
    {
        int questTalkIndex=0;
        string talkData ="";
         //Set Talk Data
        if(talk.isAnimation){
            talk.SetMsg("");
            return;
        }
        else{
            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData  = talkManager.GetTalk(id+questTalkIndex,talkIndex);
        }

        //End Talk
        if(talkData == null){
            isAction = false;
            talkIndex  =0;
            questText.text = questManager.CheckQuest(id);
            return;
        }

        if(isNpc){
            //Char Name Text
            if(id==1000)
                charNameText.text = "루도";
            else if(id==2000)
                charNameText.text = "루나";
            talk.SetMsg(talkData.Split(':')[0]);

            //Show Portrait
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1,1,1,1);
            //Animation Potrait
            if(prevPortrait != portraitImg.sprite){
                portraitAnim.SetTrigger("doEffect");
                prevPortrait = portraitImg.sprite;
            }
        }
        else{
            charNameText.text = "루나";
            talk.SetMsg(talkData);
            portraitImg.color = new Color(1,1,1,0);
        }
        isAction = true;
        talkIndex++;
    }


    public void GameSave()
    {
        PlayerPrefs.SetFloat("PlayerX",player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY",player.transform.position.y);
        PlayerPrefs.SetInt("QustId",questManager.questId);
        PlayerPrefs.SetInt("QustActionIndex",questManager.questActionIndex);
        PlayerPrefs.Save();

        menuSet.SetActive(false);

    }

    public void GameLoad()
    {
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int questId = PlayerPrefs.GetInt("QustId");
        int questActionIndex = PlayerPrefs.GetInt("QustActionIndex");

        player.transform.position = new Vector3(x,y,0);
        questManager.questId = questId;
        questManager.questActionIndex = questActionIndex;
        questManager.ControlObject();
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void GameReset()
    {
        float x = 0;
        float y = 0;
        int questId = 0;
        int questActionIndex = 0;
        player.transform.position = new Vector3(x,y,0);
        questManager.questId = questId;
        questManager.questActionIndex = questActionIndex;
        questManager.ControlObject();
    }

}
