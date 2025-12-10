using game.materials;
using store.gui;
using store.logic;
using UnityEngine;

namespace game.dispensers
{
    public class HandleDispenser : MonoBehaviour
    {

        public GameObject handlePrefab;
        public GameObject handleDispenserObject;
        public StoreGUI storeGUI;

        private void spawnHandle()
        {
            // UnityEditor.TransformWorldPlacementJSON:{"position":{"x":0.012000001966953278,"y":-0.036646999418735507,"z":-0.039000000804662707},"rotation":{"x":-0.835983157157898,"y":0.4121951162815094,"z":0.3461400270462036,"w":-0.10683891922235489},"scale":{"x":0.23132000863552094,"y":0.23132000863552094,"z":0.23132000863552094}}
            GameObject newHandle = Instantiate(handlePrefab, handleDispenserObject.transform);
            newHandle.GetComponent<Handle>().handleDispenser = this;
            newHandle.GetComponent<Handle>().recentlySpawned = true;
            newHandle.GetComponent<Handle>().storeGUI = storeGUI;
            newHandle.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            // , new Vector3(0.012f, -0.036f, -0.039f), new Quaternion(-0.836f, 0.412f, 0.346f, -0.107f)
        }

        public void trySpawnHandle()
        {
            foreach (Transform child in handleDispenserObject.transform)
            {
                if (child.CompareTag("handle"))
                {
                    return;
                }
            }
            if (GameLogic.player.getInventory().getItems().TryGetValue(Item.HANDLE, out int amount) && amount > 0)
            {
                spawnHandle();
            }
        }

    }
}