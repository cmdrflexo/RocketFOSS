using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RFOSSCore;

public class SettingsTest : MonoBehaviour {

    public SaveIO mySaveIO = new SaveIO();

	void Start ()
    {
        mySaveIO.LoadSave("Abraxillim");
        print(mySaveIO.CurrentSaveData.name);
    }
}
