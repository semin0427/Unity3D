using UnityEngine;
using System.Collections;

public class CsTitleFader : MonoBehaviour {

    public float fadeSpeed = 1.0f;

    // Use this for initialization
    void Start () {
        GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
    }
	
	// Update is called once per frame
	void Update () {

        FadeToClear();
    }

    void FadeToClear()
    {
        // Lerp the colour of the texture between itself and transparent.
        GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.clear, fadeSpeed * Time.deltaTime);
    }
}
