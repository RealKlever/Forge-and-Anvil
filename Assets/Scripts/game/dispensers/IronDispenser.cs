using game.materials;
using store.gui;
using store.logic;
using UnityEngine;

namespace game.dispensers
{
    public class IronDispenser : MonoBehaviour
    {

        public GameObject ironPrefab;
        public GameObject ironDispenserObject;
        public StoreGUI storeGUI;

        private void spawnIron()
        {
            // UnityEditor.TransformWorldPlacementJSON:{"position":{"x":0.012000001966953278,"y":-0.036646999418735507,"z":-0.039000000804662707},"rotation":{"x":-0.835983157157898,"y":0.4121951162815094,"z":0.3461400270462036,"w":-0.10683891922235489},"scale":{"x":0.23132000863552094,"y":0.23132000863552094,"z":0.23132000863552094}}
            GameObject newIron = Instantiate(ironPrefab, ironDispenserObject.transform);
            newIron.GetComponent<IronOre>().ironDispenser = this;
            newIron.GetComponent<IronOre>().recentlySpawned = true;
            newIron.GetComponent<IronOre>().storeGUI = storeGUI;
            newIron.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            // , new Vector3(0.012f, -0.036f, -0.039f), new Quaternion(-0.836f, 0.412f, 0.346f, -0.107f)
        }

        public void trySpawnIron()
        {
            foreach (Transform child in ironDispenserObject.transform)
            {
                if (child.CompareTag("ironOre"))
                {
                    return;
                }
            }
            if (GameLogic.player.getInventory().getItems().TryGetValue(Item.IRON, out int amount) && amount > 0)
            {
                spawnIron();
            }
        }

    }
}