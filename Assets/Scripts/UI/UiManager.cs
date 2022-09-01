using UnityEngine;
using TMPro;
using ChestSystem.Services;

namespace ChestSystem.UI
{
    public class UiManager : MonoBehaviour
    {
        [Header("Resources")]
        [SerializeField] private TextMeshProUGUI gemCountText;
        [SerializeField] private TextMeshProUGUI coinCountText;
        [SerializeField] private int gemInitialCount;
        [SerializeField] private int coinInitialCount;
        private int gemCount;
        private int coinCount;

        

        private void Start()
        {
            AddGemCount(gemInitialCount);
            AddCoinCount(coinInitialCount);
            
        }
        public void AddGemCount(int amount)
        {
            gemCount += amount;
            if (gemCountText)
            {
                gemCountText.text = gemCount.ToString();
            }
        }

        public void AddCoinCount(int amount)
        {
            coinCount += amount;
            if (coinCountText)
            {
                coinCountText.text = coinCount.ToString();
            }
        }

        public void OnCreateButtonPressed()
        {
            ChestService chestService = ChestService.Instance;
            if(chestService)
            {
                chestService.SpawnRandomChest();
            }
        }

        public int GetGemCount { get { return gemCount; } }
    }
}
