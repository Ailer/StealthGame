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
        //float rotationX = transform.localEulerAngles.y + Time.deltaTime* this.RotationSpeed;
        //rotationX = Mathf.Clamp(rotationX, -this.MaxRotation / 2, this.MaxRotation / 2);
        ////rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        //rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

        //transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        ////rotationY += RotationSpeed * Time.deltaTime;
        //rotationY = Mathf.Clamp(rotationY, this._startRotation.y - (MaxRotation / 2), this._startRotation.y + MaxRotation / 2);

        //rotationY += RotationSpeed * Time.deltaTime;
        //transform.localEulerAngles = new Vector3(this._startRotation.x, rotationY, 0);
        //Debug.Log(rotationY);
        ////Debug.Log(transform.eulerAngles.y);
        ////transform.Rotate(0, 5, 0);

        //if (transform.eulerAngles.y >= this.MaxRotation / 2)
        //{
        //    this.RotationSpeed *= -1;
        //}

        //this.transform.Rotate(0, 5, 0);
        this.GetComponentInChildren<Light>().enabled = this.IsActivated;
        //transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, this.MaxRotation);

        if (this.IsActivated)
        {
            if (base.LocateTarget())
            {
                this._foundPlayer = true;
                this.Weapon.Attack = true;
                this.StartCoroutine("LookAtPlayer", this.RotationSpeed);
            }
            else
            {
                this._foundPlayer = false;
                base.Weapon.Attack = false;
                this.StopCoroutine("LookAtPlayer");
                float angle = Quaternion.Angle(this._startRotation, this.transform.rotation);
                this._distanceToTarget = Math.Abs(this.MaxRotation / 2) - angle;

                if (angle >= this.MaxRotation / 2)
                {
                    this.RotationSpeed *= -1;
                }

                this.transform.Rotate(0, this.RotationSpeed * Time.deltaTime, 0);
                //this.transform.rotation.
            }
        }
        else
        {
            this.StopCoroutine("LookAtPlayer");
            this.Weapon.Attack = false;
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

        GameObject obj = new GameObject();


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
