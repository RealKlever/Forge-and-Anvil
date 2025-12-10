using System;
using game.dispensers;
using store.gui;
using store.logic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace game.materials
{
    public class Coal : MonoBehaviour
    {

        public bool recentlySpawned = false;
        public CoalDispenser coalDispenser;
        public StoreGUI storeGUI;
        public Rigidbody rigidbody;

        public void spawnNewCoal(SelectExitEventArgs args)
        {
            // if (recentlySpawned)
            // {
            //     recentlySpawned = false;
            //     coalDispenser.trySpawnCoal();
            //     Debug.Log("Try spawning new coal from previous coal");
            // }
        }

        public void grabCoal(SelectEnterEventArgs args)
        {
            if (recentlySpawned)
            {
                recentlySpawned = false;
                rigidbody.constraints = RigidbodyConstraints.None;
                GameLogic.player.getInventory().removeItem(Item.COAL);
                storeGUI.updateAmount(Item.COAL);
                coalDispenser.trySpawnCoal();
                Debug.Log("Try spawning new coal from previous coal");
            }
        }
    }
}