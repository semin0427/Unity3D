using UnityEngine;
using System.Collections;
using System.Collections.Generic; // List 데이터 구조를 사용하기 위해 필요

public class LevelData
{
    public float dayTime; // 오전오후 결정
    public float nightTime;
    public float vanishIronTime; // 아이템 소멸시간 결정
    public float vanishAppleTime;
    public float vanishPlantTime;
    public float vanishNutTime;

    public LevelData() // 생성자. 
    {
        //dayTime[0] = 0;
        //dayTime[1] = 0;
        //vanishTime[0] = 0;
        //vanishTime[1] = 0;
        //vanishTime[2] = 0;
        //vanishTime[3] = 0;
    }
}

// LevelControl.cs: LevelControl class
public class LevelControl
{
    private List<LevelData> level_datas = null; // 각 레벨의 레벨 데이터.
    public int select_level = 0; // 선택된 레벨.
    public void initialize()
    {
        // List를 초기화.
        this.level_datas = new List<LevelData>();
    }

    public void update_level()
    {
        // 현재 레벨을 구한다.

        Debug.Log("currnetLevel : " + select_level);
        // 현재 레벨용 레벨 데이터를 가져온다.
        LevelData level_data;
        level_data = this.level_datas[select_level];
    }

    public void loadLevelData(TextAsset level_data_text)
    {
        // 텍스트 데이터를 문자열로서 받아들인다.
        string level_texts = level_data_text.text;
        // 개행 코드'\'마다 나누어, 문자열 배열에 집어넣는다.
        string[] lines = level_texts.Split('\n');
        // lines 안의 각 행에 대하여 차례로 처리해가는 루프.
        foreach (var line in lines)
        {
            if (line == "")
            { // 행이 비었으면.
                continue; // 아래 처리는 하지 않고 루프의 처음으로 점프.
            }
            string[] words = line.Split(); // 행 내의 워드를 배열에 저장.
            int n = 0;
            // LevelData형 변수를 작성.
            // 여기에 현재 처리하는 행의 데이터를 넣는다.
            LevelData level_data = new LevelData();
            // words내의 각 워드에 대해서, 순서대로 처리해 가는 루프.
            foreach (var word in words)
            {
                if (word.StartsWith("#"))
                { // 워드의 시작 문자가 #이면.
                    break; // 루프 탈출.
                }
                if (word == "")
                { // 워드가 비었으면.
                    continue; // 루프 시작으로 점프.
                }
                // 'n'의 값을 0,1,2,...6으로 변화시켜감으로써 일곱 개 항목을 처리.
                // 각 워드를 float값으로 변환하고 level_data에 저장.
                switch (n)
                {
                    case 0:
                        level_data.dayTime = float.Parse(word); // 낮
                        break;
                    case 1:
                        level_data.nightTime = float.Parse(word); // 밤
                        break;
                    case 2:
                        level_data.vanishIronTime = float.Parse(word); // 철사
                        break;
                    case 3:
                        level_data.vanishAppleTime = float.Parse(word); // 사사
                        break;
                    case 4:
                        level_data.vanishPlantTime = float.Parse(word); // 나사
                        break;
                    case 5:
                        level_data.vanishNutTime = float.Parse(word); // 밤사
                        break;
                }
                n++;
            }
            if (n >= 6)
            { // 8항목(이상)이 제대로 처리되었다면.
              // 출현 확률의 합계가 정확히 100%가 되도록 하고 나서.
         //       level_data.normalize();
                // List 구조의 level_datas에 level_data를 추가한다.
                this.level_datas.Add(level_data);
            }
            else
            { // 그렇지 않으면(오류 가능성이 있다).
                if (n == 0)
                { // 1워드도 처리하지 않은 경우는 주석이므로.
                  // 문제 없음. 아무것도 하지 않는다.
                }
                else
                { // 그 이외라면 오류.
                  // 데이터의 개수가 맞지 않는다는 오류 메시지를 표시.
                    Debug.LogError("[LevelData] Out of parameter.\n");
                }
            }
        }
        // level_datas에 데이터가 하나도 없으면.
        if (this.level_datas.Count == 0)
        {
            // 오류 메시지를 표시.
            Debug.LogError("[LevelData] Has no data.\n");
            // level_datas에 LevelData를 하나 추가해 둔다.
            this.level_datas.Add(new LevelData());
        }
    }
    public void selectLevel()
    {
        update_level();
        Debug.Log("select level = " + this.select_level.ToString());
    }
    public LevelData getCurrentLevelData()
    {
        // 선택된 패턴의 레벨 데이터를 반환한다.
        return (this.level_datas[this.select_level]);
    }
    public float getIronVanishTime()
    {
        // 선택된 패턴의 연소시간을 반환한다.
        return (this.level_datas[this.select_level].vanishIronTime);
    }
    public float getAppleVanishTime()
    {
        // 선택된 패턴의 연소시간을 반환한다.
        return (this.level_datas[this.select_level].vanishAppleTime);
    }
    public float getPlantVanishTime()
    {
        // 선택된 패턴의 연소시간을 반환한다.
        return (this.level_datas[this.select_level].vanishPlantTime);
    }
    public float getNutVanishTime()
    {
        // 선택된 패턴의 연소시간을 반환한다.
        return (this.level_datas[this.select_level].vanishNutTime);
    }

    public float getDaytime()
    {
        // 선택된 패턴의 연소시간을 반환한다.
        return (this.level_datas[this.select_level].dayTime);
    }
    public float getNighttime()
    {
        // 선택된 패턴의 연소시간을 반환한다.
        return (this.level_datas[this.select_level].nightTime);
    }
}