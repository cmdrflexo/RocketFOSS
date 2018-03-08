using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RFOSSCore;
using UnityEngine.UI;
using System;

public class MainMenuController : MonoBehaviour {

    public MainMenu MMenu = new MainMenu();
    public Canvas menuCanvas;
    public GameObject PanelMainMenu;
    public GameObject PanelStartMenu;
    public GameObject PanelSettingsMenu;
    public GameObject PanelConfirmQuit;
    public GameObject PanelCreditsMenu;
    public GameObject PanelContinueGameMenu;
    public GameObject PanelSavesList;
    public GameObject PanelNewGameMenu;

    public GameObject SaveGameListObjectPrefab;
    public List<GameObject> SaveGameListObjects;

    private Dictionary<string, string> savesList;

    private void Start()
    {
        MMenu.setMenuState(MainMenu.menuStates.loading);
        GameManager.Settings.ReadSettings();
        Camera.main.GetComponent<AudioSource>().Play();
        MMenu.setMenuState(MainMenu.menuStates.main);
    }

    private void Update()
    {
        PanelStartMenu.gameObject.SetActive(MMenu.State == MainMenu.menuStates.start);

        PanelMainMenu.gameObject.SetActive(MMenu.State == MainMenu.menuStates.main);

        PanelSettingsMenu.gameObject.SetActive(MMenu.State == MainMenu.menuStates.settings);

        PanelConfirmQuit.gameObject.SetActive(MMenu.State == MainMenu.menuStates.confirmclose);

        PanelCreditsMenu.gameObject.SetActive(MMenu.State == MainMenu.menuStates.credits);

        PanelContinueGameMenu.gameObject.SetActive(MMenu.State == MainMenu.menuStates.continuegame);

        PanelNewGameMenu.gameObject.SetActive(MMenu.State == MainMenu.menuStates.newgame);
    }


    #region public menu state methods

    public void setMenuStateLoading()
    {
        MMenu.setMenuState(MainMenu.menuStates.loading);
    }
    public void setMenuStateMain()
    {
        MMenu.setMenuState(MainMenu.menuStates.main);
    }

    public void setMenuStateStart()
    {
        destroySaveButtons();
        MMenu.setMenuState(MainMenu.menuStates.start);
    }
    public void setMenuStateContinueGame()
    {
        createSaveButtons();
        MMenu.setMenuState(MainMenu.menuStates.continuegame);
    }
    public void setMenuStateNewGame()
    {
        MMenu.setMenuState(MainMenu.menuStates.newgame);
    }
    public void setMenuStateSettings()
    {
        MMenu.setMenuState(MainMenu.menuStates.settings);
    }
    public void setMenuStateCredits()
    {
        MMenu.setMenuState(MainMenu.menuStates.credits);
    }
    public void setMenuStateConfirmClose()
    {
        MMenu.setMenuState(MainMenu.menuStates.confirmclose);
    }

    public void closeGame()
    {
        Application.Quit();
        print("Closed");
    }

    public void OpenGitHub()
    {
        Application.OpenURL("https://github.com/gradylorenzo/RocketFOSS");
    }

    public void OpenReddit()
    {
        Application.OpenURL("https://www.reddit.com/r/RocketFOSS/");
    }
    #endregion

    private void createSaveButtons()
    {
        GameObject newSaveButton;
        savesList = GameManager.Save.SavesList();
        foreach(KeyValuePair<string, string> kvp in savesList)
        {
            newSaveButton = Instantiate(SaveGameListObjectPrefab, PanelSavesList.transform, false);
            newSaveButton.GetComponent<SaveGameListObject>().name = kvp.Key;
            SaveGameListObjects.Add(newSaveButton);
            newSaveButton = null;
        }
    }

    private void destroySaveButtons()
    {
        if (MMenu.State == MainMenu.menuStates.continuegame)
        {
            foreach (GameObject go in SaveGameListObjects)
            {
                Destroy(go);
            }
            SaveGameListObjects.Clear();
        }
    }
}
