using UnityEngine;
using System.Collections;

public class CsEnemyBasecamp : AtkUnit {

    PlayManager PM;
    // Use this for initialization
    void Start() {
        SetStatus(100, 0, 0, 0, 0, 0, 0, 0);
        PM = GameObject.Find("Castle").GetComponent<PlayManager>();
        gameObject.tag = "OffenseDef";
    }

    // Update is called once per frame
    void Update() {
    }

    void LateUpdate()
    {
        if (hp <= 0)
        {
            hp = 0;
            Destroy(gameObject);
        }
    }
}
