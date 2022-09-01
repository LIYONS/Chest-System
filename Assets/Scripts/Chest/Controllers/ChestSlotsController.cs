using System.Collections.Generic;
using UnityEngine;
using ChestSystem.Chest.SO;

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
                chestSlotController.SetChestSlotID=chestSlotController.GetInstanceID();
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
        }

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
