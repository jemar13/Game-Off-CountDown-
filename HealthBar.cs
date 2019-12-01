using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
public class HealthBar : MonoBehaviour
{
    public Image image;
    public Text txt;
    public  int min;
    public  int max;
    private int currentValue;
    private float currentPercent;
    public int damage =100;
    public Image damageImage;
    float flashSpeed = 0.5f;                               // The speed the damageImage will fade at.
    Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    private bool enter = false;
    public cameraShake shake;
    public BulletTime bulletTime;
    PlayerController playerMovement;
    bool dam;

    void Awake()
    {
        playerMovement = GetComponent<PlayerController>();
    }

    void Update()
    {
      
            // If the player has just been damaged...
            if (dam)
            {
                // ... set the colour of the damageImage to the flash colour.
                damageImage.color = flashColour;
                StartCoroutine(shake.Shake(.15f, .4f));
            if (currentPercent * 100 <= 40)
                FindObjectOfType<AudioManager>().Play("heartBeating");
            

            }
            // Otherwise...
            else
            {
                // ... transition the colour back to clear.
                if (currentPercent * 100> 40)
                damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }

            // Reset the damaged flag.
            dam = false;

        
            if (currentPercent * 100> 40)
            FindObjectOfType<AudioManager>().Pause("heartBeating");

          

    }

   
    public void set_health(int health)
    {
        if(health != currentValue)
        {
            if(max - min == 0)
            {
                currentValue = 0;
                currentPercent= 0;
            }
            else
            {

                currentValue = health;
                currentPercent = (float)currentValue / (float)(max - min);
                
            }

            txt.text = string.Format("{0} %", Mathf.RoundToInt(currentPercent * 100));
            image.fillAmount = currentPercent;
          


        }
    }
    public float CurrentPercent 
    {
        get { return currentPercent; }
    }

    public int CurrentValue
    {
        get { return currentValue; }
    }


    IEnumerator your_timer()
    {
        enter = true;
        playerMovement.enabled = false;
        yield return new WaitForSeconds(0.5f);
        
        SceneManager.LoadScene("GameOver");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        enter = false;
    }



    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "health")
        {
            increaseHealth();
            FindObjectOfType<AudioManager>().Play("Health");
        }
        else if (other.tag == "enemy" && damage - 25 > 0)
            decreaseHealth();
    }
    public void increaseHealth()
    {
        
        if (damage <= 80)
        {
            damage += 20;
           
            set_health(damage);
        }
        else
        {
            damage = 100;
            set_health(damage);
        }
        
    }


    void decreaseHealth()
    {
        dam = true;
        if(damage - 25 <= 0)
        {

            set_health(0);

            bulletTime.bulletTime();

            FindObjectOfType<AudioManager>().Play("Death");
            StartCoroutine(your_timer());
            


        }
        else
        {

            FindObjectOfType<AudioManager>().Play("Hit");

            damage -= 20;

            set_health(damage);
        }
    
    }

    public void decreaseHealthForShoot()
    {

        dam = true;
        if (damage - 25 <= 0) 
        {
            
            set_health(0);

            bulletTime.bulletTime();
            FindObjectOfType<AudioManager>().Play("Death");
            StartCoroutine(your_timer());
            

        }
        

        FindObjectOfType<AudioManager>().Play("Hit");

        if (damage < 25)
            damage = 0;
        else
        damage -= 25;

        set_health(damage);

    }
}
