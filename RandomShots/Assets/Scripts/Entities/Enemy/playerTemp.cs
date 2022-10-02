using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTemp :MonoBehaviour
{
    public float speed;
    private TDAQueue queue;
    public int max;
    public int currentBullets;
    public int currentGuns;
    public TDAStack stack;

    private int i;
    [SerializeField] private Gun gun;
    private CmdMove _cmdMoveForward;
    private CmdMove _cmdMoveBack;
    private CmdMove _cmdMoveLeft;
    private CmdMove _cmdMoveRight;
    private CmdAttack _cmdAttack;
    private void Awake()
    {
        queue = new TDAQueue();
        stack = new TDAStack();
       
    }
    void Start()
    {
        queue.Init(max);
        stack.Init(max);


        var mc = GetComponent<MovementController>();
        // _cmdMoveForward = new CmdMove(mc, Vector3.forward);
        // _cmdMoveBack = new CmdMove(mc, -Vector3.forward);
        _cmdMoveLeft = new CmdMove(mc, Vector2.left);
        _cmdMoveRight = new CmdMove(mc, Vector2.right);
    }

    private void ChangeWeapon()
    {
       // gun = Instantiate(pila.Tope() , transform);
        gun.Reload();
        _cmdAttack = new CmdAttack(gun);

    }
    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 direction = new Vector3(x, v*10);
        direction.Normalize();

        if (x != 0f || v != 0f)
        {
            transform.Translate(direction * Time.deltaTime * speed);
        }

      
         if (Input.GetKeyDown(KeyCode.P))
         { 
            queue.Dequeue();
           // cola.ImprimoCola();
         }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullets"))
        {
            
            if (currentBullets < max)
            {
                queue.Queue(collision.gameObject);
                currentBullets++;
                collision.gameObject.SetActive(false);
                
            }

        }
        else if (collision.gameObject.CompareTag("Guns"))
        {
            if (currentGuns< max)
            {
                stack.Stack(collision.gameObject);
                currentGuns ++;
                collision.gameObject.SetActive(false);
            }
        }
    }
    public void Attack() => GameManager.instance.AddEventQueue(_cmdAttack);
    public void Reload() => gun?.Reload();
    public void MoveForward() => GameManager.instance.AddEventQueue(_cmdMoveForward);
    public void MoveBack() => GameManager.instance.AddEventQueue(_cmdMoveBack);
    public void MoveLeft() => GameManager.instance.AddEventQueue(_cmdMoveLeft);
    public void MoveRight() => GameManager.instance.AddEventQueue(_cmdMoveRight);
}
