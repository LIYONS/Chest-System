using System.Collections.Generic;
using UnityEngine;
using ChestSystem.Services;

namespace ChestSystem.Chest
{
    public class ChestSlotsController : MonoBehaviour
    {
        [SerializeField] private int numberOfSlots;
        [SerializeField] private List<Chests> chests;
        [SerializeField] private GameObject chestSlotPrefab;
        [SerializeField] private float timeToSkipFor1Gem;

        private List<ChestSlot> chestSlots = new();

        private void Start()
        {
            for (int i = 0; i < numberOfSlots; i++)
            {
                ChestSlotController chestSlotController=Instantiate(chestSlotPrefab, transform).GetComponent<ChestSlotController>();
                chestSlotController.ChestSlotID=chestSlotController.GetInstanceID();
                ChestSlot slot = new(chestSlotController.GetInstanceID(), chestSlotController);
                chestSlots.Add(slot);
            }
        }

        public void SpawnChest(ChestConfig config)
        {
            for (int i = 0; i < chestSlots.Count; i++)
            {
                if (chestSlots[i].chestSlotController.GetIsEmpty)
                {
                    GameObject chestPrefab = chests.Find(item => item.chestType == config.chestType).chestPrefab;
                    if (chestPrefab)
                    {
                        chestSlots[i].chestSlotController.SpawnChest(chestPrefab, config);
                    }
                    return;
                }
            }
            ChestService.Instance.ShowMessage(MsgPopupType.SlotsFull);
        }

        public void ShowUnlock(ChestUnlockMsg msgObject)
        {
            if(IsSlotBusy() && ChestService.Instance.CurrentUnlockingChestId!=msgObject.chestSlotId)
            {
                ChestService.Instance.ShowMessage(MsgPopupType.SlotsBusy);
                return;
            }
            ChestService.Instance.ShowNewUnlockPopup(msgObject);
        }
        public bool IsSlotBusy()
        {
            for (int i = 0; i < chestSlots.Count; i++)
            {
                if (chestSlots[i].chestSlotController.UnlockingStatus == true)
                {
                    return true;
                }
            }
            return false;
        }
        public float GetTimeToSkipFor1Gem { get { return timeToSkipFor1Gem; } }
    }
}
