using System;
using player;
using store.gui;
using store.logic;
using UnityEngine;

namespace game
{
    public class GameLogic : MonoBehaviour
    {
        
        public static Player player;

        public static bool balanceWasUpdated = false;
                
        private void Start()
        {
            Debug.Log("Start GameLogic");
            initializeGameLogicVars();
        }

        private void initializeGameLogicVars()
        {
            player = new Player();


        }

        private void Update()
        {
            // throw new NotImplementedException();
        }
    }
}