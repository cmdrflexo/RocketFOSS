using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RFOSSCore;
using UnityEngine.UI;
using System;

public class SpaceCenterUI : MonoBehaviour {

    public Text timecodeText;

    private void Start()
    {
        GameManager.isPaused = false;
    }

    public void Update()
    {
        GameManager.UpdateTimecode(Time.deltaTime);
        timecodeText.text = ConvertTimeCode.ToString(GameManager.timecode);
    }
}
