using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class Message : MonoBehaviour
{
    #region - Privat
    #region - Vars

    private float _remainingMessageTime;
    private bool _canShow;

    #endregion

    #region - Functions

    //private void Update()
    //{
    //    this._remainingMessageTime = this._remainingMessageTime > 0 ? this._remainingMessageTime - 1 * Time.deltaTime : 0;

    //    if (this._remainingMessageTime == 0)
    //    {
    //        this.ShowMessage = false;
    //    }
    //}

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == Player.PlayerName)
        {
            GameObject.FindObjectOfType<MessageSystem>().ShowMessage(this);
            Debug.Log("Ja");
            //this._remainingMessageTime = this.ShowMessageTime;
            //this.ShowMessage = true;
        }
    }

    //private void OnGUI()
    //{
    //    this._canShow = GameObject.FindObjectsOfType<TutorialObject>().FirstOrDefault(f => f.ShowMessage && f != this) == null;

    //    if (this.ShowMessage
    //        && this._canShow
    //        && this._remainingMessageTime > 0)
    //    {
    //        //GUILayout.BeginArea(new Rect(Screen.width / 2 - this.TextWidth / 2, Screen.height - 50, this.TextWidth, this.TextHeight));
    //        GUI.Box(new Rect(Screen.width / 2 - this.TextWidth / 2, Screen.height - 50, this.TextWidth, this.TextHeight), string.Format("{0}", this.Message));
    //        //GUILayout.Label(string.Format("<size=17>{0}</size>", this.Message));
    //    }
    //}
    #endregion
    #endregion

    #region - Public
    #region - Vars

    public string MessageText;
    public Func<bool> Trigger;
    public string Key;
    public float TextWidth = 350;
    public float TextHeight = 25;

    #endregion

    #region - Properties

    public bool GetTrigger
    {
        get
        {
            return this.Trigger == null ? true : this.Trigger();
        }
    }
    #endregion

    #region - Functions

    public void SetMessage(string messageText, Func<bool> trigger, float textWidth = 350, float textHeight = 25)
    {
        this.MessageText = messageText;
        this.TextHeight = textHeight;
        this.TextWidth = textWidth;
        this.Trigger = trigger;
    }

    public void SetMessage(string messageText, float textWidth, float textHeight)
    {
        this.SetMessage(messageText, null,textWidth,textHeight);
    }

    public void SetMessage(string messageText)
    {
        this.SetMessage(messageText, null);
    }
    #endregion
    #endregion
}