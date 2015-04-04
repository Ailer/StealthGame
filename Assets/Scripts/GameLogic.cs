using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{
    public const string Name = "GameLogic";
    private MainMenu _mainMenu;
    private bool _levelFinished;
    public Texture2D CursorTexture;
    public bool ShowCrossAir = true;
    public bool IsMenuActivated = true;

    private void Start()
    {
        this._mainMenu = this.gameObject.AddComponent<MainMenu>();

        if (Application.loadedLevelName.Contains("Level")
            && Application.loadedLevelName != "SelectLevel")
        {
            Screen.showCursor = false;
        }
        else
        {
            Screen.showCursor = true;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.JoystickButton7)
            || Input.GetKeyUp(KeyCode.Escape))
        {
            this.ShowMainMenu();
        }
    }

    private void OnGUI()
    {
        if (this.ShowCrossAir
            && this._mainMenu.IsMenuClosed())
        {
            GUI.DrawTexture(new Rect(Screen.width / 2 - this.CursorTexture.width / 2,
                             Screen.height / 2 - this.CursorTexture.height / 2,
                             this.CursorTexture.width,
                             this.CursorTexture.height),
                    this.CursorTexture);
        }
    }

    public void ShowMainMenu()
    {
        if (this.IsMenuActivated)
        {
            this._mainMenu.ShowMenu();
        }
    }

    public void FinishLevel()
    {
        this._levelFinished = true;

        if (Application.loadedLevel + 1 == Application.levelCount)
        {
            this.LoadLevel("Credits");
        }
        else
        {
            this._mainMenu.ShowMenu(true);
        }
    }

    public void LoadLevel(int levelId)
    {
        Application.LoadLevel(levelId);
        Time.timeScale = 1;
        Screen.showCursor = false;
    }

    public void LoadLevel(string name)
    {
        Application.LoadLevel(name);
        Time.timeScale = 1;
    }

    public void DeactivateCameraControl()
    {
        MouseLook[] mouseLooks = GameObject.FindObjectsOfType<MouseLook>();

        foreach (MouseLook mouselook in mouseLooks)
        {
            mouselook.enabled = false;
        }
    }

    public void ActivateCameraControl()
    {
        MouseLook[] mouseLooks = GameObject.FindObjectsOfType<MouseLook>();

        foreach (MouseLook mouselook in mouseLooks)
        {
            mouselook.enabled = true;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
