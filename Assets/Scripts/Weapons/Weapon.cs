using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{
    #region - Public
    #region - Vars

    public float Damage;
    public float Range;
    public bool Attack;
    public string Name;

    #endregion

    #region - Functions

    public abstract IEnumerator AttackTarget();

    #endregion
    #endregion
}