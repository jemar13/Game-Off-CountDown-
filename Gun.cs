
using UnityEngine;
using UnityEngine.SceneManagement;
public class Gun : MonoBehaviour
{
    public float range = 50f;
    public float damage = 15f;
    public ParticleSystem Splosion;
    public GameObject impact; 
    public Camera cam;
    public Ammo ammo;

    
    void Update()
    {
        //Ammo ammo = amm.GetComponent<Ammo>();
        if (Input.GetButtonDown("Fire1") && ammo.getCurrentAmmo()>0)
        {
            if(SceneManager.GetActiveScene().name == "Level1")
                FindObjectOfType<AudioManager>().Play("Pistol");
            if (SceneManager.GetActiveScene().name == "Level2")
                FindObjectOfType<AudioManager>().Play("Heavy");
            if (SceneManager.GetActiveScene().name == "Level3")
                FindObjectOfType<AudioManager>().Play("Rifle");
            Shoot();
            ammo.decreaseAmmo();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        
        
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            
            Splosion.Play();
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            RobotEnemy robot= hit.transform.GetComponent<RobotEnemy>();
            StaticRobot sRobot = hit.transform.GetComponent<StaticRobot>();
            if (enemy!=null)
            {
                enemy.takeDamage(damage);
            }
            if (robot!= null)
            {
                robot.takeDamage(damage);
            }
            if (sRobot != null)
            {
                sRobot.takeDamage(damage);
            }
            Debug.Log(hit.transform.name);

            Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
           

        }


    }
}

