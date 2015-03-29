using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class MessageInitialize : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        Debug.Log(Application.loadedLevelName);
        switch (Application.loadedLevelName)
        {
            case "Level1":
                this.GetMessageFromGameObjects("OpenDoor").ForEach(f => f.SetMessage("Aktionstaste drücken um die Tuer zu öffnen"));
                this.GetMessageFromGameObjects("OpenKeyDoor").ForEach(f => f.SetMessage(@"Um Türen mit einem farbigen Schloss zu öffnen 
wird ein Schlüssel benoetigt", this.OpenKeyDoorTrigger, 400, 50));
                break;
            case "Level2":
                this.GetMessageFromGameObjects("TowerButton").ForEach(f => f.SetMessage("Aktionstaste drücken um die Türme abzuschalten",
                                                                         this.TowerButtonTrigger));
                break;
            case "Level4":
                this.GetMessageFromGameObjects("Weapon").ForEach(f => f.SetMessage("Aktionstaste drücken um die Waffe aufzuheben",
                                                                       this.WeaponTrigger));
                this.GetMessageFromGameObjects("Attack").ForEach(f => f.SetMessage(@"Angriffstaste drücken um den Gegner anzugreifen.
Gegner stirbt sofort bei einem unbemerkten Angriff.",
                                                                       this.AttackTrigger, 400, 50));
                break;
            default:
                break;
        }
    }

    private List<Message> GetMessageFromGameObjects(string key)
    {
        return GameObject.FindObjectsOfType<Message>().Where(f => f.Key == key).ToList();
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
        return (GameObject.FindObjectOfType<EnemySoldier>() != null);
    }
    #endregion
}
