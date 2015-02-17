using UnityEngine;
using System.Collections;

public class Progressbar : MonoBehaviour
{
    #region - Private
    #region - Functions

    private void OnGUI()
    {
        if (this.ShowProgressbar)
        {
            GUI.DrawTexture(this.ProgressbarRect, this.ProgressbarBackground, ScaleMode.StretchToFill);
            GUI.DrawTexture(new Rect(this.ProgressbarRect.x,
                                     this.ProgressbarRect.y,
                                     (this.CurrentValue / this.MaxValue) * this.ProgressbarRect.width,
                                     this.ProgressbarRect.height), 
                            this.ProgressbarForeground, ScaleMode.StretchToFill);
            GUI.Label(this.ProgressbarRect, this.Text);
        }
    }
    #endregion
    #endregion

    #region - Public
    #region - Vars

    public float MaxValue;
    public float CurrentValue;
    public Texture2D ProgressbarBackground;
    public Texture2D ProgressbarForeground;
    public bool ShowProgressbar;
    public string Text; 
    public Rect ProgressbarRect;

    #endregion

    #region - Function
    #endregion
    #endregion
}