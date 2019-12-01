using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StaticRobot : MonoBehaviour
{

    float EnemyHealth = 100f;
    public Text txt;
    int score;
    public PlayerController player;
    public BulletTime bulletTime;
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score = System.Convert.ToInt32(txt.text);
        transform.Rotate(new Vector3(0, Time.deltaTime*50, 0));
       
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
