using System.Collections;
using UnityEngine;

namespace Game
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private CollectCoinsPanel _collectCoinsPanel;
        [SerializeField] private RevivalPannel _revivalPannel;
        [SerializeField] private GameOverPannel _gameOverPannel;
        [SerializeField] private ShopPannel _shopPannel;
      

       
        public CollectCoinsPanel CollectCoinsPanel => Instance._collectCoinsPanel;
        public RevivalPannel RevivalPannel => Instance._revivalPannel;
        public GameOverPannel GameOverPannel => Instance._gameOverPannel;
        public ShopPannel ShopPannel => Instance._shopPannel;
       
        public static UIManager Instance { get; private set; }


        private void Awake()
        {
            Instance = this;

        }



    
    }
}