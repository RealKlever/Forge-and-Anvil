using System;
using store.logic;
using UnityEngine;

namespace game
{
    public class GameLogic : MonoBehaviour
    {
        
        public static Store store;
        
        private void Start()
        {
            Debug.Log("Start GameLogic");
            initializeGameLogicVars();
        }

        private void initializeGameLogicVars()
        {
            store = new Store();



            store.initialize();
        }

        private void Update()
        {
            // throw new NotImplementedException();
        }
    }
}