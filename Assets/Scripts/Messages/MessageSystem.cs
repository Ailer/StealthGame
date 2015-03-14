using UnityEngine;
using System.Collections;

public class MessageSystem : MonoBehaviour
{
    #region - Private
    #region - Vars

    private bool _showActiveMessage = false;
    private Message _activeMessage;
    private float _remainingMessageTime;

    #endregion

    #region - Functions

    private void Update()
    {
        this._remainingMessageTime = this._remainingMessageTime > 0 ? this._remainingMessageTime - 1 * Time.deltaTime : 0;

        if (this._remainingMessageTime == 0)
        {
            this._showActiveMessage = false;
            this._activeMessage = null;
        }
    }

    private void OnGUI()
    {
        if (this._activeMessage != null
            && this._showActiveMessage
            && this._activeMessage.GetTrigger)
        {
            GUI.skin.box.alignment = TextAnchor.UpperLeft;
            GUI.Box(new Rect(10, 10, this._activeMessage.TextWidth, this._activeMessage.TextHeight),this._activeMessage.MessageText);
        }
    }
    #endregion
    #endregion

    #region - Public
    #region - Vars

    public float MessageTime;

    #endregion

    public void ShowMessage(Message message)
    {
        this._activeMessage = message;
        this._showActiveMessage = true;
        this._remainingMessageTime = MessageTime;
    }
    #endregion
}
