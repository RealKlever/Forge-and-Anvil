using System;
using game.dispensers;
using store.gui;
using store.logic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace game.materials
{
    public class IronOre : MonoBehaviour
    {

        public bool recentlySpawned = false;
        public IronDispenser ironDispenser;
        public StoreGUI storeGUI;
        public Rigidbody rigidbody;

        public void grabIron(SelectEnterEventArgs args)
        {
            if (recentlySpawned)
            {
                recentlySpawned = false;
                rigidbody.constraints = RigidbodyConstraints.None;
                GameLogic.player.getInventory().removeItem(Item.IRON);
                storeGUI.updateAmount(Item.IRON);
                ironDispenser.trySpawnIron();
                Debug.Log("Try spawning new iron ore from previous iron ore");
            }
        }
    }
}