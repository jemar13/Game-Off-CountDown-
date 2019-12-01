using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ammo : MonoBehaviour
{
    public Image image;
    int min=0;
    int max=100;
    private int currentValue;
    private float currentPercent;
    private int currentAmmo = 50;
    bool destroyAmmo;

  

    public void set_ammo(int ammo)
    {
        if (ammo != currentValue)
        {
            if (max - min == 0)
            {
                currentValue = 0;
                currentPercent = 0;
            }
            else
            {
                currentValue = currentAmmo;
                currentPercent = (float)currentValue / (float)(max - min);
            }

            image.fillAmount = currentPercent;

        }
    }

    public float CurrentPercent
    {
        get { return currentPercent; }
    }

    public int getCurrentAmmo()
    {
      return currentAmmo;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ammo")
        {
            FindObjectOfType<AudioManager>().Play("Ammo");
            increaseAmmo();
            Destroy(other.gameObject);
        }

    }
    public void increaseAmmo()
    {
        if (currentAmmo <= 80)
        {
            currentAmmo += 40;
            set_ammo(currentAmmo);
        }
       
        

    }

    public void decreaseAmmo()
    {
        currentAmmo -= 10;
        set_ammo(currentAmmo);
    }

}
