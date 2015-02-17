using UnityEngine;
using System.Collections;

public class FinalDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == Player.PlayerName)
        {
            GameObject.Find(GameLogic.Name).GetComponent<GameLogic>().FinishLevel();
        }
    }
}
