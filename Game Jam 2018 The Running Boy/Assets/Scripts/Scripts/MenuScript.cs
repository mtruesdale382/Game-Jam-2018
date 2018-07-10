using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    KeyCode MenuClose = KeyCode.Escape;
    KeyCode OpenInventory = KeyCode.I;
    
    int activeMenuIndex = 0;
    public GameObject[] MenuScreens;

    public GameObject PlayerObject;
    private UnityStandardAssets.Characters.FirstPerson.MouseLook Player;

    public bool CurrentlyInGame = false;
    public bool AllowMenuToClose = false;

    private float defaultMouseSensitivityX;
    private float defaultMouseSensitivityY;

    public float defaultTimeScale;

    private void Start()
    {
        if (PlayerObject)
        {
            Player = PlayerObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().mouseLook;
            defaultMouseSensitivityX = Player.XSensitivity;
            defaultMouseSensitivityY = Player.YSensitivity;
        }
        defaultTimeScale = Time.timeScale;

        ChangeMenuTo(0);
    }

    private void Update()
    {
        //Navigate back one menu when player presses escape.
        if(Input.GetKeyDown(MenuClose) && AllowMenuToClose)
        {
            if (activeMenuIndex != 0)
            {
                ResumeGame();
            }
            else if(CurrentlyInGame)
            {
                AllowPlayerControl(false);
                ChangeMenuTo(1);
            }
        }

        if(Input.GetKeyDown(OpenInventory) && AllowMenuToClose && CurrentlyInGame)
        {
            
            if (activeMenuIndex != 2)
            {
                ChangeMenuTo(2);
                AllowPlayerControl(false);
            }
            else
            {
                ResumeGame();
            }
        }

        //If in main menu splash screen, close splash screen when any input happens.
        if (Input.anyKeyDown && !CurrentlyInGame && activeMenuIndex == 0)
        {
            ChangeMenuTo(1);
        }
    }

    //MAIN MENU BUTTONS
    //
    //
    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/Scene_Level");
    }

    //Navigate between menus.
    public void ChangeMenuTo(int newMenuIndex)
    {
        if (MenuScreens[activeMenuIndex])
        {
            MenuScreens[activeMenuIndex].SetActive(false);
        }
        if (MenuScreens[newMenuIndex])
        {
            MenuScreens[newMenuIndex].SetActive(true);
        }

        activeMenuIndex = newMenuIndex;
    }

    //Also used in pause menu
    public void QuitGame()
    {
        Application.Quit();
    }
    //
    //
    //PAUSE MENU BUTTONS
    //
    //
    public void ResumeGame()
    {
        ChangeMenuTo(0);
        if (CurrentlyInGame)
        {
            AllowPlayerControl(true);
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    private void AllowPlayerControl(bool allowControl)
    {
        
        if (allowControl)
        {
            Time.timeScale = defaultTimeScale;
            Player.SetCursorLock(true);
        }
        else
        {
            Time.timeScale = 0.0f;
            Player.SetCursorLock(false);
        }
    }
}

