using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using RFOSSCore;

public class MMStartButton : MonoBehaviour {

    public InputField TextField;
    public Text ErrorMessage;
    bool showError = false;

    public void startNewSave()
    {
        if(Directory.Exists("Saves/" + TextField.text))
        {
            showError = (true && Camera.main.GetComponent<MainMenuController>().MMenu.State == MainMenu.menuStates.newgame);
        }
        else
        {
            GameManager.Save.CreateNewSave(TextField.text, true);
            GameManager.ChangeScene("space_center");
        }
    }

    private void Update()
    {
        ErrorMessage.enabled = showError;
    }
}
