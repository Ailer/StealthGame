using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonEnemyTower : Button
{
    #region - Public
    #region - Vars

    public List<EnemyTower> ControlledTowers;

    #endregion

    #region - Functions

    public override void PushButton()
    {
        if (!this.IsActivated)
        {
            this.renderer.material.color = Color.red;
            this.ControlledTowers.ForEach(f => f.IsActivated = false);
            this.IsActivated = true; 
        }
    }
    #endregion
    #endregion
}
