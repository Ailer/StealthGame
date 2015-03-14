using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Door : MonoBehaviour
{
    #region - Public
    #region - Vars

    public InventoryObject Key;

    #endregion

    #region - Functions

    public void ActivateDoor(InventoryObject key = null)
    {
        this._showCantOpenDoorDialog = false;

        if (this.CanOpen(key))
        {
            this._isOpen = true;
            this._animator.SetTrigger(openDoorStateName);
            this.SetDoorLockColor(gameObject.GetComponentsInChildren<Renderer>()[0].material.color);
        }
        else if (this._isOpen)
        {
            this._isOpen = false;
            this._animator.SetTrigger(closeDoorStateName);
        }
        else
        {
            this.ShowCantOpenDoorDialog();
        }
    }

    #endregion
    #endregion

    #region - Private
    #region - Vars

    private const string openDoorStateName = "Open";
    private const string closeDoorStateName = "Close";
    private Animator _animator;
    protected bool _isOpen;
    private bool _showCantOpenDoorDialog = false;
    private const float openDialogTime = 5f;
    private float _currentDialogTime = 0;

    #endregion

    #region - Functions

    private void Start()
    {
        this._animator = gameObject.GetComponentInChildren<Animator>();

        if (this.Key != null)
        {
            this.SetDoorLockColor(this.Key.renderer.material.color);
        }
    }

    private void SetDoorLockColor(Color color)
    {
        IEnumerable<Renderer> doorLocks = gameObject.GetComponentsInChildren<Renderer>().Where(w => w.name == "Doorlock");

        foreach (Renderer doorLock in doorLocks)
        {
            doorLock.material.color = color;
        }
    }

    private void Update()
    {
        if (this._currentDialogTime < Door.openDialogTime
            && this._showCantOpenDoorDialog)
        {
            this._currentDialogTime += Time.deltaTime;
        }
    }

    private bool CanOpen(InventoryObject key)
    {
        return (this.Key == key || this.Key == null) && !this._isOpen;
    }

    private void OnGUI()
    {
        if (this._showCantOpenDoorDialog
            && this._currentDialogTime <= Door.openDialogTime)
        {
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 50, Screen.height - 50, 500, 20));
            GUILayout.Label("Tuer ist verschlossen.");
            GUILayout.EndArea();
        }
    }

    private void ShowCantOpenDoorDialog()
    {
        this._currentDialogTime = 0;
        this._showCantOpenDoorDialog = true;
    }
    #endregion
    #endregion
}
