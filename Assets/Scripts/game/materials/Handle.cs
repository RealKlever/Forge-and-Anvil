using System;
using game.dispensers;
using store.gui;
using store.logic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace game.materials
{
    public class Handle : MonoBehaviour
    {

        public bool recentlySpawned = false;
        public HandleDispenser handleDispenser;
        public StoreGUI storeGUI;
        public Rigidbody rigidbody;

        public void grabHandle(SelectEnterEventArgs args)
        {
            if (recentlySpawned)
            {
                recentlySpawned = false;
                rigidbody.constraints = RigidbodyConstraints.None;
                GameLogic.player.getInventory().removeItem(Item.HANDLE);
                storeGUI.updateAmount(Item.HANDLE);
                handleDispenser.trySpawnHandle();
                Debug.Log("Try spawning new handle from previous handle");
            }
        }
    }
}