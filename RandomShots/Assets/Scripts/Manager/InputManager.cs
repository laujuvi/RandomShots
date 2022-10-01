using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // ENTITIES / CONTROLLERS
    private Character _character;

    // BINDING KEYS - WEAPONS
    [SerializeField] private KeyCode _weapon1 = KeyCode.Alpha1;
    [SerializeField] private KeyCode _weapon2 = KeyCode.Alpha2;
    [SerializeField] private KeyCode _weapon3 = KeyCode.Alpha3;

    // BINDING KEYS - ACTIONS
    [SerializeField] private KeyCode _attack = KeyCode.Mouse0;
    [SerializeField] private KeyCode _reload = KeyCode.R;
    [SerializeField] private KeyCode _jump = KeyCode.Space;

    //[SerializeField] private KeyCode _moveForward = KeyCode.W;
    [SerializeField] private KeyCode _moveBack = KeyCode.S;
    [SerializeField] private KeyCode _moveLeft = KeyCode.A;
    [SerializeField] private KeyCode _moveRight = KeyCode.D;

    private void Start()
    {
        _character = GetComponent<Character>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_weapon1)) _character.ChangeWeapon(0);
        if (Input.GetKeyDown(_weapon2)) _character.ChangeWeapon(1);

        if (Input.GetKeyDown(_attack)) _character.Attack();
        if (Input.GetKeyDown(_reload)) _character.Reload();
        if (Input.GetKeyDown(_jump)) _character.Jump();


        //  if (Input.GetKey(_moveForward)) _character.MoveForward();
        if (Input.GetKey(_moveBack)) _character.MoveBack();
        if (Input.GetKey(_moveLeft)) _character.MoveLeft();
        if (Input.GetKey(_moveRight)) _character.MoveRight();
    }
}
