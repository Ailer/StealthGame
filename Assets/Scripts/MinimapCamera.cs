using UnityEngine;
using System.Collections;

public class MinimapCamera : MonoBehaviour
{
    #region - Private
    #region - Vars

    private GameObject _player;

    #endregion

    #region - Functions

    // Use this for initialization
    void Start()
    {
        this._player = GameObject.Find("Player") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
		this.transform.position = new Vector3(this._player.transform.position.x, this.transform.position.y, this._player.transform.position.z);
    }
    #endregion
    #endregion
}