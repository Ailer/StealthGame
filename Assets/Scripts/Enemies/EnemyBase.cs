using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class EnemyBase : MonoBehaviour
{
    #region - Protected
    #region - Vars
    #region - Const

    protected GameObject _player;

    #endregion
    #endregion

    #region - Functions

    protected virtual void Start()
    {
        this._player = GameObject.Find(Player.PlayerName);
        this.StopAttackWithWeapons();

        foreach (Weapon weapon in Weapons)
        {
            this.StartCoroutine(weapon.AttackTarget());
        }
    }

    protected virtual bool LocateTarget()
    {
        RaycastHit hit;
        Ray ray = new Ray();
        ray.origin = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

        for (int i = this.VisualAngle / (-2); i < this.VisualAngle / 2; i += this.VisualAngleIncrement)
        {
            ray.direction = Quaternion.AngleAxis(i, transform.up) * transform.forward;
            Debug.DrawRay(ray.origin, ray.direction * this.VisualRange, Color.yellow);
            if (Physics.Raycast(ray, out hit, this.VisualRange))
            {
                if (hit.transform.gameObject.name == Player.PlayerName)
                {
                    return true;
                }
            }
        }

        return false;
    }

    protected virtual IEnumerator LookAtPlayer(float rotationSpeed)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        bool lookAt = false;
        Quaternion destRotation;
        Quaternion rotation;

        while (true)
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.blue, this.VisualRange);
            lookAt = false;

            if (Physics.Raycast(ray, out hit, this.VisualRange))
            {
                if (hit.transform.gameObject == this._player)
                {
                    lookAt = true;
                }
            }

            if (!lookAt)
            {
                destRotation = Quaternion.LookRotation(this._player.transform.position - this.transform.position);
                rotation = Quaternion.Slerp(this.transform.rotation, destRotation, rotationSpeed * Time.deltaTime);
                transform.rotation = new Quaternion(0, rotation.y, 0, rotation.w);
            }

            yield return null;
        }
    }

    public void AttackWithWeapons()
    {
        foreach (Weapon weapon in Weapons)
        {
            weapon.Attack = true;
        }
    }

    public void StopAttackWithWeapons()
    {
        foreach (Weapon weapon in Weapons)
        {
            weapon.Attack = false;
        }
    }

    #endregion
    #endregion

    #region - Public

    public List<Weapon> Weapons;
    public float VisualRange;
    public int VisualAngle;
    public int VisualAngleIncrement;

    #endregion
}
