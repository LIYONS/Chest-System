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
        [SerializeField] private List<ChestSlotController> chestSlotControllers;
        [SerializeField] private List<Chests> chests;

        [Header("Popup")]
        [SerializeField] private UiManager uiManager;
        [SerializeField] private string slotsFullMsgtitle;
        [SerializeField] private string slotsFullMsgDescription;

        [Header("ChestPopup")]
        [SerializeField] private string newChestTitle;
        [SerializeField] private GameObject chestPopupWindow;
        public void SpawnChest(ChestConfig config)
        {
            for (int i = 0; i < chestSlotControllers.Count; i++)
            {
                if(chestSlotControllers[i].GetIsEmpty)
                {
                    GameObject chestPrefab = chests.Find(item => item.chestType == config.chestType).chestPrefab;
                    if (chestPrefab)
                    {
                        chestSlotControllers[i].SpawnChest(chestPrefab,config);
                       
                    }
                    return;
                }
            }
            if(uiManager)
            {
                uiManager.PopUp(slotsFullMsgtitle, slotsFullMsgDescription);
            }
        }

        [System.Serializable]
        public struct Chests
        {
            public ChestType chestType;
            public GameObject chestPrefab;
        }
    }
}
