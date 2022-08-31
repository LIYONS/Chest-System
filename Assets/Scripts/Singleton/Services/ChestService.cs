using UnityEngine;
using ChestSystem.Chest;
using Singleton;

namespace ChestSystem.Services
{
    public class ChestService : MonoSingletonGeneric<ChestService>
    {
        [SerializeField] private ChestConfiguration chestConfiguration;
        [SerializeField] private ChestSlotsController chestSlotsController;

        public void SpawnChest(ChestType chestType)
        {
            ChestConfig config = chestConfiguration.ChestList.Find(item => item.chestType == chestType);
            if (chestSlotsController)
            {
                chestSlotsController.SpawnChest(config);
            }
        }

        public void SpawnRandomChest()
        {
            int index = Random.Range(0, chestConfiguration.ChestList.Count);
            if (chestSlotsController)
            {
                chestSlotsController.SpawnChest(chestConfiguration.ChestList[index]);
            }

        }
    }
}
