using UnityEngine;
using System.Collections;

public class CsClearFade : MonoBehaviour {

    public float fadeSpeed = 1.0f;
    public float SceneTime = 0;
    // Use this for initialization
    void Start()
    {
        GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        StartScene();
        SceneTime += Time.deltaTime;
        if (SceneTime > 5)
            FadeToBlack();
    }

    void FadeToClear()
    {
        // Lerp the colour of the texture between itself and transparent.
        GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    void StartScene()
    {
        // Fade the texture to clear.
        FadeToClear();

        // If the texture is almost clear...
        if (GetComponent<GUITexture>().color.a <= 0.05f)
        {
            // ... set the colour to clear and disable the GUITexture.
            GetComponent<GUITexture>().color = Color.clear;
            GetComponent<GUITexture>().enabled = false;
            // The scene is no longer starting.
            //    sceneStarting = false;
        }
    }

    void FadeToBlack()
    {
  //      yield return new WaitForSeconds(5);
        // Lerp the colour of the texture between itself and black.
        GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.black, fadeSpeed * Time.deltaTime);
    }

    public void EndScene()
    {
        //      yield return new WaitForSeconds(term);
        // Make sure the texture is enabled.
        GetComponent<GUITexture>().enabled = true;

        // Start fading towards black.
        FadeToBlack();

        // If the screen is almost black...
        if (GetComponent<GUITexture>().color.a >= 0.95f)
        {
            // ... reload the level.
            GetComponent<GUITexture>().color = Color.clear;
            GetComponent<GUITexture>().enabled = true;
        }
        Application.LoadLevel(1);
    }
}
