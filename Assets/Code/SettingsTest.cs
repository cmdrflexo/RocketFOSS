using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RFOSSCore;

public class SettingsTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RFOSSCore.SettingsIO.WriteDefaultSettings();
	}
}
