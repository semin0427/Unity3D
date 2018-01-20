using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageManager : MonoBehaviour {

    public GameObject questMessage;
    public GameObject npcSword;
    public GameObject npcBow;
    public GameObject npcHorse;
    
    public GameObject CatapultBtn;

    public GameObject AtkBtn;
    public GameObject DefBtn;

    public GameObject ScreenCover;

    GameObject npcDialogue;
    GameObject npcDialogue2;
    GameObject npcDialogue3;
    GameObject questDialogue;

    GameObject castlePos;
    GameObject unitPos;
    EnemyCreater EC;

    int stageNum;
    float dialogueTerm;
    GameObject _camera;
    public int tutorialNum;

    // Use this for initialization
	void Start () {
        stageNum = Application.loadedLevel;
        tutorialNum = 0;
        EC = GameObject.Find("UnitCreater").GetComponent<EnemyCreater>();
        SoundManager.instance.myAudios.Stop();
        SoundManager.instance.SoundPlay(SoundManager.instance.MainBgm);
        _camera = GameObject.Find("CameraPivot");
        switch (stageNum)
        {
            case 2: //기습
                InitStage1();
                break;
            case 3: //
                InitStage2();
                break;
            case 4: //
                InitStage3();
                break;
        }
    }
	
	// Update is called once per frame
	void LateUpdate () {

        switch (stageNum)
        {
            case 2: //기습
                Stage1();
                break;
            case 3: //
                Stage2();
                break;
            case 4: //
                Stage3();
                break;
        }
    }
    
    void InitStage1()
    {
        npcDialogue = Instantiate<GameObject>(npcSword);
        questDialogue = Instantiate<GameObject>(questMessage);

        unitPos = GameObject.Find("unitCreatePos");
        unitPos.SetActive(false);
        castlePos = GameObject.Find("castlePos");
        castlePos.SetActive(false);

        questDialogue.SetActive(false);
    }

    void InitStage2()
    {
        npcDialogue = Instantiate<GameObject>(npcHorse);
        questDialogue = Instantiate<GameObject>(questMessage);
        questDialogue.SetActive(false);
        CatapultBtn.SetActive(false);
        AtkBtn.SetActive(false);
        DefBtn.SetActive(false);
    }

    void InitStage3()
    {
        npcDialogue = Instantiate<GameObject>(npcHorse);
        npcDialogue2 = Instantiate<GameObject>(npcBow);
        npcDialogue3 = Instantiate<GameObject>(npcSword);
        questDialogue = Instantiate<GameObject>(questMessage);
        questDialogue.SetActive(false);
    }

    void Stage1()
    {
        dialogueTerm += Time.deltaTime;
        if (tutorialNum == 0 || tutorialNum == 1 || tutorialNum == 7 
            || tutorialNum == 9 || tutorialNum == 13 || tutorialNum == 14 
            || tutorialNum == 16 || tutorialNum == 17)
        {
            //if (dialogueTerm > 2.0f)
            //{
            //    tutorialNum += 1;
            //    dialogueTerm = 0;
            //}
            if (Input.GetKeyDown(KeyCode.Space))
                tutorialNum += 1;
        }
        else
            dialogueTerm = 0;

        switch (tutorialNum)
        {
            case 0:
                stage1Npc(0);
                break;
            case 1:
                stage1Npc(1);
                break;
            case 2:
                questDialogue.SetActive(true);
                stage1Quest(0);
                npcDialogue.SetActive(false);
                castlePos.SetActive(true);
                castlePos.transform.position = GameObject.Find("Castle").transform.position + new Vector3(0, 0.1f, 0);
                ScreenCover.SetActive(false);
                break;
            case 3:
                castlePos.SetActive(false);
                stage1Quest(1);
                break;
            case 4:
                unitPos.SetActive(true);
                stage1Quest(2);
                break;
            case 5:
                questDialogue.SetActive(false);
                unitPos.SetActive(false);
                ScreenCover.SetActive(true);
                break;
            case 6:
                unitPos.SetActive(false);
                ScreenCover.SetActive(true);
                npcDialogue.SetActive(true);
                stage1Npc(2);
                break;
            case 7:
                stage1Npc(3);
                break;
            case 8:
                npcDialogue.SetActive(false);
                questDialogue.SetActive(true);
                stage1Quest(3);
                ScreenCover.SetActive(false);
                break;
            case 9:
                npcDialogue.SetActive(true);
                stage1Npc(4);
                questDialogue.SetActive(false);
                break;
            case 10:
                npcDialogue.SetActive(false);
                questDialogue.SetActive(true);
                stage1Quest(4);
                break;
            case 11:
                break;
            case 12:
                stage1Quest(5);
                break;
            case 13:
                questDialogue.SetActive(false);
                npcDialogue.SetActive(true);
                stage1Npc(5);
                break;
            case 14:
                stage1Npc(6);
                break;
            case 15:
                EC.SendMessage("createEnemy01", 5.0f);
                npcDialogue.SetActive(false);
                questDialogue.SetActive(true);
                stage1Quest(6);
                break;
            case 16:
                npcDialogue.SetActive(true);
                questDialogue.SetActive(false);
                stage1Npc(7);
                break;
            case 17:
                questDialogue.SetActive(true);
                npcDialogue.SetActive(false);
                stage1Quest(7);
                break;
            case 18:
                questDialogue.SetActive(false);
                npcDialogue.SetActive(false);
                Application.LoadLevel(5);
                break;
        }
    }

    void Stage2()
    {
        if (tutorialNum == 0 || tutorialNum == 1 || tutorialNum == 2
            || tutorialNum == 3 || tutorialNum == 5 || tutorialNum == 7 || tutorialNum == 9)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                tutorialNum += 1;
            //if (dialogueTerm > 2.0f)
            //{
            //    tutorialNum += 1;
            //    dialogueTerm = 0;
            //}
        }

        if (tutorialNum == 8)
            dialogueTerm += Time.deltaTime;
        else
            dialogueTerm = 0;

        switch (tutorialNum)
        {
            case 0:
                stage2Npc(0);
                break;
            case 1:
                EC.SendMessage("createEnemy03");
                EC.SendMessage("stage2tutorial1");
                stage2Npc(1);
                break;
            case 2:
                stage2Npc(2);
                break;
            case 3:
                stage2Npc(3);
                break;
            case 4:
                EC.SendMessage("createEnemy02", 7.0f);
                npcDialogue.SetActive(false);
                questDialogue.SetActive(true);
                stage2Quest(0);
                break;
            case 5:
                npcDialogue.SetActive(true);
                questDialogue.SetActive(false);
                stage2Npc(4);
                break;
            case 6:
                npcDialogue.SetActive(false);
                questDialogue.SetActive(true);
                stage2Quest(1);
                break;
            case 7:
                npcDialogue.SetActive(true);
                questDialogue.SetActive(false);
                stage2Npc(5);
                break;
            case 8:
                npcDialogue.SetActive(false);
                questDialogue.SetActive(true);
                stage2Quest(2);
                CatapultBtn.SetActive(true);
                if (dialogueTerm > 2.0f)
                    tutorialNum = 9;
                break;
            case 9:
                npcDialogue.SetActive(true);
                questDialogue.SetActive(false);
                stage2Npc(6);
                _camera.transform.position = new Vector3(-8.4f, 0, 67);
                _camera.GetComponentInChildren<Transform>().rotation = Quaternion.identity;
                //카메라 이동
                EC.SendMessage("stage2tutorial9");
                break;
            case 10:
                npcDialogue.SetActive(false);
                questDialogue.SetActive(true);
                AtkBtn.SetActive(true);
                DefBtn.SetActive(true);
                stage2Quest(3);
                _camera.transform.position = new Vector3(-12.18f, 0, 7.4f);
                break;
            case 11:
                GameObject.Find("AtkCircle").GetComponent<Image>().enabled = false;
                stage2Quest(4);
                break;
        }
    }

    void Stage3()
    {
        if (tutorialNum == 0 || tutorialNum == 1 || tutorialNum == 2)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                tutorialNum += 1;
        }

        switch (tutorialNum)
        {
            case 0:
                npcDialogue.SetActive(false);
                npcDialogue2.SetActive(false);
                stage3Npc(0);
                break;
            case 1:
                npcDialogue2.SetActive(true);
                npcDialogue3.SetActive(false);
                stage3Npc(1);
                break;
            case 2:
                npcDialogue.SetActive(true);
                npcDialogue2.SetActive(false);
                stage3Npc(2);
                break;
            case 3:
                npcDialogue.SetActive(false);
                questDialogue.SetActive(true);
                stage3Quest(0);
                ScreenCover.SetActive(false);
                break;
            case 4:
                questDialogue.SetActive(false);
                break;
        }
    }

    //stage1
    void stage1Npc(int _dialogueNum)
    {
        Text dialogue = npcDialogue.GetComponentInChildren<Text>();

        switch(_dialogueNum)
        {
            case 0:
                dialogue.text = "칼 대위\n\n오셨군요! 다들 기다리고 있었답니다."
                    + "\n\n                                                                                                                                      Space Bar▷▶";
                break;
            case 1:
                dialogue.text = "칼 대위\n\n앗! 겁도없이 적이 쳐들어 왔군요.. 대장님! 명령을..!!"
                    + "\n\n                                                                                                                                      Space Bar▷▶";
                break;
            case 2:
                dialogue.text = "칼 대위\n\n역시 대장님이십니다.";
                break;
            case 3:
                dialogue.text = "칼 대위\n\n이런, 기습입니다..!"
                    + "\n\n                                                                                                                                      Space Bar▷▶";
                break;
            case 4:
                dialogue.text = "칼 대위\n\n기습에 당해버렸군요. 수리가 필요할 것 같습니다."
                    + "\n\n                                                                                                                                      Space Bar▷▶";
                break;
            case 5:
                dialogue.text = "칼 대위\n\n역시 훌륭하십니다."
                    + "\n\n                                                                                                                                      Space Bar▷▶";
                break;
            case 6:
                dialogue.text = "칼 대위\n\n흠.. 끈질긴 녀석들이군요.."
                    + "\n\n                                                                                                                                      Space Bar▷▶";
                break;
            case 7:
                dialogue.text = "칼 대위\n\n대단하십니다! 대장님과 함께라면 승리는 따놓은 당상입니다!"
                    + "\n\n                                                                                                                                      Space Bar▷▶";
                break;
        }
    }

    void stage1Quest(int _questNum)
    {
        Text quest = questDialogue.GetComponentInChildren<Text>();
        switch (_questNum)
        {
            //stage1///////////
            case 0:
                quest.text = "조각상을 눌러 UI를 활성화 하십시오.";
                break;
            case 1:
                quest.text = "배치 할 유닛을 선택하십시오.";
                break;
            case 2:
                quest.text = "배치 할 위치를 선택하십시오.";
                break;
            case 3:
                quest.text = "적을 제거하십시오.";
                break;
            case 4:
                quest.text = "일꾼을 배치하여 목재를 채집하십시오.";
                break;
            case 5:
                quest.text = "목재를 사용하여 조각상을 수리하십시오.";
                break;
            case 6:
                quest.text = "적들을 모두  섬멸 하십시오.";
                break;
            ///////////////////
            case 7:
                quest.text = "SpaceBar를 누르시면 결과화면으로 이동합니다.";
                break;

        }
    }

    //stage2
    void stage2Npc(int _dialogueNum)
    {
        Text dialogue = npcDialogue.GetComponentInChildren<Text>();
        
        switch (_dialogueNum)
        {
            case 0: dialogue.text = "돈키호테 중령\n\n후퇴하라!"
                    + "\n\n                                                                                                                                      Space Bar▷▶";
                break;
            case 1:
                dialogue.text = "돈키호테 중령\n\n헉헉.. 오셨습니까 대장님..!"
                    + "\n\n                                                                                                                                      Space Bar▷▶";
                break;
            case 2:
                dialogue.text = "돈키호테 중령\n\n생각보다 적들의 저항이 더욱 거샙니다."
                    + "\n\n                                                                                                                                      Space Bar▷▶";
                break;
            case 3:
                dialogue.text = "돈키호테 중령\n\n또 몰려오는군요.."
                    + "\n\n                                                                                                                                      Space Bar▷▶";
                break;
            case 4:
                dialogue.text = "돈키호테 중령\n\n후우.. 투석기를 이용 할 수 있다면 좋으련만.."
                    + "\n\n                                                                                                                                      Space Bar▷▶";
                break;
            case 5:
                dialogue.text = "돈키호테 중령\n\n이것으로 이제 반격이 가능하겠군요!"
                    + "\n\n                                                                                                                                      Space Bar▷▶";
                break;
            case 6:
                dialogue.text = "돈키호테 중령\n\n대장님! 적들의 숙영지를 발견했습니다.\n공격명령을!!"
                    + "\n                                                                                                                                      Space Bar▷▶";
                break;
            case 7:
                dialogue.text = "돈키호테 중령\n\n대승입니다!\n지겨운 전쟁도 이제 곧 끝이겠군요.."
                    + "\n                                                                                                                                      Space Bar▷▶";
                break;
        }
    }

    void stage2Quest(int _questNum)
    {
        Text quest = questDialogue.GetComponentInChildren<Text>();
        switch (_questNum)
        {
            case 0: quest.text = "적들의 공격을 막아라.";
                break;
            case 1:
                quest.text = "일꾼으로 부숴진 투석기를 회수하라.";
                break;
            case 2:
                quest.text = "지금부터 투석기 생산이 가능합니다.";
                break;
            case 3:
                quest.text = "공격버튼을 눌러 공격모드로 전환하라.";
                break;
            case 4:
                quest.text = "표시된 영역에 유닛생산이 가능합니다.";
                break;
            case 5:
                quest.text = "";
                break;
        }
    }

    // stage3
    void stage3Npc(int _dialogueNum)
    {
        Text dialogue = npcDialogue.GetComponentInChildren<Text>();
        Text dialogue2 = npcDialogue2.GetComponentInChildren<Text>();
        Text dialogue3 = npcDialogue3.GetComponentInChildren<Text>();

        switch(_dialogueNum)
        {
            case 0:
                dialogue3.text = "칼 대위\n\n공격하라!"
                    + "\n\n                                                                                                                                      Space Bar▷▶";
                break;
            case 1:
                dialogue2.text = "보우 하사\n\n원군이 도착했습니다!"
                    + "\n\n                                                                                                                                      Space Bar▷▶";
                break;
            case 2:
                dialogue.text = "돈키호테 중령\n\n고생이 많았군.. \n전군! 진격하라!!"
                    + "\n                                                                                                                                      Space Bar▷▶";
                break;
        }
    }

    void stage3Quest(int _questNum)
    {
        Text quest = questDialogue.GetComponentInChildren<Text>();
        switch (_questNum)
        {
            case 0:
                quest.text = "공격을 막으며 기지를 점령하라.";
                break;
        }
    }
}