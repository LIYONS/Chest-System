using ChestSystem.Chest;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Services
{
    public class ChestSlotsController : MonoBehaviour
    {
        [SerializeField] private int numberOfSlots;

        [SerializeField] private List<ChestSlotController> chestSlotControllers;

        [SerializeField] private List<Chests> chests;
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
        }

        [System.Serializable]
        public struct Chests
        {
            public ChestType chestType;
            public GameObject chestPrefab;
        }
    }
}
