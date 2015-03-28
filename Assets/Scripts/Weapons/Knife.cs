using UnityEngine;
using System.Collections;

public class Knife : Weapon
{
    private void Start()
    {
        this.Range = 2;
        this.Damage = 2;
    }

    public override IEnumerator AttackTarget()
    {
        Camera cam = Camera.main;
        this.transform.rotation = cam.transform.rotation;
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * this.Range, Color.yellow, 1);
        float realRange = base.Range;
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, this.Range))
        {
            Vector3 tmp = hit.transform.position - transform.position;
            realRange = tmp.magnitude - 1;
        }

        if (realRange > 0)
        {
            this.transform.position = cam.transform.position;
            this.transform.position += transform.forward * realRange;
        }
        else
        {
            this.transform.rotation = transform.parent.rotation;
        }

        yield return new WaitForSeconds(0.3f);
        this.transform.position = this.transform.parent.position;
        this.transform.rotation = this.transform.parent.rotation;
    }
}
