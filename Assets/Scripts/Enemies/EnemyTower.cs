using UnityEngine;
using System.Collections;
using System;

public class EnemyTower : EnemyBase
{
    #region - Private
    #region - Vars

    private bool _foundPlayer;
    private Quaternion _startRotation;
    private float _distanceToTarget;
    float rotationY = 0;

    #endregion
    // Use this for initialization
    private void Start()
    {
        this._startRotation = this.transform.rotation;
        this._foundPlayer = false;
        this.rotationY = this._startRotation.y;
        base.Start();
    }



    private void FixedUpdate()
    {
        this.GetComponentInChildren<Light>().enabled = this.IsActivated;

        if (this.IsActivated)
        {
            if (base.LocateTarget())
            {
                this._foundPlayer = true;
                this.AttackWithWeapons();
                this.StartCoroutine("LookAtPlayer", this.RotationSpeed);
            }
            else
            {
                this._foundPlayer = false;
                this.StopAttackWithWeapons();
                this.StopCoroutine("LookAtPlayer");
                float angle = Quaternion.Angle(this._startRotation, this.transform.rotation);
                this._distanceToTarget = Math.Abs(this.MaxRotation / 2) - angle;

                if (angle >= this.MaxRotation / 2)
                {
                    this.RotationSpeed *= -1;
                }

                this.transform.Rotate(0, this.RotationSpeed * Time.deltaTime, 0);
            }
        }
        else
        {
            this.StopCoroutine("LookAtPlayer");
            this.AttackWithWeapons();
        }
    }
    #endregion

    #region - Protected
    #region - Functions

    protected virtual IEnumerator LookAtPlayer(float rotationSpeed)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        bool lookAt = false;
        Quaternion destRotation;
        Quaternion rotation;

        while (true)
        {
            Debug.DrawRay(ray.origin, ray.direction * this.VisualRange, Color.blue);
            lookAt = false;

            if (Physics.Raycast(ray, out hit, this.VisualRange))
            {
                if (hit.transform.gameObject == this._player)
                {
                    Debug.Log("Hit");
                    lookAt = true;
                }
            }

            if (!lookAt)
            {
                destRotation = Quaternion.LookRotation(this._player.transform.position - this.transform.position);
                rotation = Quaternion.Slerp(this.transform.rotation, destRotation, rotationSpeed * Time.deltaTime);

                if (Quaternion.Angle(this._startRotation, rotation) <= this.MaxRotation / 2)
                {
                    transform.rotation = new Quaternion(0, rotation.y, 0, rotation.w);
                }
            }

            yield return null;
        }
    }

    #endregion
    #endregion

    #region - Public
    #region - Vars

    public float MaxRotation;
    public float RotationSpeed;
    public bool IsActivated;

    #endregion
    #endregion
}
