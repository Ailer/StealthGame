using UnityEngine;
using System.Collections;

public abstract class Button : MonoBehaviour 
{
    public const string ButtonTag = "Button";
    public bool IsActivated = false;
    public abstract void PushButton();
}
