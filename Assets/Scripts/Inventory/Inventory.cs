using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory : ScriptableObject
{
    #region - Private
    #region - Vars

    private IDictionary<string, InventoryObject> _inventory = new Dictionary<string, InventoryObject>();

    #endregion
    #endregion

    #region - Public
    #region - Vars

    public bool ShowInventory;

    #endregion

    #region - Functions

    /// <summary>
    /// Gets the inventar object.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    public InventoryObject GetInventoryObject(string name)
    {
        if (!this._inventory.ContainsKey(name))
        {
            return null;
        }

        return this._inventory[name];
    }

    /// <summary>
    /// Adds the inventar object.
    /// </summary>
    /// <param name="inventoryObject">The inventar object.</param>
    public void AddInventoryObject(InventoryObject inventoryObject, int quantity = 1)
    {
        if (this._inventory.ContainsKey(inventoryObject.ObjectName))
        {
            this._inventory[inventoryObject.ObjectName].Quantity += 1;
        }
        else
        {
            this._inventory.Add(inventoryObject.ObjectName, inventoryObject);
        }
    }

    /// <summary>
    /// Removes the inventory object.
    /// </summary>
    /// <param name="inventoryObject">The inventory object.</param>
    /// <param name="destroyObject">if set to <c>true</c> [destroy object].</param>
    public void RemoveInventoryObject(InventoryObject inventoryObject, bool destroyObject = false)
    {
        if (inventoryObject != null &&
            this._inventory.ContainsKey(inventoryObject.ObjectName))
        {
            this._inventory.Remove(inventoryObject.ObjectName);

            if (destroyObject)
            {
                Destroy(inventoryObject);
            }
        }
    }

    /// <summary>
    /// Gets the complete inventory.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<InventoryObject> GetCompleteInventory()
    {
        return this._inventory.Select(s => s.Value);
    }
    #endregion
    #endregion
}
