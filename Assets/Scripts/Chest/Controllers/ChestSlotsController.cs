using System;
using System.Collections.Generic;
using UnityEngine;
using ChestSystem.UI;

namespace ChestSystem.Chest
{
    public class ChestSlotsController : MonoBehaviour
    {
        [SerializeField] private int numberOfSlots;
        [SerializeField] private List<Chests> chests;
        [SerializeField] private GameObject chestSlotPrefab;
        [SerializeField] private float timeToSkipFor1Gem;
        private List<ChestSlot> chestSlots = new();

        [Header("Popup")]
        [SerializeField] private string slotsFullMsgtitle;
        [SerializeField] private string slotsFullMsgDescription;
        private UiManager uiManager;

        [Header("NewChestPopup")]
        [SerializeField] private string newChestTitle;
        private bool isUnlockingChest = false;


        private void Start()
        {
            uiManager = UiService.Instance.GetUiManager;
            for (int i = 0; i < numberOfSlots; i++)
            {
                ChestSlot chestSlot=new(i,Instantiate(chestSlotPrefab, transform).GetComponent<ChestSlotController>());
                chestSlots.Add(chestSlot);
                chestSlot.chestSlotController.SetChestSlotID = i;
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
            if(uiManager)
            {
                uiManager.PopUp(slotsFullMsgtitle, slotsFullMsgDescription);
            }
        }
        public bool GetIsUnlockingChest { get { return isUnlockingChest; } set { isUnlockingChest = value; } }

        public float GetTimeToSkipFor1Gem { get { return timeToSkipFor1Gem; } }
        [System.Serializable]
        public struct Chests
        {
            public ChestType chestType;
            public GameObject chestPrefab;
        }

    }
    public struct ChestSlot
    {
        public int chestSlotID;
        public ChestSlotController chestSlotController;

        public ChestSlot(int id, ChestSlotController controller)
        {
            this.chestSlotID = id;
            this.chestSlotController = controller;
        }
    }
}
