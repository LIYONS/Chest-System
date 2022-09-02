using System.Collections.Generic;
using UnityEngine;
using ChestSystem.Services;
using System;

namespace ChestSystem.Chest
{
    public class ChestSlotsController : MonoBehaviour
    {
        [SerializeField] private int numberOfSlots;
        [SerializeField] private List<Chests> chests;
        [SerializeField] private GameObject chestSlotPrefab;
        [SerializeField] private float timeToSkipFor1Gem;
        [SerializeField] private int unlockQueueSize=2;
        [SerializeField] private string closeBtnTxt="Close";
        [SerializeField] private string startTimerTxt="Start Timer";
        [SerializeField] private string queueText="Queue Opening";

        private ChestService chestService;
        private List<Tuple<int,Action>> unlockList=new();
        private List<ChestSlot> chestSlots = new();

        private void Start()
        {
            for (int i = 0; i < numberOfSlots; i++)
            {
                chestService = ChestService.Instance;
                ChestSlotController chestSlotController=Instantiate(chestSlotPrefab, transform).GetComponent<ChestSlotController>();
                chestSlotController.ChestSlotID=chestSlotController.GetInstanceID();
                ChestSlot slot = new(chestSlotController.GetInstanceID(), chestSlotController);
                chestSlots.Add(slot);
            }
        }

        private void Update()
        {
            if(!IsSlotBusy() && unlockList.Count!=0)
            {
                var queuedElement = unlockList[0];
                unlockList.RemoveAt(0);
                queuedElement.Item2.Invoke(); ;
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
            chestService.ShowMessage(MsgPopupType.SlotsFull);
        }

        public void ShowUnlock(ChestUnlockMsg msgObject)
        {
            if (IsSlotBusy())
            {
                if(chestService.CurrentUnlockingChestId != msgObject.chestSlotId &&unlockList.Count<unlockQueueSize && !IsInQueue(msgObject.chestSlotId))
                {
                    msgObject.btn1Txt = queueText;
                    Action action = msgObject.btn1Action;
                    msgObject.btn1Action = new Action(() => QueueUnlockingAction(msgObject.chestSlotId,action));
                }
                else if(chestService.CurrentUnlockingChestId == msgObject.chestSlotId || IsInQueue(msgObject.chestSlotId))
                {
                    msgObject.btn1Txt = closeBtnTxt;
                    msgObject.btn1Action = null;
                }
                else if(chestService.CurrentUnlockingChestId != msgObject.chestSlotId && unlockList.Count >= unlockQueueSize)
                {
                    chestService.ShowMessage(MsgPopupType.SlotsBusy);
                    return;
                }
            }
            else
            {
                msgObject.btn1Txt = startTimerTxt;
            }
            chestService.ShowNewUnlockPopup(msgObject);
        }
        public void QueueUnlockingAction(int slotId,Action action)
        {
            Debug.Log("Q");
            unlockList.Add(new Tuple<int,Action>(slotId,action));
            ChestSlotController slotController = chestSlots.Find(i => i.chestSlotID == slotId).chestSlotController;
            if(slotController)
            {
                slotController.IsQueued = true;
            }
        }

        public void RemoveFromUnlockQueue(int id)
        {
           var item= unlockList.Find(i => i.Item1 == id);
            if(item!=null)
            {
                unlockList.Remove(item);
            }
        }
        private bool IsInQueue(int id)
        {
            ChestSlotController slotController = chestSlots.Find(i => i.chestSlotID == id).chestSlotController;
            if (slotController && slotController.IsQueued==true)
            {
                return true;
            }
            return false;
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
