using UnityEngine;
using System.Collections;


//TODO: Ziel Cursor im Menü verbergen
public class GameLogic : MonoBehaviour
{
    private MainMenu _mainMenu;
    private bool _levelFinished;
    public const string Name = "GameLogic";
    public Texture2D CursorTexture;

    // Use this for initialization
    private void Start()
    {
        Screen.showCursor = false;
        this._mainMenu = this.gameObject.AddComponent<MainMenu>();
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
        if (this._mainMenu.IsMenuClosed())
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
        this._mainMenu.ShowMenu();
    }

    public void FinishLevel()
    {
        this._levelFinished = true;
        this._mainMenu.ShowMenu(true);
    }

    public static void LoadLevel(int levelId)
    {
        Application.LoadLevel(levelId);
        Time.timeScale = 1;
        Screen.showCursor = true;
    }

    public static void DeactivateCameraControl()
    {
        MouseLook [] mouseLooks = GameObject.FindObjectsOfType<MouseLook>();

        foreach (MouseLook mouselook in mouseLooks)
        {
            mouselook.enabled = false;
        }
    }

    public static void ActivateCameraControl()
    {
        MouseLook[] mouseLooks = GameObject.FindObjectsOfType<MouseLook>();

        foreach (MouseLook mouselook in mouseLooks)
        {
            mouselook.enabled = true;
        }
    }
}
