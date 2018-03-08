using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RFOSSCore;

public class SaveGameListObject : MonoBehaviour {

    public string name;

    private void Start()
    {
        Text buttonText = transform.FindChild("Text").GetComponent<Text>();
        buttonText.text = name;
    }

    public void LoadGame()
    {
        GameManager.Save.LoadSave(name);
        GameManager.ChangeScene("space_center");
    }
}
