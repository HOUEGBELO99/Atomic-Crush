using System;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

    public Text rightText;
    public Text leftText;
    public Text restartText;
    public Text gameOverText;
    public Text collisionText;
    public Text repositoryText;
    public Text successText;
    public Text gatherHintText;
    public Text lifesText;
    public Text gemsText;

    [HideInInspector]public bool gameOver;
     [HideInInspector]public bool restart;
     [HideInInspector]public bool hasWon;
    public AppController appInstance;


    private Dictionary<string, int> repo;

    void Start() {

        SetDefault();
    }

    void Update() {


    }

    public void CollectMols(){
        if(!gameOver){

             // Gather stuff.
            CollectNearbyMols();
            // Check for completion only after a valid keystroke to avoid useless checks.
            checkForCompletion();
        }
       
    }

    public void UpdateColliderTag(String colTag)
    {
        collisionText.text = colTag;
    }

    public void SetTimerText(string newValue)
    {
        rightText.text = newValue;
        if (newValue == "Temps épuisé!")
        {
            LevelTimedOut();
        }
    }

    public void PlayBackgroundMusic()
    {
        GetComponent<AudioSource>().Play();
    }

    public void StopBackgroundMusic()
    {
        GetComponent<AudioSource>().Stop();
    }

    public void SetGuideText(string newValue)
    {
        leftText.text = newValue;
    }

    public void GameOver()
    {
        Debug.Log("Game over");
        // Make it false to ensure that only one event will be handled.
        gameOver = false;

        StopBackgroundMusic();

        SceneManager.LoadScene(1);
    }

    public void Restart()
    {
        Debug.Log("Restarting.");
        // Make it false to ensure that only one event will be handled.
        restart = false;

        StopBackgroundMusic();
        
        // Restart the scene.
        SceneManager.LoadScene(appInstance.ACTUAL_LEVEL +1);
    }

    public void NextLevel()
    {
        Debug.Log("Loading next level.");
        AppController appInstance = FindObjectOfType<AppController>();
        int currentBuildIndex = appInstance.ACTUAL_LEVEL +1;
        // Ignore space keystrokes on level 3.
        if (currentBuildIndex == 10) return;

        // Load the next one.
        int next = currentBuildIndex + 1;
        SceneManager.LoadScene(next);
        appInstance.ACTUAL_LEVEL ++;
    }

    private void CollectNearbyMols()
    {
        var playerObject = GameObject.Find("Player");
        playerObject.GetComponent<AudioSource>().Play(); 
        Taptic.Light();
        Collider[] hitColliders = Physics.OverlapSphere(playerObject.transform.position, 2);
        foreach (Collider col in hitColliders)
        {
            string colTag = col.gameObject.tag;
            if (colTag == "Hydrogen" || colTag == "Oxygen" || colTag == "Calcium" || colTag == "Carbon"|| colTag == "Zinc" || colTag == "Chlorine")
            {
                UpdateRepository(col.gameObject);
            }
        }
    }

    private void SetDefault()
    {
        gameOver = false;
        restart = false;
        hasWon = false;

        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 2:
                leftText.text = "Quitter la zone bleu pour commencer le jeu...";
                rightText.text = "...mais avant résolver l'équation chimique avant d'y aller!";
                break;
            default:
                break;
        }
        restartText.text = "";
        gameOverText.text = "";
        successText.text = "";
        collisionText.text = "";
        repositoryText.text = "Aucune molecules collectées.";
        gatherHintText.text = "Appuyer le bouton Collecter pour attrapper!";
        appInstance = FindObjectOfType<AppController>();
        lifesText.text = "Vies: " + appInstance.GetRemainingLifes();
        gemsText.text = "Gems: " + appInstance.GEMS;

        repo = new Dictionary<string, int>();
    }

    private void LevelTimedOut()
    {
        if (hasWon) return;

        AppController appInstance = FindObjectOfType<AppController>();
        if (appInstance.GetRemainingLifes() == 1)
        {
            Debug.Log("Game over");
            gameOverText.text = "";
            //Invoke("GameOver", 3f);
            UIManager.Instance.GameOverPannel.Show();
        }
        else
        {
            appInstance.ReduceLifes();
            restartText.text = "";
            UIManager.Instance.RevivalPannel.Show();
            restart = true;
        }
    }

    private void UpdateRepository(GameObject molecule)
    {
        if (repo.ContainsKey(molecule.tag))
        {
            repo[molecule.tag]++;
        }
        else
        {
            repo.Add(molecule.tag, 1);
        }
        // First clean the previous text.
        repositoryText.text = "";
        foreach (KeyValuePair<string, int> entry in repo)
        {
            repositoryText.text += entry.Value + " " + entry.Key + " ";
        }
        Destroy(molecule);
    }

    private void checkForCompletion()
    {
        if (restart || gameOver) return;

        switch(SceneManager.GetActiveScene().name)
        {
            case "Level1":
                // The answer is 1 Ca / 1 O
                int rightItems = 0;
                foreach (KeyValuePair<string, int> entry in repo)
                {
                    
                    if (entry.Key == "Calcium" && entry.Value == 1)
                    {
                        rightItems++;
                    }
                     if (entry.Key == "Oxygen" && entry.Value == 1)
                    {
                        rightItems++;
                    }
                }
                if (rightItems == 2)
                {
                    hasWon = true;
                    successText.text = "";
                    TimerEvents timer = FindObjectOfType<TimerEvents>();
                    UIManager.Instance.CollectCoinsPanel.Show();
                    timer.Stop();
                }
                break;
            case "Level2":
                // The answer is 1 C / 4 O / 4 H
                int rightItems2 = 0;
                foreach (KeyValuePair<string, int> entry in repo)
                {
                    if (entry.Key == "Carbon" && entry.Value == 1)
                    {
                        rightItems2++;
                    }
                    if (entry.Key == "Oxygen" && entry.Value == 4)
                    {
                        rightItems2++;
                    }
                    if (entry.Key == "Hydrogen" && entry.Value == 4)
                    {
                        rightItems2++;
                    }
                }
                if (rightItems2 == 3)
                {
                    hasWon = true;
                    successText.text = "";
                    TimerEvents timer = FindObjectOfType<TimerEvents>();
                    timer.Stop();
                    UIManager.Instance.CollectCoinsPanel.Show();
                }
                break;
            case "Level3":
                // The answer is 4 Fe / O 6
                int rightItems3 = 0;
                foreach (KeyValuePair<string, int> entry in repo)
                {
                    if (entry.Key == "Oxygen" && entry.Value == 6)
                    {
                        rightItems3++;
                    }
                    if (entry.Key == "Fer" && entry.Value == 4)
                    {
                        rightItems3++;
                    }
                }
                if (rightItems3 == 2)
                {
                    hasWon = true;
                    successText.text = "";
                    TimerEvents timer = FindObjectOfType<TimerEvents>();
                    UIManager.Instance.CollectCoinsPanel.Show();
                    timer.Stop();
                }
                break;
            case "Level4":
                // The answer is 1 Na / 1 Cl / 2 H / 1 O
                int rightItems4 = 0;
                foreach (KeyValuePair<string, int> entry in repo)
                {
                    if (entry.Key == "Oxygen" && entry.Value == 1)
                    {
                        rightItems4++;
                    }
                    if (entry.Key == "Sodium" && entry.Value == 1)
                    {
                        rightItems4++;
                    }
                    if (entry.Key == "Clorine" && entry.Value == 1)
                    {
                        rightItems4++;
                    }
                    if (entry.Key == "Hydrogen" && entry.Value == 1)
                    {
                        rightItems4++;
                    }
                }
                if (rightItems4 == 4)
                {
                    hasWon = true;
                    successText.text = "";
                    TimerEvents timer = FindObjectOfType<TimerEvents>();
                    UIManager.Instance.CollectCoinsPanel.Show();
                    timer.Stop();
                }
                break;
            case "Level5":
                // The answer is 1 S / 1 O
                int rightItems5 = 0;
                foreach (KeyValuePair<string, int> entry in repo)
                {
                    if (entry.Key == "Oxygen" && entry.Value == 1)
                    {
                        rightItems5++;
                    }
                    if (entry.Key == "Soufre" && entry.Value == 1)
                    {
                        rightItems5++;
                    }
                   
                }
                if (rightItems5 == 2)
                {
                    hasWon = true;
                    successText.text = "";
                    TimerEvents timer = FindObjectOfType<TimerEvents>();
                    UIManager.Instance.CollectCoinsPanel.Show();
                    timer.Stop();
                }
                break;
            default:
                break;
        }
    }
}
