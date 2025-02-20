using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class GameOverPannel : ShowHidable
{
    [SerializeField] GameObject BackBtn;
    [SerializeField] GameObject ShopBtn;

        public LevelController levelController;


    public  AppController appInstance;

    void Start()
    {
        appInstance = FindObjectOfType<AppController>();
    }
    void Update()
    {
        CheckCanOpenShop();
    }
    public void OpenShop(){
        UIManager.Instance.ShopPannel.Show();
        UIManager.Instance.GameOverPannel.Hide();
    }
    public void Back(){
          levelController.GameOver();
    }

    private void  CheckCanOpenShop(){
        BackBtn.SetActive(appInstance.GEMS < 0);
        ShopBtn.SetActive(appInstance.GEMS > 0);
    }
}
