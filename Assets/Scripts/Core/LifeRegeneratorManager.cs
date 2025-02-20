using System;
using System.Collections;
using UnityEngine;

public class LifeRegeneratorManager : MonoBehaviour
{
    public int maxHealth = 3;
    public float regenerationTimeInMinutes = 30f;
    
    private int currentHealth;
    private DateTime lastDeathTime;
    private bool isRegenerating = false;

    private const string LAST_LOSE_TIME_KEY = "LastDeathTime";

    public AppController appController;

    public static LifeRegeneratorManager Instance;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    void Start()
    {
        appController = FindObjectOfType<AppController>();
        LoadGameState();
        CheckForRegeneration();
    }

    void LoadGameState()
    {
        currentHealth = appController.LIFES;
        
        string savedTime = PlayerPrefs.GetString(LAST_LOSE_TIME_KEY, "");
        if (!string.IsNullOrEmpty(savedTime))
        {
            if (DateTime.TryParse(savedTime, out DateTime parsedTime))
            {
                lastDeathTime = parsedTime;
            }
            else
            {
                lastDeathTime = DateTime.Now; // Réinitialisation en cas de corruption des données
                PlayerPrefs.SetString(LAST_LOSE_TIME_KEY, lastDeathTime.ToString());
            }
        }
    }

    void SaveGameState()
    {
        if (currentHealth <= 0)
        {
            lastDeathTime = DateTime.Now;
            PlayerPrefs.SetString(LAST_LOSE_TIME_KEY, lastDeathTime.ToString());
        }
        PlayerPrefs.Save();
    }

    void CheckForRegeneration()
    {
        if (currentHealth <= 0 && !isRegenerating)
        {
            if (lastDeathTime != default(DateTime))
            {
                TimeSpan timeSinceDeath = DateTime.Now - lastDeathTime;
                if (timeSinceDeath.TotalMinutes >= regenerationTimeInMinutes)
                {
                    RegenerateHealth();
                }
                else
                {
                    StartCoroutine(WaitForRegeneration(timeSinceDeath));
                }
            }
        }
    }

    IEnumerator WaitForRegeneration(TimeSpan timeSinceDeath)
    {
        isRegenerating = true;
        float remainingTime = (float)(regenerationTimeInMinutes - timeSinceDeath.TotalMinutes);
        
        while (remainingTime > 0)
        {
            Debug.Log($"Régénération dans {remainingTime:F1} minutes");
            yield return new WaitForSeconds(60f); // Attendre 1 minute
            remainingTime--;
        }

        RegenerateHealth();
        isRegenerating = false;
    }

    void RegenerateHealth()
    {
        appController.LIFES = maxHealth;
        lastDeathTime = DateTime.MinValue; // Réinitialisation après régénération
        PlayerPrefs.DeleteKey(LAST_LOSE_TIME_KEY); // Supprime la sauvegarde pour éviter des erreurs
        PlayerPrefs.Save();
        Debug.Log("Vie régénérée !");
    }

    public string GetRemainingRegenerationTime()
    {    
        if (lastDeathTime == DateTime.MinValue)
        {
            return "00:00:00"; // Évite les erreurs si aucune mort n'a été enregistrée
        }
        
        TimeSpan timeSinceDeath = DateTime.Now - lastDeathTime;
        TimeSpan remainingTime = TimeSpan.FromMinutes(regenerationTimeInMinutes) - timeSinceDeath;
            Debug.Log(remainingTime.TotalSeconds);
        if (remainingTime.TotalSeconds <= 0) return "00:00:00";
        
        return string.Format("{0:00}:{1:00}:{2:00}", 
            remainingTime.Hours, 
            remainingTime.Minutes, 
            remainingTime.Seconds);
    }
}
