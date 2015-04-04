using UnityEngine;
using System.Collections;

public class MainMenu : MenuBase
{
    private bool showMenu = false;
    private bool levelFinished = false;

    private void CloseMenu()
    {
        this.showMenu = false;
        Time.timeScale = 1;
        Screen.showCursor = false;
        GameObject.FindObjectOfType<GameLogic>().ActivateCameraControl();
    }

    protected override void OnGUI()
    {
        if (this.showMenu)
        {
            bool showPreviousLevel = Application.loadedLevel > 1 ? true : false;
            Screen.showCursor = true;
            Time.timeScale = 0;
            this.SetMenuBase(10, Screen.width / 2 - 70, 75);
            GUI.Label(new Rect(base.GetLeftPosition(), 10, 70, 35), "<color=white> <size=20> Menü </size> </color>");

            if (this.levelFinished)
            {
                GUIStyle centeredStyle = GUI.skin.GetStyle("Label");
                centeredStyle.alignment = TextAnchor.MiddleCenter;
                GUILayout.Label("<size=20>Level beendet. </size>", centeredStyle);
            }

            if (!this.levelFinished &&
                GUI.Button(new Rect(base.GetLeftPosition(), base.GetTopPositionForElement(), MenuBase.ButtonWidth, MenuBase.ButtonHeight),
                "Fortsetzen"))
            {
                this.CloseMenu();
            }
            else if (showPreviousLevel &&
                GUI.Button(new Rect(base.GetLeftPosition(), base.GetTopPositionForElement(), MenuBase.ButtonWidth, MenuBase.ButtonHeight),
                "Vorheriges Level laden"))
            {
                GameObject.FindObjectOfType<GameLogic>().LoadLevel(Application.loadedLevel - 1);
                this.CloseMenu();
            }
            else if (GUI.Button(new Rect(base.GetLeftPosition(), base.GetTopPositionForElement(), MenuBase.ButtonWidth, MenuBase.ButtonHeight),
                "Level neustarten"))
            {
                GameObject.FindObjectOfType<GameLogic>().LoadLevel(Application.loadedLevel);
                this.CloseMenu();
            }
            else if (this.levelFinished
                && Application.loadedLevel < Application.levelCount
                && GUI.Button(new Rect(base.GetLeftPosition(), base.GetTopPositionForElement(), MenuBase.ButtonWidth, MenuBase.ButtonHeight),
                "Nächstes Level"))
            {
                GameObject.FindObjectOfType<GameLogic>().LoadLevel(Application.loadedLevel + 1);
                this.CloseMenu();
            }
            if (GUI.Button(new Rect(base.GetLeftPosition(), base.GetTopPositionForElement(), MenuBase.ButtonWidth, MenuBase.ButtonHeight),
                "Zurück zum Hauptmenü"))
            {
                GameObject.FindObjectOfType<GameLogic>().LoadLevel("StartMenu");
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
        GameObject.FindObjectOfType<GameLogic>().DeactivateCameraControl();
    }
}
