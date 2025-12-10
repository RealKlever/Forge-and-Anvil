using System;
using game.furnace;
using player;
using store.gui;
using store.logic;
using UnityEngine;

namespace game
{
    public class GameLogic : MonoBehaviour
    {
        
        public static Player player;

        // public static Furnace furnace;

        public static bool balanceWasUpdated = false;
                
        private void Start()
        {
            Debug.Log("Start GameLogic");
            initializeGameLogicVars();
        }

        private void initializeGameLogicVars()
        {
            player = new Player();
            // furnace = new Furnace();


        }

        private void Update()
        {
            // throw new NotImplementedException();
        }
    }
}