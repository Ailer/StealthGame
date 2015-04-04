using UnityEngine;
using System.Collections;

public class StartMenu : MenuBase
{
    protected override void OnGUI()
    {
        base.OnGUI();
        base.SetMenuBase(25, Screen.width / 2 - 70, 75, "Can You Escape?");

        if (GUI.Button(new Rect(this.GetLeftPosition(), this.GetTopPositionForElement(), MenuBase.ButtonWidth, MenuBase.ButtonHeight),
            "Spiel starten"))
        {
            GameObject.FindObjectOfType<GameLogic>().LoadLevel("Intro");
        }

        if (GUI.Button(new Rect(this.GetLeftPosition(), this.GetTopPositionForElement(), MenuBase.ButtonWidth, MenuBase.ButtonHeight),
            "Level auswahl"))
        {
            GameObject.FindObjectOfType<GameLogic>().LoadLevel("SelectLevel");
        }

        if (GUI.Button(new Rect(this.GetLeftPosition(), this.GetTopPositionForElement(), MenuBase.ButtonWidth, MenuBase.ButtonHeight),
            "Steuerung anzeigen"))
        {
            GameObject.FindObjectOfType<GameLogic>().LoadLevel("ShowControl");
        }

        if (GUI.Button(new Rect(this.GetLeftPosition(), this.GetTopPositionForElement(), MenuBase.ButtonWidth, MenuBase.ButtonHeight),
            "Spielziel anzeigen!"))
        {
            GameObject.FindObjectOfType<GameLogic>().LoadLevel("ShowTarget");
        }

        if (GUI.Button(new Rect(this.GetLeftPosition(), this.GetTopPositionForElement(), MenuBase.ButtonWidth, MenuBase.ButtonHeight),
            "Spiel beenden"))
        {
            Application.Quit();
        }
    }
}
