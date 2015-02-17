using UnityEngine;
using System.Collections;

public class Gun : Weapon
{
    #region - Public
    #region - Vars

    public GameObject BulletPrefab;
    public float ShootingSpeed;
    public float BulletSpeed;

    #endregion

    #region - Functions

    public override IEnumerator AttackTarget()
    {
        while (true)
        {
            if (this.Attack)
            {
                Debug.Log("Schiessen");
                Vector3 position = new Vector3(transform.position.x + transform.forward.x * 0.12f, transform.position.y + 0.12f, transform.position.z);
                GameObject bulletGob = Instantiate(this.BulletPrefab,
                                                   position,
                                                   transform.rotation) as GameObject;
                Bullet bullet = bulletGob.GetComponent<Bullet>();
                bullet.InstantiateBullet(this.Damage, this.Range, this.BulletSpeed);
            }

            yield return new WaitForSeconds(100 / this.ShootingSpeed);
        }
    }
    #endregion
    #endregion
}
