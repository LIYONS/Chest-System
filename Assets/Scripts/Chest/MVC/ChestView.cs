using UnityEngine;
using TMPro;
using System;
using ChestSystem.UI;

namespace ChestSystem.Chest.MVC
{
    public class ChestView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private string unlockPopupTitle;
        private ChestController chestController;
        

        private void Start()
        {
            chestController.Start();
            
            
        }
        private void Update()
        {
            chestController.Update();
        }
        public void SetTimerText(string text)
        {
            timerText.text = text;
        }
        public void SetChestController(ChestController _controller)
        {
            chestController = _controller;
        }
        public string GetUnlockTitle { get { return unlockPopupTitle; } }
    }
}
