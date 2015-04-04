using UnityEngine;
using System.Collections;

public abstract class MenuBase : MonoBehaviour
{
    private float currentTopPosition;
    private float startTopPosition;
    private float leftPosition;
    private float spaceBetweenElements;
    private string headline;
    private const float HeadlineTopPosition = 5;
    private const float spaceBetweenHeadAndElements = 25;
    protected const float ButtonWidth = 250;
    protected const float ButtonHeight = 50;
    public Texture2D Background;

    protected void SetMenuBase(float startTopPosition, float leftPosition, float spaceBetweenElements, string headline = null)
    {
        this.startTopPosition = startTopPosition;
        this.leftPosition = leftPosition;
        this.spaceBetweenElements = spaceBetweenElements;
        this.currentTopPosition = this.startTopPosition + MenuBase.HeadlineTopPosition + MenuBase.spaceBetweenHeadAndElements;
        this.headline = headline;
    }

    protected float GetTopPositionForElement()
    {
        float tmp = this.currentTopPosition;
        this.currentTopPosition += this.spaceBetweenElements;

        return tmp;
    }

    protected float GetLeftPosition()
    {
        return this.leftPosition;
    }

    protected virtual void OnGUI()
    {

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);
        GUI.Label(new Rect(this.GetLeftPosition(), MenuBase.HeadlineTopPosition, 300, 25),
                  string.Format("<size=20><color=black>{0}</color></size>", this.headline));
    }
}
