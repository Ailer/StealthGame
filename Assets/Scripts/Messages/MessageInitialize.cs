using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class MessageInitialize : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        switch (Application.loadedLevelName)
        {
            case "Level1":
                this.GetMessageFromGameObject("OpenDoor").SetMessage("Aktionstaste drücken um die Tuer zu öffnen");
                this.GetMessageFromGameObject("OpenKeyDoor").SetMessage(@"Um Türen mit einem farbigen Schloss zu öffnen 
wird ein Schlüssel benoetigt",this.OpenKeyDoorTrigger, 400, 50);
                break;
            case "Level2":
                this.GetMessageFromGameObject("TowerButton").SetMessage("Aktionstaste drücken um die Türme abzuschalten", 
                                                                         this.TowerButtonTrigger);
                break;
            case "Level4":
                this.GetMessageFromGameObject("Weapon").SetMessage("Aktionstaste drücken um die Waffe aufzuheben",
                                                                       this.WeaponTrigger);
                this.GetMessageFromGameObject("Attack").SetMessage(@"Angriffstaste drücken um den Gegner anzugreifen. 
                Gegner stirbt sofort bei einem unbemerkten Angriff.",
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
        return (GameObject.FindObjectOfType<EnemySoldier>() != null); 
    }
    #endregion
}
