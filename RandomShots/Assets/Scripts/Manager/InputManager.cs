using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // ENTITIES / CONTROLLERS
    private playerTemp playerTemp;
    //private playerTemp playerTemp;

    // BINDING KEYS - WEAPONS
    [SerializeField] private KeyCode _weapon1 = KeyCode.Alpha1;
    [SerializeField] private KeyCode _weapon2 = KeyCode.Alpha2;
    [SerializeField] private KeyCode _weapon3 = KeyCode.Alpha3;

    // BINDING KEYS - ACTIONS
    [SerializeField] private KeyCode _attack = KeyCode.Mouse0;
    [SerializeField] private KeyCode _reload = KeyCode.R;

    [SerializeField] private KeyCode _moveForward = KeyCode.W;
    [SerializeField] private KeyCode _moveBack = KeyCode.S;
    [SerializeField] private KeyCode _moveLeft = KeyCode.A;
    [SerializeField] private KeyCode _moveRight = KeyCode.D;

    private void Start()
    {
        playerTemp = GetComponent<playerTemp>();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(_weapon1)) playerTemp.ChangeWeapon(0);
        //if (Input.GetKeyDown(_weapon2)) playerTemp.ChangeWeapon(1);

        if (Input.GetKeyDown(_attack)) playerTemp.Attack();
        if (Input.GetKeyDown(_reload)) playerTemp.Reload();

        if (Input.GetKey(_moveForward)) playerTemp.MoveForward();
        if (Input.GetKey(_moveBack)) playerTemp.MoveBack();
        if (Input.GetKey(_moveLeft)) playerTemp.MoveLeft();
        if (Input.GetKey(_moveRight)) playerTemp.MoveRight();
    }
}
