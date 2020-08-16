using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class enemy : MonoBehaviour
{
    public float wanderRadius = 10;
    public float fov = 80f;
    public float wanderTimer = 5;
    public float lamaNyari = 5;
    public AudioClip jumpScareClip;
    AudioSource jumpScareSource;
    public GameObject GameOver;
    //public float lamaKePos = 50;
    public List<GameObject> Pos = new List<GameObject>();

    Transform player,Enemy;
    NavMeshAgent nav;
    Animator anim;
    public bool PlayerNearby;
    private int rand;
    public float timer;
    public bool jalan;
    public float timer2;
    private float timerNyari;
    private Vector3 posisiPlayerTerakhir;
    private float speedEnemy;
    public bool death;
    private player playerScript;

    void Awake()
    {
        GameOver.SetActive(false);
        death = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = player.GetComponent<player>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        speedEnemy = nav.speed;
        jumpScareSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        Enemy = this.transform;
        timer += Time.deltaTime;
        //timer2 += Time.deltaTime;
        timerNyari -= Time.deltaTime;

        anim.SetFloat("Velocity", nav.velocity.magnitude);

        if (IsInMeleeRangeOf(player))
            RotateTowards(Enemy,player);

        if (PlayerInSight() == true)
        {
            nav.SetDestination(player.position);
            //timer = 0;
            //timer2 = 0;
            timerNyari = lamaNyari;
            nav.speed = speedEnemy + 2;
            jumpScareSource.PlayOneShot(jumpScareClip,0.7f);
            //Debug.Log("Chasing");
        }
        else
            nav.speed = speedEnemy;

        if (PlayerInSight() == false && timerNyari >= 0)
        {
            nav.SetDestination(posisiPlayerTerakhir);
            //Debug.Log("Searching");
        }
        
        if (jalan == false && timer >= wanderTimer)
        {
            rand = Random.Range(0, Pos.Count);
            nav.SetDestination(Pos[rand].transform.position);
        }

        if (nav.remainingDistance > nav.stoppingDistance + 2)
            jalan = true;
        
        else if (nav.remainingDistance < nav.stoppingDistance + 2)
            jalan = false;

        if (jalan == true)
            timer = 0;

        //else if (PlayerInSight() == false && timer >= wanderTimer)
        //{
        //    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        //    nav.SetDestination(newPos);
        //    timer = 0;
        //    //Debug.Log("Wandering");
        //}

        //else if (PlayerInSight() == false && timer2 > lamaKePos)
        //{
        //    rand = Random.Range(0, Pos.Count);
        //    nav.SetDestination(Pos[rand].transform.position);
        //    timer2 = 0;
        //}

        // DEATH
        if (death == true)
        {
            timer2 += Time.deltaTime;
            timer = 0;
            nav.velocity = new Vector3(0,0,0);
            playerScript.moveSpeed = 0;
            
            RotateTowards(player,Enemy);
            if (timer2 >= 2f)
                GameOver.SetActive(true);

            if (timer2 >= 5f)
                SceneManager.LoadScene("SampleScene");

        }
    }


    private bool PlayerInSight()
    {
        Vector3 targetDir = player.position - transform.position;
        float angleToPlayer = (Vector3.Angle(targetDir, transform.forward));

        RaycastHit hit;
        var rayDirection = player.transform.position - transform.position;
        if (PlayerNearby == true)
        {
            if (Physics.Raycast(transform.position, rayDirection, out hit))
            {
                if (angleToPlayer >= -fov && angleToPlayer <= fov)
                {
                    if (hit.transform.tag == "Player")
                    {
                        posisiPlayerTerakhir = hit.point;
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }

            else
                return false;
        }
        else
            return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerNearby = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            death = true;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    private bool IsInMeleeRangeOf(Transform target)
    {
        float distance = Vector3.Distance(transform.position, target.position);
        return distance < 6f;
    }

    private void RotateTowards(Transform thisObjek, Transform target)
    {
        Vector3 direction = (target.position - thisObjek.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        thisObjek.rotation = Quaternion.Slerp(thisObjek.rotation, lookRotation, 2*Time.deltaTime);
    }
}
