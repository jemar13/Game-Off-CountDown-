using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class RobotEnemy : MonoBehaviour
{
    private NavMeshAgent enemy;
    public GameObject Player;
    public float enemyDistanceRun = 20.0f;
    public Text txt;
    int score;
    public PlayerController player;
    public BulletTime bulletTime;


    float EnemyHealth = 100f;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();


    }

    // Update is called once per frame
    void Update()
    {
        score = System.Convert.ToInt32(txt.text);
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if (distance < enemyDistanceRun)
        {

            Vector3 dirToPlayer = transform.position - Player.transform.position;

            Vector3 newPos = transform.position - dirToPlayer;

            enemy.SetDestination(newPos);


            

            enemy.speed = 3.5f;

        }
        

    }




    public void takeDamage(float damage)
    {
        EnemyHealth -= damage;
        if (EnemyHealth <= 0)
        {
            FindObjectOfType<AudioManager>().Play("Explosion");
            Destroy(gameObject);
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

        score += 20;
        txt.text = string.Format("{0} ", score);

        if (score == 100)
        {
            bulletTime.bulletTime();

            StartCoroutine(your_timer2());

            GameManager.instance.DoThing();
        }

    }

}

