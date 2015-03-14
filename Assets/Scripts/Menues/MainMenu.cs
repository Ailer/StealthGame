using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    private bool showMenu = false;
    private bool levelFinished = false;

    private void CloseMenu()
    {
        this.showMenu = false;
        Time.timeScale = 1;
        Screen.showCursor = false;
        GameLogic.ActivateCameraControl();
    }

    private void OnGUI()
    {
        if (this.showMenu)
        {
            bool showPreviousLevel = Application.loadedLevel > 1 ? true : false;
            Screen.showCursor = true;
            Time.timeScale = 0;
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 70, 400, 400));
            GUILayout.Label("<size=20>Menue</size>");

            if (this.levelFinished)
            {
                GUIStyle centeredStyle = GUI.skin.GetStyle("Label");
                centeredStyle.alignment = TextAnchor.MiddleCenter;
                GUILayout.Label("<size=20>Level beendet. </size>", centeredStyle);
            }

            if (!this.levelFinished && GUILayout.Button("Fortsetzen"))
            {
                this.CloseMenu();
            }
            else if (showPreviousLevel && GUILayout.Button("Vorheriges Level laden"))
            {
                GameLogic.LoadLevel(Application.loadedLevel - 1);
                this.CloseMenu();
            }
            else if (GUILayout.Button("Level neustarten"))
            {
                GameLogic.LoadLevel(Application.loadedLevel);
                this.CloseMenu();
            }
            else if (this.levelFinished
                && Application.loadedLevel < Application.levelCount
                && GUILayout.Button("Nächstes Level"))
            {
                GameLogic.LoadLevel(Application.loadedLevel + 1);
                this.CloseMenu();
            }
            if (GUILayout.Button("Beenden"))
            {
                Application.Quit();
            }

            GUILayout.EndArea();
        }
    }

    public bool IsMenuClosed()
    {
        return !this.showMenu;
    }

    public void ShowMenu(bool levelFinished = false)
    {
        this.showMenu = true;
        this.levelFinished = levelFinished;
        GameLogic.DeactivateCameraControl();
    }
}
