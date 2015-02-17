using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySoldier : EnemyBase
{
    #region - Private
    #region - Vars

    //private const float visualRange = 3f;
    private const float followPlayerTimerStart = 10;
    private const float detectWallRayCastDistance = 2;
    private const string wallTag = "House";
    private const float distanceToPlayer = 5f;
    private int _currentWaypoint = 0;
    //private Animator _enemyController;
    private bool _walkToWaypoints = true;
    private float _followPlayerTimer;
    private NavMeshAgent _navMeshAgent;
    private float _walkSpeed;


    #endregion

    #region - Functions
    // Use this for initialization
    private new void Start()
    {
        //this._enemyController = this.GetComponent<Animator>();
        base.Start();
        this._navMeshAgent = this.GetComponent<NavMeshAgent>();
        this._walkSpeed = this._navMeshAgent.speed;
        this._followPlayerTimer = 0;
        InstantiateAttentionIndicator();
    }

    private void InstantiateAttentionIndicator()
    {
        this.AttentionIndicator.ProgressbarRect = new Rect(Screen.width - 345, Screen.height - 50, 250, 20);
        this.AttentionIndicator.ShowProgressbar = false;
        this.AttentionIndicator.MaxValue = EnemySoldier.followPlayerTimerStart;
        this.AttentionIndicator.CurrentValue = this._followPlayerTimer;
        this.AttentionIndicator.Text = "Spielerverfolgen";
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        this.Move();
    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    private void Update()
    {
        this.ChangeWalkMode();
    }

    private void WalkToWayPoints()
    {
        Vector3 target = new Vector3(this.Waypoints[this._currentWaypoint].position.x,
                                     this.transform.position.y,
                                     this.Waypoints[this._currentWaypoint].position.z);
        Vector3 delta = target - transform.position;

        if (delta.magnitude <= 1)
        {
            if (this._currentWaypoint < (this.Waypoints.Count - 1))
            {
                this._currentWaypoint++;
            }
            else
            {
                this._currentWaypoint = 0;
            }
        }
        else
        {
            this._navMeshAgent.SetDestination(target);
            this.SetSpeed(this._walkSpeed);
        }
    }

    private void FollowPlayer()
    {
        Vector3 target = this._player.transform.position;
        this._navMeshAgent.SetDestination(target);

        if (this._navMeshAgent.remainingDistance <= EnemySoldier.distanceToPlayer)
        {
            this._navMeshAgent.speed = 0;
            //this.SetSpeed(0);
            //this.transform.LookAt(new Vector3(target.x, this.transform.position.y, target.z));
        }
        else
        {
            this.SetSpeed(this._walkSpeed);
        }
    }

    private void Move()
    {
        if (this._walkToWaypoints
            && this._followPlayerTimer == 0)
        {
            this.WalkToWayPoints();
            StopCoroutine("LookAtPlayer");
        }
        else
        {
            this.FollowPlayer();
            StartCoroutine("LookAtPlayer", 30);
        }
    }

    private void SetSpeed(float speed)
    {
        if (speed > 0)
        {
            this._navMeshAgent.speed = speed;
            //this._enemyController.SetFloat("Speed", speed);
        }
        else
        {
            this._navMeshAgent.Stop(true);
            Debug.Log("Stop");
            //this._enemyController.SetFloat("Speed", 0);
        }
    }

    private void ChangeWalkMode()
    {
        this._followPlayerTimer = this._followPlayerTimer > 0 ? this._followPlayerTimer - 1.0f * Time.deltaTime : 0f;
        this.AttentionIndicator.CurrentValue = this._followPlayerTimer;
        this.Weapon.Attack = false;

        if (base.LocateTarget())
        {
            this._walkToWaypoints = false;
            this.Weapon.Attack = true;
            this._followPlayerTimer = EnemySoldier.followPlayerTimerStart;
            this.AttentionIndicator.ShowProgressbar = true;
        }
        else if (this._followPlayerTimer == 0)
        {
            this._walkToWaypoints = true;
            this.AttentionIndicator.ShowProgressbar = false;
        }
    }

    private void OnTriggerEnter(Collider inCollider)
    {
        if (inCollider.gameObject.name == "Knife(Clone)")
        {
            if (this._followPlayerTimer > 0)
            {
                this.Live -= inCollider.GetComponent<Knife>().Damage;
            }
            else
            {
                this.Live = 0;
            }
        }

        if (this.Live == 0)
        {
            Destroy(this.gameObject);
        }
    }
    #endregion
    #endregion

    #region - Public
    #region - Vars

    public float Live;
    public Progressbar AttentionIndicator;
    public List<Transform> Waypoints;

    #endregion
    #endregion
}
