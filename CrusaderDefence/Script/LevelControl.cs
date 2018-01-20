using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelData
{
    public float end_time;
    public float regen_term;
    public float play_time;
    public float rest_time;

    public LevelData()
    {
        end_time = 20.0f;
        regen_term = 10.0f;
        play_time = 40.0f;
        rest_time = 10.0f;
    }
}

public class LevelControl : MonoBehaviour
{
    private List<LevelData> level_datas = new List<LevelData>();

    public int level = 0; // 난이도.

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void initialize()
    {
    }

    private void update_level(float passage_time)
    {
        float local_time = Mathf.Repeat(passage_time, level_datas[level_datas.Count - 1].end_time);
        // 현재 레벨을 구한다.
        int i;
        for (i = 0; i < level_datas.Count - 1; i++)
        {
            if (local_time <= level_datas[i].end_time)
            {
                
                break;
            }
        }

        level = i - 1;
        Debug.Log("genTerm : " + level_datas[i].regen_term);

        Debug.Log("currnetLevel : " + level);
        // 현재 레벨용 레벨 데이터를 가져온다.
        LevelData level_data;
        level_data = this.level_datas[level];
    }

    public float regenTime(int _level)
    {
        return level_datas[_level].regen_term;
    }

    public float playTime(int _level)
    {
        return level_datas[_level].play_time;
    }

    public float restTime(int _level)
    {
        return level_datas[_level].rest_time;
    }

    public void update(float passage_time)
    {
        update_level(passage_time);
    }

    public void loadLevelData(TextAsset level_data_text)
    {
        string level_texts = level_data_text.text; // 텍스트 데이터를 문자열로 가져온다.
        string[] lines = level_texts.Split('\n'); // 개행 코드 '\'마다 분할해서 문자열 배열에 넣는다.
                                                  // lines 내의 각 행에 대해서 차례로 처리해 가는 루프.
        foreach (var line in lines)
        {
            if (line == "")
            { // 행이 빈 줄이면.
                continue; // 아래 처리는 하지 않고 반복문의 처음으로 점프한다.
            };
            Debug.Log(line); // 행의 내용을 디버그 출력한다.
            string[] words = line.Split(); // 행 내의 워드를 배열에 저장한다.
            int n = 0;
            // LevelData형 변수를 생성한다.
            // 현재 처리하는 행의 데이터를 넣어 간다.
            LevelData level_data = new LevelData();
            // words내의 각 워드에 대해서 순서대로 처리해 가는 루프.
            foreach (var word in words)
            {
                if (word.StartsWith("#"))
                { // 워드의 시작문자가 #이면.
                    break;
                } // 루프 탈출.
                if (word == "")
                { // 워드가 텅 비었으면.
                    continue;
                }
                switch (n)
                {
                    case 0:
                        level_data.end_time = float.Parse(word);
                        break;
                    case 1:
                        level_data.regen_term = float.Parse(word);
                        break;
                    case 2:
                        level_data.play_time = float.Parse(word);
                        break;
                    case 3:
                        level_data.rest_time = float.Parse(word);
                        break;
                }
                n++;
            }
            if (n >= 4)
            { // 8항목(이상)이 제대로 처리되었다면.
                level_datas.Add(level_data); // List 구조의 level_datas에 level_data를 추가한다.
            }
            else
            { // 그렇지 않다면(오류의 가능성이 있다).
                if (n == 0)
                { // 1워드도 처리하지 않은 경우는 주석이므로.
                  // 문제없다. 아무것도 하지 않는다.
                }
                else
                { // 그 이외이면 오류다.
                  // 데이터 개수가 맞지 않다는 것을 보여주는 오류 메시지를 표시한다.
                    Debug.LogError("[LevelData] Out of parameter.\n");
                }
            }
        }
        if (level_datas.Count == 0)
        { // level_datas에 데이터가 하나도 없으면.
            Debug.LogError("[LevelData] Has no data.\n"); // 오류 메시지를 표시한다.
            level_datas.Add(new LevelData()); // level_datas에 기본 LevelData를 하나 추가해 둔다.
        }
    }
}
