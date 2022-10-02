using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTemp : Actor
{
    public float speed;
    private Cola cola;
    public int maximo;
    public int currentBullets;
    public int currentGuns;
    public TDAPila pila;

    [SerializeField] private List<int> layerMasks;
    [SerializeField] private List<Gun> gunList;
    [SerializeField] private Gun gun;
    [SerializeField] private GameObject shotgun;
    [SerializeField] private GameObject pistol;
    private int intPistol = 1;
    private int intShotgun = 2;

    private CmdMove _cmdMoveForward;
    private CmdMove _cmdMoveBack;
    private CmdMove _cmdMoveLeft;
    private CmdMove _cmdMoveRight;
    private CmdAttack _cmdAttack;
    private void Awake()
    {
        cola = new Cola();
        pila = new TDAPila();

    }
    void Start()
    {
 //--------INITIALIZE COLA & PILA-------
        cola.Inicializarcola(maximo);
        pila.InicializarPila(maximo);

        var mc = GetComponent<MovementController>();
        // _cmdMoveForward = new CmdMove(mc, Vector3.forward);
        // _cmdMoveBack = new CmdMove(mc, -Vector3.forward);
        _cmdMoveLeft = new CmdMove(mc, Vector2.left);
        _cmdMoveRight = new CmdMove(mc, Vector2.right);
    }
    public void Attack() => GameManager.instance.AddEventQueue(_cmdAttack);
    public void Reload() => gun?.Reload();
    public void MoveForward() => GameManager.instance.AddEventQueue(_cmdMoveForward);
    public void MoveBack() => GameManager.instance.AddEventQueue(_cmdMoveBack);
    public void MoveLeft() => GameManager.instance.AddEventQueue(_cmdMoveLeft);
    public void MoveRight() => GameManager.instance.AddEventQueue(_cmdMoveRight);

    //private void ChangeWeapon()
    //{
    //    _cmdAttack = new CmdAttack(gun);

    //}
    private void Update()
    {
       // ChangeWeapon();
        float x = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 direction = new Vector3(x, v * 10);
        direction.Normalize();

        if (x != 0f || v != 0f)
        {
            transform.Translate(direction * Time.deltaTime * speed);
        }


        if (Input.GetKeyDown(KeyCode.P))
        {
            var guns = pila.Tope();
            pila.Desapilar();
            ChangeWeaponStateMachine(guns);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cola.Desacolar();
            cola.ImprimoCola();
        }
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (layerMasks.Contains(collision.gameObject.layer))
        {
            Debug.Log(collision.gameObject.name);

            if (currentGuns < maximo)
            {
                if (collision.gameObject.name == "Pistol")
                {
                    pila.Apilar(intPistol);
                    currentGuns++;
                    collision.gameObject.SetActive(false);
                }
                else if (collision.gameObject.name == "Shotgun")
                {
                    pila.Apilar(intShotgun);
                    currentGuns++;
                    collision.gameObject.SetActive(false);
                }
            }
        }
         if (layerMasks.Contains(collision.gameObject.layer))
        {
            if (currentBullets < maximo)
            {
                if (collision.gameObject.name == "Misil")
                {
                    cola.Acolar(collision.gameObject);
                    collision.gameObject.SetActive(false);
                    currentBullets++;
                }
                else if (collision.gameObject.name == "Balas9mm")
                {
                    cola.Acolar(collision.gameObject);
                    collision.gameObject.SetActive(false);
                    currentBullets++;
                }
                else if (collision.gameObject.name == "Tornillos")
                {
                    cola.Acolar(collision.gameObject);
                    collision.gameObject.SetActive(false);
                    currentBullets++;
                }
            }
        }
    }
    private void ChangeWeaponStateMachine(int gunType)
    {
        switch (gunType)
        {
            case 1:
                transform.GetChild(1).gameObject.SetActive(true);
                gun.Reload();
                _cmdAttack = new CmdAttack(gun);
                break;
            case 2:
                transform.GetChild(2).gameObject.SetActive(true);
                gun.Reload();
                _cmdAttack = new CmdAttack(gun);
                break;

            default:
                break;
        }
    }
}
    
