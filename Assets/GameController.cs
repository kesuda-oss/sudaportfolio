using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private float elapsedTime;
    private bool isTimerRunning;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI bestTimeText;

    private float bestTime;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //PlayerPrefs.DeleteAll(); 
        bestTime = PlayerPrefs.GetFloat("BestTime", 999f);
        ResetTimer();
        ShowResult();
       
        
    }

    void Update()
    {
        if (isTimerRunning && timeText != null)
        {
            elapsedTime += Time.deltaTime;
            timeText.text = "Time: " + elapsedTime.ToString("F2");
        }

        // ★ Rキーでリスタート
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    void UpdateTimeUI()
    {
        if (timeText != null)
        {
            timeText.text = "Time: " + elapsedTime.ToString("F2");
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Goal()
    {
        isTimerRunning = false;

        // ★ ハイスコア更新
        if (elapsedTime < bestTime)
        {
            bestTime = elapsedTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
            PlayerPrefs.Save();
        }

        ShowResult();
    }

    void ShowResult()
    {
        if (bestTimeText != null)
            bestTimeText.text = "Best Time: " + bestTime.ToString("F2") + "s";
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        isTimerRunning = true;
        UpdateTimeUI();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            Transform t1 = canvas.transform.Find("TimeText");
            if (t1 != null) timeText = t1.GetComponent<TextMeshProUGUI>();

            Transform t2 = canvas.transform.Find("BestTimeText");
            if (t2 != null) bestTimeText = t2.GetComponent<TextMeshProUGUI>();
        }

        ShowResult(); // ← これ超大事
    }

}
