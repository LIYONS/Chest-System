using UnityEngine;
using TMPro;
using Singleton;
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

        [Header("PopUpWindow")]
        [SerializeField] private GameObject popUpWindow;
        [SerializeField] private TextMeshProUGUI popUpTitle;
        [SerializeField] private TextMeshProUGUI popUpDescription;

        private void Start()
        {
            SetGemCount(gemInitialCount);
            SetCoinCount(coinInitialCount);
            if (popUpWindow)
            {
                popUpWindow.SetActive(false);
            }
        }
        public void SetGemCount(int amount)
        {
            gemCount += amount;
            if (gemCountText)
            {
                gemCountText.text = gemCount.ToString();
            }
        }

        public void SetCoinCount(int amount)
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

        public void PopUp(string title,string description)
        {
            if (popUpWindow)
            {
                popUpTitle.text = title;
                popUpDescription.text = description;
                popUpWindow.SetActive(true);
            }
        }

        public void OnPopupCloseClicked()
        {
            if (popUpWindow)
            {
                popUpWindow.SetActive(false);
            }
        }
    }
}
