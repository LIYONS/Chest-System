using ChestSystem.Chest;
using ChestSystem.Chest.SO;
using ChestSystem.Services;
using UnityEngine;

namespace ChestSystem
{
    public class ChestSpawner : MonoBehaviour
    {
        [SerializeField] private ChestConfiguration chestConfiguration;
        [SerializeField] private float timeToskipFor1Gem;
        private ChestSlotsController chestSlotsController;

        private void Start()
        {
            chestSlotsController = ChestService.Instance.GetChestSlotsController;
        }
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
        public float GetTimeToSkipFor1Gem { get { return timeToskipFor1Gem; } }
    }
}
