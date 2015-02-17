using UnityEngine;
using System.Collections;

public abstract class Button : MonoBehaviour 
{
    public const string ButtonTag = "Button";

    public abstract void PushButton();
}
