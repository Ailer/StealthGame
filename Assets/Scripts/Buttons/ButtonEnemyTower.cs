using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonEnemyTower : Button
{
    #region - Private
    #endregion

    #region - Public
    #region - Vars

    public List<EnemyTower> ControlledTowers;

    #endregion

    #region - Functions

    public override void PushButton()
    {
        this.renderer.material.color = Color.red;
        this.ControlledTowers.ForEach(f => f.IsActivated = false);
    }
    #endregion
    #endregion
}
