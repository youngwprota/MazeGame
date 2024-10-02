using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class Clock : MonoBehaviour
{
    public float timeLeft = 0f; 
    public TMP_Text timerText; 
    public GameObject door; 
    private bool isTimerActive = true; 

    void Update()
    {
        if (isTimerActive && timeLeft >= 0)
        {
            timeLeft += Time.deltaTime; 
            DisplayTime(timeLeft);
            
        }
        else
        {
            timeLeft = 0;
            DisplayTime(timeLeft);
            
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered with: " + other.gameObject.name);
        if (other.gameObject == door) 
        {
            isTimerActive = false; 
            Debug.Log("Timer stopped");
        }
    }
}