﻿using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    public Camera mainCam;
    float shakeAmount = 0;
    void Awake()
    {
        if (mainCam == null)
            mainCam = Camera.main;
    }
    void Update()
    {
        /*
        if (CsPlayer.C_move == true)
        {
            Shake(0.1f, 0.2f);
            CsPlayer.C_move = false;
        }
        */
        if (CsPlayer.B_Colled == true)
        {
            Shake(0.1f, 0.2f);
            CsPlayer.B_Colled = false;
        }
    }
    public void Shake(float amt, float length)
    {
        shakeAmount = amt;
        InvokeRepeating("DoShake", 0, 0.01f);
        Invoke("StopShake", length);
    }
    void DoShake()
    {
        if (shakeAmount > 0)
        {
            Vector3 camPos = mainCam.transform.position;
            float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;
            camPos.x += offsetX;
            camPos.y += offsetY;

            mainCam.transform.position = camPos;
        }
    }
    void StopShake()
    {
        CancelInvoke("DoShake");
        mainCam.transform.localPosition = Vector3.zero;
    }
}
