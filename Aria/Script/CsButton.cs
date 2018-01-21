using UnityEngine;
using System.Collections;

public class CsButton : MonoBehaviour
{
    public int P_1 = 0;
    public int M_1 = 0;

    public bool C_C;
    public bool M_C;
    public GameObject Main;
    public GameObject Cha_Sel;
    public GameObject Map_Sel;
    public GameObject Button_GS;
    public GameObject Button_MR;
    public GameObject Button_Op;
    public GameObject Button_Ex;
    public GameObject Button_Before_Main;           //캐릭터선택창에서 메인으로 돌아가는 버튼
    public GameObject Button_Before_Cha_Sel;        //맵선택창에서 캐릭터 선택창으로 돌아가는 버튼
    public GameObject Character1;                   // 캐릭터 선택 이미지1
    public GameObject Character2;                   // 캐릭터 선택 이미지2
    public GameObject Character3;                   // 캐릭터 선택 이미지3
    public GameObject map1;                         // 맵 선택 이미지1
    public GameObject map2;                         // 맵 선택 이미지2
    public GameObject map3;                         // 맵 선택 이미지3
    public GameObject map2_unlock;                  // 클리어 전 맵2
    public GameObject Button_Front;                 // 민들레 오브젝트 생성
    public Transform Button_Gs_F;                   // 플레이 버튼의 앞 위치 (민들레 오브젝트 생성을 위해)
    public Transform Button_Mr_F;                   // 메모리얼 버튼의 앞 위치                       
    public Transform Button_Op_F;                   // 옵션 버튼의 앞 위치
    public Transform Button_Ex_F;                   // 종료 버튼의 앞 위치
    public Animator Gs_P_L;                         // 플레이 버튼의 라인 생성 위치        
    public Animator Mr_P_L;                         // 메모리얼 버튼의 라인 생성 위치  
    public Animator Op_P_L;                         // 옵션 버튼의 라인 생성 위치  
    public Animator Ex_P_L;                         // 종료 버튼의 라인 생성 위치  


    /// //////////////////////////////////////////////////////////////////////////

    public Animator C1_L;
    public Animator C2_L;

    
    public Animator M1_L;                           // 맵1의 빛 위치
    public Animator M2_L;                           // 맵2의 빛 위치
    // Update is called once per frame
    void Start()
    {
        C_C = false;
        M_C = false;
        P_1 = 0;
        M_1 = 0;
        Button_GS.SetActive(true);
        Button_MR.SetActive(true);
        Button_Op.SetActive(true);
        Button_Ex.SetActive(true);
        Cha_Sel.SetActive(false);
        Map_Sel.SetActive(false);
        map1.SetActive(false);
        map2.SetActive(false);
        map3.SetActive(false);
        map2_unlock.SetActive(false);
        Character1.SetActive(false);
        Character2.SetActive(false);
        Character3.SetActive(false);
        Button_Before_Main.SetActive(false);
        Button_Before_Cha_Sel.SetActive(false);        
    }
    void Update()
    {
        Time.timeScale = 1f;
        if (CsCutScene.ClearS1 == true)
        {
            if (C_C == true)
            {
                Character1.SetActive(false);
                Character2.SetActive(false);
                Character3.SetActive(false);
                map1.SetActive(true);
                map2.SetActive(true);
                map3.SetActive(true);
                map2_unlock.SetActive(false);
                Map_Sel.SetActive(true);
                Button_Before_Cha_Sel.SetActive(true);
                Button_Before_Main.SetActive(false);
            }
            if (C_C == true && M_C == true)
            {
                Button_Before_Cha_Sel.SetActive(false);
                Application.LoadLevel(5);
                C_C = false;
                M_C = false;
            }
        }
        else if (CsCutScene.ClearS1 == false)
        {
            if (C_C == true)
            {
                Character1.SetActive(false);
                Character2.SetActive(false);
                Character3.SetActive(false);
                map1.SetActive(true);
                map2.SetActive(false);
                map3.SetActive(true);
                map2_unlock.SetActive(true);
                Map_Sel.SetActive(true);
                Button_Before_Cha_Sel.SetActive(true);
                Button_Before_Main.SetActive(false);
            }
            if (C_C == true && M_C == true)
            {
                Button_Before_Cha_Sel.SetActive(false);                
                Application.LoadLevel(5);
                StartCoroutine("unlock");
                C_C = false;
                M_C = false;
                
            }
        }
    }
    public void GameStart()
    {
        GetComponent<AudioSource>().Play();
        StartCoroutine(this.Play_Button());
    }

    public void MeMorial()
    {
        StartCoroutine(this.Memorial_Button());
    }
    public void Exit()
    {
        StartCoroutine(this.Exit_Button());
        Application.Quit();
    }
    public void Character_B1()
    {
        GetComponent<AudioSource>().Play();
        //게임화면 씬을 불러옵니다.
        DontDestroyOnLoad(this);
        StartCoroutine(this.Character_Select1());
    }
    public void Character_B2()
    {
        GetComponent<AudioSource>().Play();
        //게임화면 씬을 불러옵니다.   
        DontDestroyOnLoad(this);
        StartCoroutine(this.Character_Select2());
    }
    public void Character_B3()
    {
        //게임화면 씬을 불러옵니다.
        DontDestroyOnLoad(this);
        C_C = true;
        P_1 = 3;
    }
    public void Map1()
    {
        //게임화면 씬을 불러옵니다.
        GetComponent<AudioSource>().Play();
        DontDestroyOnLoad(this);
        StartCoroutine(this.Map_Select1());
    }
    public void Map2()
    {
        //게임화면 씬을 불러옵니다.
        GetComponent<AudioSource>().Play();
        DontDestroyOnLoad(this);
        StartCoroutine(this.Map_Select2());
    }
    public void Map3()
    {
        //게임화면 씬을 불러옵니다.
       
        DontDestroyOnLoad(this);
        M_C = true;
        M_1 = 3;
    }
    public void Befor_Main()
    {
        Button_GS.SetActive(true);
        Button_MR.SetActive(true);
        Button_Op.SetActive(true);
        Button_Ex.SetActive(true);
        Character1.SetActive(false);
        Character2.SetActive(false);
        Character3.SetActive(false);
        map2_unlock.SetActive(false);
        Main.SetActive(true);
        Cha_Sel.SetActive(false);
        Button_Before_Main.SetActive(false);
    }
    public void Befor_Cha_Sel()
    {
        Character1.SetActive(true);
        Character2.SetActive(true);
        Character3.SetActive(true);
        map1.SetActive(false);
        map2.SetActive(false);
        map3.SetActive(false);
        map2_unlock.SetActive(false);
        Cha_Sel.SetActive(true);
        Map_Sel.SetActive(false);
        Button_Before_Cha_Sel.SetActive(false);
        Button_Before_Main.SetActive(true);
        C_C = false;
        P_1 = 0;

    }
    IEnumerator unlock()
    {
        yield return new WaitForSeconds(0.05f);
        map2_unlock.SetActive(false);

    }

    IEnumerator Play_Button()
    {
        Instantiate(Button_Front, Button_Gs_F.position, Quaternion.identity);
        Gs_P_L.SetBool("P_Motion", true);
        yield return new WaitForSeconds(1);
        Gs_P_L.SetBool("P_Motion", false);
        yield return new WaitForSeconds(1);
        Main.SetActive(true);
        Button_GS.SetActive(false);
        Button_MR.SetActive(false);
        Button_Op.SetActive(false);
        Button_Ex.SetActive(false);
        Character1.SetActive(true);
        Character2.SetActive(true);
        Character3.SetActive(true);
        Cha_Sel.SetActive(true);
        Button_Before_Main.SetActive(true);
    }
    IEnumerator Character_Select1()
    {
        C1_L.SetBool("M_Motion", true);
        yield return new WaitForSeconds(1);
        C1_L.SetBool("M_Motion", false);
        yield return new WaitForSeconds(1);
        C_C = true;
        P_1 = 1;
        Cha_Sel.SetActive(false);
    }
    IEnumerator Character_Select2()
    {
        C2_L.SetBool("M_Motion", true);
        yield return new WaitForSeconds(1);
        C2_L.SetBool("M_Motion", false);
        yield return new WaitForSeconds(1);
        
        C_C = true;
        P_1 = 2;
        Cha_Sel.SetActive(false);
    }
    IEnumerator Map_Select1()
    {
        M1_L.SetBool("M_Motion", true);
        yield return new WaitForSeconds(2);
        M1_L.SetBool("M_Motion", false);
        M_C = true;
        M_1 = 1;
        Map_Sel.SetActive(false);

    }
    IEnumerator Map_Select2()
    {
        M2_L.SetBool("M_Motion", true);
        yield return new WaitForSeconds(2);
        M2_L.SetBool("M_Motion", false);
        M_C = true;
        M_1 = 2;
        Map_Sel.SetActive(false);
    }
    IEnumerator Memorial_Button()
    {
        Instantiate(Button_Front, Button_Mr_F.position, Quaternion.identity);
        Mr_P_L.SetBool("P_Motion", true);
        yield return new WaitForSeconds(1);
        Mr_P_L.SetBool("P_Motion", false);
        yield return new WaitForSeconds(1);

    }
    IEnumerator Exit_Button()
    {
        Instantiate(Button_Front, Button_Ex_F.position, Quaternion.identity);
        Ex_P_L.SetBool("P_Motion", true);
        yield return new WaitForSeconds(1);
        Ex_P_L.SetBool("P_Motion", false);
        yield return new WaitForSeconds(1);

    }
}