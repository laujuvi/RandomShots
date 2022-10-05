using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // ENTITIES / CONTROLLERS
    private Character _character;
    //[SerializeField] private Animator _animator;

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
        //_character = GetComponent<Character>();
        //_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //_animator.SetFloat("XDirection", 0);
        //_animator.SetFloat("Shoot", 0);
        if (Input.GetKeyDown(_weapon1)) _character.ChangeWeapon(0);
        if (Input.GetKeyDown(_weapon2)) _character.ChangeWeapon(1);
        if (Input.GetKeyDown(_weapon3)) _character.ChangeWeapon(2);


        if (Input.GetKeyDown(_attack)) _character.Attack();
        if (Input.GetKeyDown(_reload)) _character.Reload();

        if (Input.GetKeyDown(_jump)) {
            _character.Jump();
           // _animator.SetFloat("Shoot", 1);
        }


        if (Input.GetKey(_moveLeft)) {
            _character.MoveLeft();
            //_animator.SetFloat("XDirection", -1);
        }
        if (Input.GetKey(_moveRight)) {
            _character.MoveRight();
            //_animator.SetFloat("XDirection", 1);
        }  
    }
}
