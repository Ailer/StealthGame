using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour
{
    public Texture2D Background;

    private void OnGUI()
    {

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);
        GUILayout.BeginArea(new Rect(Screen.width/2- 100, Screen.height / 2 - 200, 200, 700));
        GUIStyle headline = new GUIStyle(); 
        GUILayout.Label("<size=20><color=black> Can you Escape?</color></size>", headline);
        GUILayout.Space(25);
        GUIStyle buttonSkin = GUI.skin.GetStyle("Button");
        buttonSkin.margin = new RectOffset(0, 0, 0, 15);

        if (GUILayout.Button("Spiel starten"))
        {
            GameObject.FindObjectOfType<GameLogic>().LoadLevel("Intro");
        }

        if (GUILayout.Button("Level auswahl"))
        {
            GameObject.FindObjectOfType<GameLogic>().LoadLevel("SelectLevel");
        }

        if (GUILayout.Button("Steuerung anzeigen"))
        {
            GameObject.FindObjectOfType<GameLogic>().LoadLevel("ShowControl");
        }

        if (GUILayout.Button("Spielziel anzeigen!"))
        {
            GameObject.FindObjectOfType<GameLogic>().LoadLevel("ShowTarget");
        }

        if (GUILayout.Button("Spiel beenden"))
        {
            Application.Quit();
        }
        GUILayout.EndArea();
    }
}
