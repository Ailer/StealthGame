using UnityEngine;
using System.Collections;
using System.Linq;

public class TutorialObject : MonoBehaviour
{
    #region - Privat
    #region - Vars

    private float _remainingMessageTime;
    private bool _canShow;

    #endregion

    #region - Functions

    private void Update()
    {
        this._remainingMessageTime = this._remainingMessageTime > 0 ? this._remainingMessageTime - 1 * Time.deltaTime : 0;

        if (this._remainingMessageTime == 0)
        {
            this.ShowMessage = false;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == Player.PlayerName)
        {
            this._remainingMessageTime = this.ShowMessageTime;
            this.ShowMessage = true;
        }
    }

    private void OnGUI()
    {
        this._canShow = GameObject.FindObjectsOfType<TutorialObject>().FirstOrDefault(f => f.ShowMessage && f != this) == null;

        if (this.ShowMessage
            && this._canShow
            && this._remainingMessageTime > 0)
        {
            //GUILayout.BeginArea(new Rect(Screen.width / 2 - this.TextWidth / 2, Screen.height - 50, this.TextWidth, this.TextHeight));
            GUI.Box(new Rect(Screen.width / 2 - this.TextWidth / 2, Screen.height - 50, this.TextWidth, this.TextHeight), string.Format("{0}", this.Message));
            //GUILayout.Label(string.Format("<size=17>{0}</size>", this.Message));
        }
    }
    #endregion
    #endregion

    #region - Public
    #region - Vars

    public string Message;
    public bool ShowMessage = false;
    public float ShowMessageTime;
    public float TextWidth;
    public float TextHeight;

    #endregion
    #endregion
}