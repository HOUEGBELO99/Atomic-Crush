using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TimerEvents: MonoBehaviour
{
    public int timeLeft;

    private bool gameStarted;
    private bool initialExit;

    private Coroutine lastRoutine;

    void Start()
    {
        // For the bonus level we want the timer to start immediately.
        if (SceneManager.GetActiveScene().name == "Level2")
        {
            gameStarted = true;
            lastRoutine = StartCoroutine(Timer());
        }
        else
        {
            gameStarted = false;
        }
        initialExit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            LevelController controller = FindObjectOfType<LevelController>();
            if (controller == null) return;

            controller.SetTimerText("Temps restant: " + timeLeft);
            if (timeLeft <= 0)
            {
                gameStarted = false;
                StopCoroutine(lastRoutine);
                controller.SetTimerText("Temps épuisé!");
            }
        }
    }

    IEnumerator Timer()
    {
        while (gameStarted)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!initialExit && other.tag == "Player")
        {
            initialExit = true;

            LevelController controller = FindObjectOfType<LevelController>();
            controller.SetGuideText("Collecter les molécules pour résoudre l'exercice");
            // The time has come! Start the music.
            controller.PlayBackgroundMusic();

            gameStarted = true;
            lastRoutine = StartCoroutine(Timer());
        }
    }

    public void Stop()
    {
        gameStarted = false;
        StopCoroutine(lastRoutine);
    }
}
