using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour
{
    private bool _showControl = false;
    private bool _showGameTarget = false;

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(50, Screen.height / 2 - 200, 200, 700));
        GUIStyle headline = new GUIStyle(); 
        GUILayout.Label("<size=20><color=white> Can you Escape?</color></size>", headline);
        GUILayout.Space(25);
        GUIStyle buttonSkin = GUI.skin.GetStyle("Button");
        buttonSkin.margin = new RectOffset(0, 0, 0, 15);

        if (GUILayout.Button("Spiel starten"))
        {
            Application.LoadLevel(Application.loadedLevel + 1);
        }

        if (GUILayout.Button("Steuerung anzeigen        >"))
        {
            this._showControl = !this._showControl;
            this._showGameTarget = false;
        }

        if (GUILayout.Button("Spielziel anzeigen!         >"))
        {
            this._showGameTarget = !this._showGameTarget;
            this._showControl = false;
        }

        if (GUILayout.Button("Spiel beenden"))
        {
            Application.Quit();
        }
        GUILayout.EndArea();

        string content = "<size=17>Meldungen:</size>";

        if (this._showGameTarget)
        {
            content = @"<size=17>Spielziel: </size>

Ziel des Spiels ist es aus dem Haus zu entkommen.
Um die Tueren oeffnen zu koennen benoetigt man den entsprechenden Schluessel.
Ein Schluessel wird fuer alle Tueren mit farbigen Griff benoetigt.
Dabei sollte man moeglichst unaufaellig vorgehen um keine Wachen zu alarmieren.";
        }
        else if (this._showControl)
        {
            content = @"<size=17>Steuerung:</size>
 
Vorwaerts:W 
Links: A
Rechts: D
Rueckwaerts: S
Aktionstaste: E
Angreifen: Linke Maustaste";
        }

        GUI.Box(new Rect(330, Screen.height / 2 - 155, 500, 300), content);
    }
}
