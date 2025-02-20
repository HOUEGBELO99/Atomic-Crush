using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppController : MonoBehaviour {

    public int maxLifes;

    private int currentLifes = 0;

    public event Action<int> TopScoreChanged;
    public event Action<int> GemsChanged;

    void Awake ()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Initialize life variable.
        currentLifes = maxLifes;
        LIFES = 3;

        // Start the MainMenu
        SceneManager.LoadScene(1);
    }

    public int GetRemainingLifes()
    {
        return LIFES;
    }

    public void ReduceLifes()
    {
        LIFES--;
    }

    public int BEST_SCORE{
        get => PlayerPrefs.GetInt(nameof(BEST_SCORE), 0);
        set{
            if(BEST_SCORE >= value) return;
            PlayerPrefs.SetInt(nameof(BEST_SCORE), value);
            TopScoreChanged?.Invoke(value);
        }
    }

     public int GEMS{
        get => PlayerPrefs.GetInt(nameof(GEMS), 0);
        set{
            PlayerPrefs.SetInt(nameof(GEMS), value);
            GemsChanged?.Invoke(value);
        }
    }

     public int LIFES{
        get => PlayerPrefs.GetInt(nameof(LIFES), currentLifes);
        set{
            PlayerPrefs.SetInt(nameof(LIFES), value);
            GemsChanged?.Invoke(value);
        }
    }

    public int ACTUAL_LEVEL{
        get => PlayerPrefs.GetInt(nameof(ACTUAL_LEVEL), 1);
        set{
            PlayerPrefs.SetInt(nameof(ACTUAL_LEVEL), value);
            GemsChanged?.Invoke(value);
        }
    }
}
