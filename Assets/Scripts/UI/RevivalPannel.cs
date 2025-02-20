using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevivalPannel : ShowHidable
{
        public LevelController levelController;

    public void Restart(){
         levelController.Restart();
    }

    public void Back(){
          levelController.GameOver();
    }
}
