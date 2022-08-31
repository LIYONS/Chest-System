using ChestSystem.Chest;
using System;
using System.Collections.Generic;
using UnityEngine;
using ChestSystem.UI;

namespace ChestSystem.Services
{
    public class ChestSlotsController : MonoBehaviour
    {
        [SerializeField] private int numberOfSlots;
        [SerializeField] private List<Chests> chests;
        [SerializeField] private GameObject chestSlotPrefab;
        [SerializeField] private float timeToSkipFor1Gem;
        private List<ChestSlotController> chestSlotControllers = new();

        [Header("Popup")]
        [SerializeField] private string slotsFullMsgtitle;
        [SerializeField] private string slotsFullMsgDescription;
        private UiManager uiManager;

        [Header("NewChestPopup")]
        [SerializeField] private string newChestTitle;

        private void Start()
        {
            uiManager = UiService.Instance.GetUiManager;
            for (int i = 0; i < numberOfSlots; i++)
            {
                chestSlotControllers.Add(Instantiate(chestSlotPrefab, transform).GetComponent<ChestSlotController>());
            }
        }
        public void SpawnChest(ChestConfig config)
        {
            for (int i = 0; i < chestSlotControllers.Count; i++)
            {
                if (chestSlotControllers[i].GetIsEmpty)
                {
                    GameObject chestPrefab = chests.Find(item => item.chestType == config.chestType).chestPrefab;
                    if (chestPrefab)
                    {
                        chestSlotControllers[i].SpawnChest(chestPrefab, config);
                        ShowNewChestPopup(config);
                    }
                    return;
                }
            }
            if(uiManager)
            {
                uiManager.PopUp(slotsFullMsgtitle, slotsFullMsgDescription);
            }
        }

        public void ShowNewChestPopup(ChestConfig config)
        {
            if(uiManager)
            {
                uiManager.PopUp(newChestTitle, $"You have acquired a new{config.chestObject.name} Chest\n GemRange\t {config.chestObject.minGems} - {config.chestObject.maxGems} \n CoinRange {config.chestObject.minCoins} - {config.chestObject.maxCoins} " );
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
}
