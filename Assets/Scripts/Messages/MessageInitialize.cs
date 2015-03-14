using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class MessageInitialize : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        switch (Application.loadedLevel)
        {
            case 5:
                this.GetMessageFromGameObject("OpenDoor").SetMessage("Aktionstaste druecken um die Tuer zu oeffnen");
                this.GetMessageFromGameObject("OpenKeyDoor").SetMessage(@"Um Tueren mit einem farbigen Schloss zu oeffnen 
wird ein Schluessel benoetigt",this.OpenKeyDoorTrigger, 400, 50);
                break;
            case 6:
                this.GetMessageFromGameObject("TowerButton").SetMessage("Aktionstaste druecken um die Tuerme abzuschalten", 
                                                                         this.TowerButtonTrigger);
                break;
            case 8:
                this.GetMessageFromGameObject("Weapon").SetMessage("Aktionstaste druecken um die Waffe aufzuheben",
                                                                       this.WeaponTrigger);
                this.GetMessageFromGameObject("Attack").SetMessage("Aktionstaste druecken um die Tuerme abzuschalten",
                                                                       this.AttackTrigger);
                break;
            default:
                break;
        }
    }

    private Message GetMessageFromGameObject(string key)
    {
        return GameObject.FindObjectsOfType<Message>().FirstOrDefault(f => f.Key == key);
    }

    #region - Trigger

    private bool OpenKeyDoorTrigger()
    {
        return !Player.PlayerInventory.InventoryContainsObject("Key0") && GameObject.Find("FinalDoor").GetComponent<Door>().Key != null;
    }

    private bool TowerButtonTrigger()
    {
        return !GameObject.FindObjectOfType<Button>().IsActivated;
    }

    private bool WeaponTrigger()
    {
        Debug.Log(GameObject.Find("Knife") != null);
        return GameObject.Find("RightHandObject").GetComponentInChildren<Weapon>() == null;
    }

    private bool AttackTrigger()
    {
        return GameObject.Find("Enemy0") != null;
    }
    #endregion
}
