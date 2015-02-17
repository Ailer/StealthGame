using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//TODO: Waffe in GUI anzeigen
public class Player : MonoBehaviour
{
    #region - Private
    #region - Vars

    //private IList<GameObject> _inventory = new List<GameObject>();
    private Animator _playerController;
    private Weapon _weapon;
    private const float openDistance = 8f;
    private const string weaponTag = "Weapon";
    private const string keyTag = "Key";
    private const string pushButtonAnimationName = "PushButton";
    private const string speedVariableName = "Speed";
    public const string PlayerName = "Player";
    private GameObject _rightHand;

    #endregion

    #region - Functiions

    void Start()
    {
        this._playerController = this.GetComponent<Animator>();
        this._rightHand = GameObject.Find("RightHandObject") as GameObject;
        this.CurrentLive = this.MaxLive;
        Player.PlayerInventory = ScriptableObject.CreateInstance<Inventory>();
        InstantiateLiveIndicator();
        //this._rightHand.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, transform.position.x);
    }

    private void InstantiateLiveIndicator()
    {
        this.LiveIndicator.ProgressbarRect = new Rect(85, Screen.height - 50, 200, 20);
        this.LiveIndicator.MaxValue = this.MaxLive;
        this.LiveIndicator.CurrentValue = this.CurrentLive;
        this.LiveIndicator.Text = string.Format("{0}/{1}", this.CurrentLive, this.MaxLive);
    }

    private void FixedUpdate()
    {
        Vector3 movement = Vector3.zero;
        movement += Input.GetAxis("Horizontal") * transform.right;
        movement += Input.GetAxis("Vertical") * transform.forward;

        if (movement != Vector3.zero)
        {
            this.SetSpeed(this.WalkSpeed);
        }
        else
        {
            this.SetSpeed(0);
        }

        rigidbody.MovePosition(rigidbody.position + (movement.normalized * this.WalkSpeed * Time.deltaTime));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton3)
            || Input.GetKeyDown(KeyCode.E))
        {
            this.InteractWithGameObject();
        }
        else if (this._weapon != null
            && (Input.GetKeyDown(KeyCode.Mouse0)
            || Input.GetKeyDown(KeyCode.Q)
            || Input.GetKeyDown(KeyCode.JoystickButton1)))
        {
            StartCoroutine(this._weapon.AttackTarget());
        }
    }

    private void InteractWithGameObject()
    {
        RaycastHit hit;

        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), transform.forward * Player.openDistance, Color.yellow);

        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), transform.forward, out hit, Player.openDistance))
        {
            if (hit.transform.tag == "Door")
            {
                this._playerController.SetTrigger(Player.pushButtonAnimationName);
                Door door = hit.transform.gameObject.GetComponent<Door>();

                if (door.Key != null)
                {
                    InventoryObject key = Player.PlayerInventory.GetInventoryObject(door.Key.name);
                    door.ActivateDoor(key);
                    PlayerInventory.RemoveInventoryObject(key, true);
                }
                else
                {
                    door.ActivateDoor();
                }
            }
            else if (hit.transform.tag == Player.weaponTag
                    && !hit.transform.gameObject.name.Contains("Clone"))
            {
                this._weapon = (Instantiate(this.Weapons[0]) as GameObject).GetComponent<Weapon>();
                Destroy(hit.transform.gameObject);
                this._weapon.transform.parent = this._rightHand.transform;
                this._weapon.transform.rotation = this._rightHand.transform.rotation;
                this._weapon.transform.position = this._rightHand.transform.position;
            }
            else if (hit.transform.tag == Button.ButtonTag)
            {
                Debug.Log("Button");
                Button button = hit.transform.GetComponent<Button>();
                button.PushButton();
            }
        }
    }

    private void SetSpeed(float speed)
    {
        if (speed >= 0)
        {
            this._playerController.SetFloat(Player.speedVariableName, speed);
        }
    }

    private void OnTriggerEnter(Collider inCollider)
    {
        if (inCollider.gameObject.name == "Bullet(Clone)")
        {
            Debug.Log("ja");
            Bullet bullet = inCollider.GetComponent<Bullet>();
            this.CurrentLive = this.CurrentLive - bullet.Damage < 0 ? 0 : this.CurrentLive - bullet.Damage;
            this.LiveIndicator.CurrentValue = this.CurrentLive;
            this.LiveIndicator.Text = string.Format("{0}/{1}", this.CurrentLive, this.MaxLive);
            Destroy(inCollider.gameObject);

            if (this.CurrentLive <= 0)
            {
                Debug.Log("Game Over");
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10,
                           this.LiveIndicator.ProgressbarRect.y - 30,
                           70,
                           25),
                           "<size=15> Waffe: </size>");

        Rect weaponNameRect = new Rect(85,
                   this.LiveIndicator.ProgressbarRect.y - 30,
                   100,
                   25);

        if (this._weapon == null)
        {
            GUI.Label(weaponNameRect,
                      "<size=15> Keine Waffe </size>");
        }
        else
        {
            GUI.Label(weaponNameRect,
                      string.Format("<size=15> {0} </size>", this._weapon.GetComponent<Weapon>().Name));
        }

        GUI.Label(new Rect(10,
                           this.LiveIndicator.ProgressbarRect.y,
                           70,
                           25),
                           "<size=15>Leben: </size>");

        float left = this.LiveIndicator.ProgressbarRect.x + this.LiveIndicator.ProgressbarRect.width + 20;

        foreach (InventoryObject item in Player.PlayerInventory.GetCompleteInventory().Select(s => s).Take(5))
        {
            GUI.DrawTexture(new Rect(left, this.LiveIndicator.ProgressbarRect.y - 12.5f, 50, 50), item.Icon);
            left += 65;
        }
    }
    #endregion
    #endregion

    #region - Public
    #region - Vars

    public float WalkSpeed;
    public float MaxLive;
    public float CurrentLive;
    public List<GameObject> Weapons;
    public Progressbar LiveIndicator;
    public static Inventory PlayerInventory;

    #endregion
    #endregion
}
