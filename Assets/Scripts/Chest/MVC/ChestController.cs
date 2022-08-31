using UnityEngine;
using UnityEngine.UI;
using ChestSystem.UI;
using ChestSystem.Services;
using System;

namespace ChestSystem.Chest.MVC
{
    public class ChestController
    {
        private ChestModel chestModel;
        private ChestView chestView;
        private UiManager uiManager;
        private ChestSlotsController chestSlotsController;
        private float unlockDuration;
        private int gemToUnlock;
        private float unlockTimer;
        private bool startCountDown=false;
        private bool isUnlocked = false;

        
        public void Start()
        {
            uiManager = UiService.Instance.GetUiManager;
            chestSlotsController = UiService.Instance.GetSlotsController;
            unlockDuration = chestModel.GetChestObject.unlockDuration;
            unlockTimer = unlockDuration;
            chestView.SetTimerText(TimeToString(unlockDuration));
            gemToUnlock = (int) (unlockDuration / chestSlotsController.GetTimeToSkipFor1Gem);
        }
        public void Update()
        {
            if(startCountDown && !isUnlocked)
            {
                CheckUnlock();
            }
        }
        public void OnUnlockClicked()
        {
            if(chestSlotsController.GetIsUnlockingChest && !startCountDown)
            {
                uiManager.PopUp("Oops", "Something is being Unlocked.Try later");
                return;
            }
            uiManager.ChestUnlockPopup(chestView.GetUnlockTitle,  gemToUnlock.ToString());
            uiManager.GetUnlockImmediateBtn.GetComponent<Button>().onClick.AddListener(OnUnlockImmediate);
            if(startCountDown)
            {
                uiManager.GetStartTimerButton.SetActive(false);
                return;
            }
            uiManager.GetStartTimerButton.GetComponent<Button>().onClick.AddListener(OnStartTimer);
        }

        public void OnStartTimer()
        {
            startCountDown = true;
            chestSlotsController.GetIsUnlockingChest = true;
            uiManager.GetChestPopupWindow.SetActive(false);
        }

        public void OnUnlockImmediate()
        {
            if((uiManager.GetGemCount-gemToUnlock)<0)
            {
                uiManager.PopUp("Oops", "Not enough gems.Try to earn some more");
                return;
            }
            uiManager.AddGemCount(-gemToUnlock);
            Unlock();
            uiManager.GetChestPopupWindow.SetActive(false);
        }
        public void Unlock()
        {
            if (!isUnlocked)
            {
                isUnlocked = true;
                ChestObject chestObject = chestModel.GetChestObject;
                chestSlotsController.GetIsUnlockingChest = false;
                int gemAquired = UnityEngine.Random.Range(chestObject.minGems, chestObject.maxGems);
                int coinAquired = UnityEngine.Random.Range(chestObject.minCoins, chestObject.maxCoins);
                uiManager.AddCoinCount(coinAquired);
                uiManager.AddGemCount(gemAquired);
                uiManager.PopUp("Congratulations", $"You have acquired \n {gemAquired} -Gems \n {coinAquired} -Coins");
                chestView.GetComponentInParent<ChestSlotController>().FreeSlot();
            }
        }

        private void CheckUnlock()
        {
            unlockTimer -= Time.deltaTime;
            if (unlockTimer <= unlockDuration - 1)
            {
                unlockDuration--;
                chestView.SetTimerText(TimeToString(unlockDuration));
                if ((int)unlockDuration % (int)UiService.Instance.GetSlotsController.GetTimeToSkipFor1Gem == 0f)
                {
                    gemToUnlock--;
                }
            }
            if (unlockDuration <= 0f)
            {
                Unlock();
            }
        }

        private string TimeToString(float value)
        {
            TimeSpan time = TimeSpan.FromSeconds(value);
            return time.ToString(@"hh\:mm\:ss");
        }
        public ChestModel GetChestModel { get { return chestModel; } }
        public ChestController(ChestModel _model)
        {
            chestModel = _model;
        }
        public void SetChestView(ChestView _view)
        {
            chestView = _view;
        }
    }
}
