using UnityEngine;
using System.Collections;

public class InventoryObject : MonoBehaviour
{
	#region - Private

	private void Start()
	{
		this.ObjectName = this.gameObject.name;
	}

	private void OnTriggerEnter(Collider inCollider)
	{
		if (inCollider.gameObject.name == "Player")
		{
			Player.PlayerInventory.AddInventoryObject(this.GetComponent<InventoryObject>());
			this.gameObject.SetActive(false);
		}
	}
	#endregion

	#region - Public
	#region - Vars

	public Texture2D Icon;
	public string ObjectName;
	public float Quantity = 1;

	#endregion
	#endregion
}
