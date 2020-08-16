using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameBuilder : MonoBehaviour
{
    public List<GameObject> Dinding = new List<GameObject>();
    public List<GameObject> SpawnPointEnemy = new List<GameObject>();
    public List<GameObject> SpawnPointPlayer = new List<GameObject>();
    public GameObject loadingScreen;

    NavMeshAgent pathFinder;
    NavMeshPath path,path2,path3;
    Transform enemy,player,Kunci,Finish;
    private float rand;
    private bool rand2, GameRun;
    private int rand3,rand4,rand5,rand6;
    private Vector3 dir;

    private float timer,timer3;

    void Awake()
    {
        loadingScreen.SetActive(true);
        path = new NavMeshPath();
        path2 = new NavMeshPath();
        path3 = new NavMeshPath();
        pathFinder = GameObject.FindGameObjectWithTag("Enemy").GetComponent<NavMeshAgent>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Kunci = GameObject.FindGameObjectWithTag("Kunci").transform;
        Finish = GameObject.FindGameObjectWithTag("Finish").transform;
        GenerateLevel();
    }

    private void Update()
    {
        if (!GameRun)
        {
            timer += Time.deltaTime;

            if (timer > 0 && adaJalan() == false)
            {
                timer = 0;
                GenerateLevel();
                //Debug.Log("Jalan tidak ada");
            }

            if (adaJalan() == true)
            {
                timer3 += Time.deltaTime;
            }

            if (timer3 >= 2f)
            {
                loadingScreen.SetActive(false);
                GameRun = true;
            }
        }

        else
        {
            timer = 0;
            timer3 = 0;
        }
    }

    private bool adaJalan()
    {
        pathFinder.CalculatePath(player.position, path);
        pathFinder.CalculatePath(Kunci.transform.position, path2);
        pathFinder.CalculatePath(Finish.transform.position, path3);

        if (path3.status == NavMeshPathStatus.PathComplete)
        {
            if (path2.status == NavMeshPathStatus.PathComplete)
            {
                if (path.status == NavMeshPathStatus.PathComplete)
                {
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

    private void GenerateLevel()
    {
        for (int i = 0; i < Dinding.Count; i++)
        {
            rand = Random.Range(0, 2) == 0 ? 0 : 90;
            dir = new Vector3(0f, rand, 0f);
            Dinding[i].transform.Rotate(dir);

            rand2 = Random.Range(0, 10) == 0 ? false : true;
            Dinding[i].SetActive(rand2);
        }

        rand3 = Random.Range(0, SpawnPointEnemy.Count);
        enemy.transform.position = new Vector3(SpawnPointEnemy[rand3].transform.position.x,enemy.transform.position.y, SpawnPointEnemy[rand3].transform.position.z);

        rand4 = Random.Range(0, SpawnPointPlayer.Count);
        player.transform.position = new Vector3(SpawnPointPlayer[rand3].transform.position.x, player.transform.position.y, SpawnPointPlayer[rand3].transform.position.z);

        rand5 = Random.Range(0, Dinding.Count);
        Kunci.transform.position = new Vector3(Dinding[rand5].transform.position.x-3.9f, Kunci.transform.position.y, Dinding[rand5].transform.position.z-3.3f);

        rand6 = Random.Range(0, Dinding.Count);
        Finish.transform.position = new Vector3(Dinding[rand6].transform.position.x + 3.3f, Finish.transform.position.y, Dinding[rand6].transform.position.z + 3.3f);
    }
}
