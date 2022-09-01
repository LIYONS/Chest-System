using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace ChestSystem.Chest.MVC
{
    public class ChestView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private string unlockPopupTitle;
        [SerializeField] private string unlockedPopupTitle = "Congratulations";
        [SerializeField] private Button unlockButton;

        [SerializeField] private string spawnPopupTitle = "Congratulations";
        private ChestController chestController;

        private void Start()
        {
            chestController.Start();
            unlockButton.onClick.AddListener(OnUnlockClicked);
        }
        private void Update()
        {
            chestController.Update();
        }
        public void SetTimerText(string text)
        {
            timerText.text = text;
        }

        public void OnUnlockClicked()
        {
            chestController.UnlockClicked(unlockPopupTitle);
        }
        public void SetChestController(ChestController _controller)
        {
            chestController = _controller;
        }

        public string GetChestUnlockedTitle { get { return unlockedPopupTitle; } }

        public string GetSpawnPopupTitle { get { return spawnPopupTitle; } }
    }
}
