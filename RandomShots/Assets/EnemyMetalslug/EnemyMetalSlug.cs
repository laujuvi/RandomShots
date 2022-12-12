using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMetalSlug : Actor
{
    public enum States
    {
        Patrol,
        Persuit,
        Attack
    }

    public List<Transform> wayPoints;

    private Animator myAnimator;

    public  float speed;
    public float distance;
    public  float distanceToWaypoint;
    private float distanceToPlayer;
    public  float maxRadiusSight;
    public  float targetAngle;
    public  float angle;
    public  float distanceMin;
    private float TimeToShot = 0f;
    private float TimeShoot = 0.1f;
    public int nextPoint;
    
    public States state;

    public LayerMask mask;

    public Transform target;
    public Transform pointToShoot;
    public LifeController lifeController;


    public UnityEngine.GameObject bulletPrefab;

    private bool ISee;
    private bool IsFollow;
    private bool Shooting;
    
    public int MaxLife => 100;

    private void Awake()
    {
        myAnimator = gameObject.GetComponent<Animator>();
        lifeController.InitializateLife();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, target.position);
        LastPointSee();

    }
    private void LastPointSee()
    {
        if (target == null) return;
        if (LineOfSight(target))
        {

            ISee = true;
         

        }
        else
        {
            ISee = false;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Bullet bulletPlayer = collision.gameObject.GetComponent<Bullet>();
            lifeController.GetDamage(bulletPlayer.damageBullet);
            Destroy(collision.gameObject);
        }
    }
    private void FixedUpdate()
    {
        UpdateState();
    }

  
    private void UpdateState()
    {
        switch (state)
        {
            case States.Persuit:
                Persuit(target);
                break;
            case States.Patrol:
                Patrol();
                break;
            case States.Attack:
                CheckShoot();
                break;
        }
    }
    private bool LineOfSight(Transform target)
    {
        Vector3 forwardEnemy = transform.right;
        forwardEnemy.Normalize();
        Vector3 diff = target.position - transform.position;
        distance = diff.magnitude;
        if (maxRadiusSight < distance)
        {
            return false;
        }
        targetAngle = Vector3.Angle(forwardEnemy, diff.normalized);
        if (targetAngle > angle / 2)
        {
            return false;

        }
        if (Physics.Raycast(transform.position, diff.normalized, distance, mask))
        {
            return false;
        }
        return true;
    }

    private void Patrol()
    {
        if (!LineOfSight(target) && IsFollow == false)
        {
            myAnimator.SetBool("IsFollow", true);

            transform.position = Vector3.MoveTowards(transform.position, wayPoints[nextPoint].transform.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, wayPoints[nextPoint].transform.position) < distanceToWaypoint)
            {
                if (wayPoints[nextPoint].transform.position.y < transform.position.y)
                {
                    transform.Rotate(0, 180, 0);

                }
                else
                {
                    transform.Rotate(0, -180, 0);
                }

                nextPoint++;


                if (nextPoint >= wayPoints.Count)
                {
                    nextPoint = 0;
                }
            }
        }
        else
        {
            state = States.Persuit;
            IsFollow = true;

        }

    }
  
    private void Persuit(Transform target)
    {

        if (LineOfSight(target) && distanceToPlayer > distanceMin)
        {
            myAnimator.SetBool("IsFollow", true);

            IsFollow = false;
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else if (distanceToPlayer <= distanceMin)
        {
            CheckShoot();
        }
        else 
        {
            state = States.Patrol;
            IsFollow = false;
        }
    }
    private void Shoot()
    {
        myAnimator.SetBool("ISee", true);
        
        GameObject bullet = Instantiate(bulletPrefab);

        bullet.transform.position = pointToShoot.transform.position;
        bullet.transform.rotation = pointToShoot.transform.rotation;

    }
   
    private void CheckShoot()
    {
        if (distanceToPlayer > distanceMin)
        {
            state = States.Persuit;
            IsFollow = true;
        }
        else
        {
            if (TimeShoot > TimeToShot)
            {
                TimeToShot = 1;
                Shoot();
            }
            TimeToShot -= Time.deltaTime;
        }
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, maxRadiusSight);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceMin);
        Gizmos.DrawRay(transform.position, transform.right * distance);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 0, angle / 2) * transform.right * distance);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 0, -angle / 2) * transform.right * distance);
    }

 
}
