using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class swichScenes : MonoBehaviour
{
    
    public void GoToTitle2()
    {
        
        SceneManager.LoadScene("TitleScreen 1");
        

    }

    public void GoToTitle3()
    {
        SceneManager.LoadScene("TitleScreen 2");

    }
    public void GotoLevel1()
    {
        SceneManager.LoadScene("Level1");

    }

    public void GoToLevel2()
    {
        SceneManager.LoadScene("Level2");

    }

    public void GoToLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void GoToLevel4()
    {
        SceneManager.LoadScene("Level4");
    }


    public void GoToGameOver()
    {
        SceneManager.LoadScene("Home");
    }

}