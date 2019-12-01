using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager2 : MonoBehaviour
{
    public static GameManager2 instance;
    public Text txt;
    public int score = 0;
    public PlayerController player;
    public BulletTime bulletTime;
    // Start is called before the first frame update
    void Start()
    {

        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance!=this)
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {



    }

    IEnumerator your_timer()
    {

        player.enabled = false;
        yield return new WaitForSeconds(1.5f);
        if (SceneManager.GetActiveScene().name == "Level1")
            SceneManager.LoadScene("Level2");
        if (SceneManager.GetActiveScene().name == "Level2")
            SceneManager.LoadScene("Level3");
        if (SceneManager.GetActiveScene().name == "Level3")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("YouWin");
        }
    }

    public void DoThing()
    {

        score += 20;
        txt.text = string.Format("{0} pts", score);

        if (score == 100)
        {
            bulletTime.bulletTime();
            StartCoroutine(your_timer());
            FindObjectOfType<AudioManager>().Play("YouWin");
        }

    }



}