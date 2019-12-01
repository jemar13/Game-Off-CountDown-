using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public float range = 100f;
    public ParticleSystem Splosion;
    public Camera cam;
    public float fireRate = 1f;
    public GameObject player; 
    private float nextShoot=5f;
    private float enemyDistanceRun = 60f;
    RaycastHit hit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range)&& Time.time>=nextShoot&& distance < enemyDistanceRun)
        {
            nextShoot = Time.time + 1f / fireRate;
            Shoot();
           
        }
    }

    void Shoot()
    {
        HealthBar player = hit.transform.GetComponent<HealthBar>();
        if (player != null)
        {
            
            FindObjectOfType<AudioManager>().Play("Shoot");
            Splosion.Play();
            player.decreaseHealthForShoot();
        }

    }
    }
