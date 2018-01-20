using UnityEngine;
using System.Collections;

public class AtkRange : MonoBehaviour {

    public Renderer[] presets;
    public Renderer blindRenderer;

	// Use this for initialization
	void Start () {
        foreach(var preset in presets)
            preset.material.color = new Color(1, 1, 1, 0.5f);

        if(blindRenderer != null)
            blindRenderer.material.color = new Color(1, 1, 0, 0.3f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
