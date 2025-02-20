using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneOnClick : MonoBehaviour {

	[SerializeField] Text PlayText;
	public AppController appController;


    void Start()
    {
		appController = FindObjectOfType<AppController>();

        PlayText.text = "COMMENCER";
    }

    public void LoadByIndex() 
	{
		SceneManager.LoadScene(appController.ACTUAL_LEVEL +1);
	} 
}
