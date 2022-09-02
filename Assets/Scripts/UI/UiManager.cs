using UnityEngine;
using TMPro;
using ChestSystem.Services;
using System;

namespace ChestSystem.UI
{
    public class UiManager : MonoBehaviour
    {
        [Header("Resources")]
        [SerializeField] private TextMeshProUGUI gemCountText;
        [SerializeField] private TextMeshProUGUI coinCountText;
        [SerializeField] private int gemInitialCount;
        [SerializeField] private int coinInitialCount;
        [SerializeField] private int maxGems;
        [SerializeField] private int maxCoins;
        private int gemCount;
        private int coinCount;

        

        private void Start()
        {
            AddGemCount(gemInitialCount);
            AddCoinCount(coinInitialCount);  
        }

        public bool AddCoinCount(int amount)
        {
            if (coinCount + amount <= maxCoins)
            {
                coinCount += amount;
                if (coinCountText)
                {
                    coinCountText.text = coinCount.ToString();
                }
                return true;
            }
            return false;
        }

        public bool AddGemCount(int amount)
        {
            if(gemCount+amount<=maxGems)
            {
                gemCount += amount;
                if (gemCountText)
                {
                    gemCountText.text = gemCount.ToString();
                }
                return true;
            }
            return false;
        }
        public bool ReduceGemCount(int amount)
        {
            if(gemCount-amount>=0)
            {
                gemCount -= amount;
                if (gemCountText)
                {
                    gemCountText.text = gemCount.ToString();
                }
                return true;
            }
            return false;
        }

        public bool ReduceCoinCount(int amount)
        {
            if(coinCount-amount>0)
            {
                coinCount -= amount;
                if (coinCountText)
                {
                    coinCountText.text = coinCount.ToString();
                }
                return true;
            }
            return false;
        }
        public void OnCreateButtonPressed()
        {
            ChestService chestService = ChestService.Instance;
            if(chestService)
            {
                chestService.SpawnChest();
            }
        }

        public int GetGemCount { get { return gemCount; } }
    }
}
