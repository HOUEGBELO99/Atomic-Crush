using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPannel : ShowHidable
{
    [SerializeField] Button pack1;
    [SerializeField] Button pack2;
    [SerializeField] Button pack3;
    [SerializeField] Button pack4;

    public AppController appInstance;
            public LevelController levelController;


    void Start()
    {
        appInstance = FindObjectOfType<AppController>();  
    }
    void Update()
    {
      CheckIfCanBuy();  
    }

    public void BuyPack(int packName){
    if(packName == 1){
        appInstance.GEMS -= 30;
        appInstance.LIFES += 2;
    }
     else if(packName == 2){
        appInstance.GEMS -= 40;
        appInstance.LIFES += 4;
    }
     else if(packName == 3){
        appInstance.GEMS -= 50;
        appInstance.LIFES += 6;
    }
     else if(packName == 4){
        appInstance.GEMS -= 60;
        appInstance.LIFES += 8;
    }
   }

   private void CheckIfCanBuy(){
    pack1.interactable = appInstance.GEMS >= 30;
    pack2.interactable = appInstance.GEMS >= 40;
    pack3.interactable = appInstance.GEMS >= 50;
    pack4.interactable = appInstance.GEMS >= 60;
   }

   public void Close(){
    Hide();
    levelController.GameOver();
   }
}
