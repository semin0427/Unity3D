using UnityEngine;
using System.Collections;

public class CutScenePlayer : MonoBehaviour {

    Rigidbody2D D2;
    Transform T;
    public Vector2 move;
    public float CutTime;
    // Use this for initialization
    void Start () {
        D2 = GetComponent<Rigidbody2D>();
        T = GetComponent<Transform>();
        CutTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (T.position.y > 0.5f)
        {
            D2.AddForce(-move, ForceMode2D.Impulse);

        }
        if (T.position.y < 0.5f)
        {
            D2.AddForce(move, ForceMode2D.Impulse);

        }
        CutTime += Time.deltaTime;

        if (CutTime > 8)
            Application.LoadLevel(1);

    }
}
