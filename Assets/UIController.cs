using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{   
    public GameObject menu;

    public void Menu()
    {
        menu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}