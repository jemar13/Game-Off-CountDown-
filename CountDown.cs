using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 55f;
    public Text countDownText;
    bool playing = false;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }


    IEnumerator your_timer()
    {

        //player.enabled = false;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameOver");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;

        if ((currentTime <= 15 && !playing)&& (SceneManager.GetActiveScene().name == "Level1"))
        {
            FindObjectOfType<AudioManager>().Play("Clock");
            playing = true;
        }
        if ((currentTime+60 <= 15 && !playing) && (SceneManager.GetActiveScene().name == "Level2"))
        {
            FindObjectOfType<AudioManager>().Play("Clock");
            playing = true;
        }

        if ((currentTime-45 <= 9 && !playing) && (SceneManager.GetActiveScene().name == "Level3"))
        {
            FindObjectOfType<AudioManager>().Play("Clock");
            playing = true;
        }

        if (SceneManager.GetActiveScene().name == "Level1")
            countDownText.text ="Time: "+ currentTime.ToString("0");

        if (SceneManager.GetActiveScene().name == "Level2")
            countDownText.text = "Time: " + (currentTime+60f).ToString("0");

        if (SceneManager.GetActiveScene().name == "Level3")
            countDownText.text = "Time: " + (currentTime -45f).ToString("0");

        if ((currentTime<=0)&& (SceneManager.GetActiveScene().name == "Level1"))
        {
            currentTime = 0;
            StartCoroutine(your_timer());
           
        }
        if ((currentTime+60 <= 1)&& (SceneManager.GetActiveScene().name == "Level2"))
        {
            currentTime = 0;
            StartCoroutine(your_timer());

        }

        if ((currentTime - 45 <= 0) && (SceneManager.GetActiveScene().name == "Level3"))
        {
            currentTime = 0;
            StartCoroutine(your_timer());

        }
    }
}

