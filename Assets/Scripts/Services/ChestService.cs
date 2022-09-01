using UnityEngine;
using ChestSystem.Chest;
using Singleton;
using ChestSystem.Chest.SO;
using ChestSystem.UI;

namespace ChestSystem.Services
{
    public class ChestService : MonoSingletonGeneric<ChestService>
    {
        [SerializeField] private ChestSpawner chestSpawner;
        [SerializeField] private ChestSlotsController chestSlotsController;

        private PopupService popUpService;
        private UiService uiService;

        private void Start()
        {
            popUpService = PopupService.Instance;
            uiService = UiService.Instance;
        }
        public void SpawnChest(ChestType chestType)
        {
            chestSpawner.SpawnChest(chestType);
        }

        public void SpawnRandomChest()
        {
            chestSpawner.SpawnRandomChest();
        }
        public void ShowNewUnlockPopup(ChestUnlockMsg msgObject)
        {
            popUpService.ShowNewUnlockPopup(msgObject);
        }
        public void OnChestUnlocked(ChestUnlockedMsg msgObject)
        {
            Message msg = new(msgObject.title, msgObject.description);
            popUpService.OnChestUnlocked();
            popUpService.ShowMessage(msg);
            uiService.AddGemCount(msgObject.gems);
            uiService.AddCoinCount(msgObject.coins);

        }
        public void ShowMessage(Message message)
        {
            popUpService.ShowMessage(message);
        }
        public ChestSlotsController GetChestSlotsController { get { return chestSlotsController; } }

        public float GetTimeToSkipFor1Gem { get { return chestSpawner.GetTimeToSkipFor1Gem; } }
    }
}
