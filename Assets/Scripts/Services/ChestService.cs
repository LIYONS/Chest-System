using UnityEngine;
using ChestSystem.Chest;
using Singleton;
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

        public void SpawnChest()
        {
            chestSpawner.SpawnChest();
        }
        public void ShowNewUnlockPopup(ChestUnlockMsg msgObject)
        {
            popUpService.QueuePopup(msgObject);
        }

        public void OnChestUnlocked(ChestUnlockedMsg msgObject)
        {
            Message msg = new(msgObject.title, msgObject.description);
            ShowMessage(msg);
            _ = AddGemCount(msgObject.gems);
            _ = AddCoinCount(msgObject.coins);
        }

        public void ShowMessage(Message message)
        {
            popUpService.QueuePopup(message);
        }

        public void ShowMessage(MsgPopupType msgType)
        {
            popUpService.QueuePopup(msgType);
        }

        public bool ReduceGemCount(int amount)
        {
            if(uiService.ReduceGemCount(amount))
            {
                return true;
            }
            ShowMessage(MsgPopupType.NotEnoughGems);
            return false;
        }
        public bool ReduceCoinCount(int amount)
        {
            if(uiService.ReduceCoinCount(amount))
            {
                return true;
            }
            ShowMessage(MsgPopupType.NotEnoughCoins);
            return false;
        }
        public bool AddCoinCount(int amount)
        {
            if(uiService.AddCoinCount(amount))
            {
                return true;
            }
            ShowMessage(MsgPopupType.CoinStorageFull);
            return false;
        }

        public bool AddGemCount(int amount)
        {
            if (uiService.AddGemCount(amount))
            {
                return true;
            }
            ShowMessage(MsgPopupType.GemStorageFull);
            return false;
        }
        public ChestSlotsController GetChestSlotsController { get { return chestSlotsController; } }

        public float GetTimeToSkipFor1Gem { get { return chestSpawner.GetTimeToSkipFor1Gem; } }


        public bool IsSlotBusy { get { return chestSlotsController.IsSlotBusy(); } }

        public int CurrentUnlockingChestId { get; set; }
    }
}
