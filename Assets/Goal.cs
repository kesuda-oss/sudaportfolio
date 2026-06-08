using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Goal : MonoBehaviour
{   
    public GameController gameController;
    public GoalMessage goalMessage;
    public float restartDelay = 2f;

    private bool isGoal = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isGoal) return;

        if (other.CompareTag("Player"))
        {
            isGoal = true;
            goalMessage.Show();
            other.GetComponent<PlayerController>().enabled = false;
            GameController.instance.Goal();
            StartCoroutine(Restart());
            
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
}