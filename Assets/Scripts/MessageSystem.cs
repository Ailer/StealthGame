using UnityEngine;
using System.Collections;

public class MessageSystem : MonoBehaviour
{
    Vector2 scrollPosition = Vector2.zero;

    private void OnGUI()
    {
        //// An absolute-positioned example: We make a scrollview that has a really large client
        //// rect and put it in a small rect on the screen.
        //scrollPosition = new Vector2(0, Input.GetAxis("Mouse ScrollWheel"));
        //Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        //GUI.BeginScrollView(new Rect(Screen.width / 2, Screen.height - 100, 300, 100),

        //scrollPosition, new Rect(0, 0, 220, 200));
        //GUI.Label(new Rect(0, 0, 50, 50), "Test");
        //// Make four buttons - one in each corner. The coordinate system is defined
        //// by the last parameter to BeginScrollView.
        //GUI.Button(new Rect(0, 0, 100, 20), "Top-left");
        //GUI.Button(new Rect(120, 0, 100, 20), "Top-right");
        //GUI.Button(new Rect(0, 180, 100, 20), "Bottom-left");
        //GUI.Button(new Rect(120, 180, 100, 20), "Bottom-right");
        //// End the scroll view that we began above.
        //GUI.EndScrollView(true);
    }
}
