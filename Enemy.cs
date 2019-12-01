using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Text txt;
    int score;
    public PlayerController player;
    public BulletTime bulletTime;
    private NavMeshAgent enemy;
    private NavMeshAgent agent;
    public GameObject Player;
    public float enemyDistanceRun = 60.0f;
    public Animator anim;
    public Transform[] points;
    private int destPoint = 0;
    private bool enter = false;
    public float EnemyHealth = 50f;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        agent = GetComponent<NavMeshAgent>();


        agent.autoBraking = false;

        GotoNextPoint();

    }

    // Update is called once per frame
    void Update()
    {

        score=System.Convert.ToInt32(txt.text);

        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if ((distance < enemyDistanceRun)&&(EnemyHealth>0))
        {

            Vector3 dirToPlayer = transform.position - Player.transform.position;

            Vector3 newPos = transform.position - dirToPlayer;

            enemy.SetDestination(newPos);


            anim.SetBool("run", true);
            if (SceneManager.GetActiveScene().name == "Level3")
                enemy.speed = 15f;
            else
                enemy.speed = 7f;

        }
        else
        {
            anim.SetBool("run", false);
        }

        if (!agent.pathPending && agent.remainingDistance < 2f)
        {
            
            GotoNextPoint();
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if ((other.tag == "Player") && (other.transform.position.y > 4))
        {
            Destroy(gameObject);

        }

        if ((other.tag == "Player") && (currentScene.name == "Level4"))
        { }

       
    }

    void GotoNextPoint()
    {

        if (points.Length == 0)
            return;
        anim.SetBool("walk", true);

        agent.destination = points[destPoint].position;


        destPoint = (destPoint + 1) % points.Length;
        anim.SetBool("walk", true);
    }

    IEnumerator your_timer()
    {
        enter = true;
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
        enter = false;
    }

    public void takeDamage( float damage)
    {
        EnemyHealth -= damage;
        
        if ((EnemyHealth<=0)&&(EnemyHealth>=-20))
        {
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
            anim.SetBool("dead", true);
            FindObjectOfType<AudioManager>().Play("Wolf");
            
            if (enter == false)
                StartCoroutine(your_timer());
           
            DoThing();
            
        }
    }

    IEnumerator your_timer2()
    {

        player.enabled = false;
        FindObjectOfType<AudioManager>().Play("YouWin");
        yield return new WaitForSeconds(1.5f);

    }

    public void DoThing()
    {
        if (SceneManager.GetActiveScene().name == "Level1")
            score += 20;
        if (SceneManager.GetActiveScene().name == "Level2")
            score += 10;
        if (SceneManager.GetActiveScene().name == "Level3")
            score += 50;

        txt.text = string.Format("{0} ", score);

        if (score == 100)
        {
            bulletTime.bulletTime();

            StartCoroutine(your_timer2());

            GameManager.instance.DoThing();

        }

    }

}
