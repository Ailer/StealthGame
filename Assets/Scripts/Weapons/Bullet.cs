using UnityEngine;
using System.Linq;
using System.Collections;

public class Bullet : MonoBehaviour
{
    #region - Private
    #region - Vars

    private float _damage;
    private float _range;
    private float _speed;
    private Vector3 _startPosition;
    private const float rayDistance = 1f;
    private const string destroyhitTag = "House";

    #endregion

    #region - Functions

    // Use this for initialization
    private void Start()
    {
        this._startPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        rigidbody.MovePosition(transform.position + transform.forward * this._speed * Time.deltaTime);
        Vector3 delta = (this._startPosition + (transform.forward * this._range)) - this.rigidbody.position;

        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
        RaycastHit[] hits = Physics.RaycastAll(ray, Bullet.rayDistance);

        if (delta.magnitude <= 1
            || hits.Select(s => s.transform.tag).Contains(Bullet.destroyhitTag))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Ja");
    }

    #endregion
    #endregion

    #region - Public

    public float Damage
    {
        get
        {
            return this._damage;
        }
    }

    public void InstantiateBullet(float damage, float range, float speed)
    {
        this._damage = damage;
        this._range = range;
        this._speed = speed;
    }
    #endregion
}