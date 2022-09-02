using UnityEngine;
using ChestSystem.Chest.SO;
using ChestSystem.Services;
using System;

namespace ChestSystem.Chest.MVC
{
    public class ChestController
    {
        private ChestModel chestModel;
        private ChestView chestView;
        private ChestSlotsController chestSlotsController;
        private ChestSlotController chestSlotController;
        private float unlockDuration;
        private int gemToUnlock;
        private float unlockTimer;
        private bool startCountDown=false;
        private bool isUnlocked = false;

        public Action StartUnlockingAction;
        public Action UnlockImmediateAction;
        
        public void Start()
        {
            ShowSpawnPopup();
            chestSlotController = chestView.GetComponentInParent<ChestSlotController>();
            chestSlotsController = ChestService.Instance.GetChestSlotsController;
            unlockDuration = chestModel.GetChestObject.unlockDuration;
            unlockTimer = unlockDuration;
            chestView.SetTimerText(TimeToString(unlockDuration));
            gemToUnlock = (int) (unlockDuration / chestSlotsController.GetTimeToSkipFor1Gem);
            StartUnlockingAction = StartUnlockingChest;
            UnlockImmediateAction = UnlockImmediate;
        }

        public void Update()
        {
            if(startCountDown && !isUnlocked)
            {
                CheckUnlock();
            }
        }

        private void ShowSpawnPopup()
        {
            Message msg = new(chestView.GetSpawnPopupTitle, $"You have acquired a new {chestModel.GetChestObject.name} chest.\n Coin Range {chestModel.GetChestObject.minCoins} - {chestModel.GetChestObject.maxCoins} \n Gems Range {chestModel.GetChestObject.minGems} - {chestModel.GetChestObject.maxGems} ");
            ChestService.Instance.ShowMessage(msg);
        }
        public void OnUnlockClicked(string title)
        {
            if(chestSlotController)
            {
                ChestUnlockMsg msgObject = new(title,gemToUnlock," ",StartUnlockingAction,UnlockImmediateAction);
                chestSlotController.OnUnlockClicked(msgObject);
            }
        }

        public void UnlockImmediate()
        {
            if(!isUnlocked)
            {
                OnStartUnlocking();
                bool result= ChestService.Instance.ReduceGemCount(gemToUnlock);
                if(result)
                {
                    Unlock();
                }
            }
        }

        public void StartUnlockingChest()
        {
            startCountDown = true;
            OnStartUnlocking();
            ChestService.Instance.CurrentUnlockingChestId = chestSlotController.ChestSlotID;
        }

        private void OnStartUnlocking()
        {
            chestSlotController.UnlockingStatus = true;
            if (chestSlotController.IsQueued)
            {
                chestSlotController.IsQueued = false;
                ChestService.Instance.GetChestSlotsController.RemoveFromUnlockQueue(chestSlotController.ChestSlotID);
            }
        }
        private void CheckUnlock()
        {
            unlockTimer -= Time.deltaTime;
            if (unlockTimer <= unlockDuration - 1)
            {
                unlockDuration--;
                chestView.SetTimerText(TimeToString(unlockDuration));
                if ((int)unlockDuration % (int)ChestService.Instance.GetTimeToSkipFor1Gem == 0f)
                {
                    gemToUnlock--;
                }
            }
            if (unlockDuration <= 0f)
            {
                Unlock();
            }
        }

        public void Unlock()
        {
            if (!isUnlocked)
            {
                isUnlocked = true;
                ChestObject chestObject = chestModel.GetChestObject;
                int gemAquired = UnityEngine.Random.Range(chestObject.minGems, chestObject.maxGems);
                int coinAquired = UnityEngine.Random.Range(chestObject.minCoins, chestObject.maxCoins);
                string description = $"You have acquired\n {gemAquired} gems \n {coinAquired} coins";
                ChestUnlockedMsg msg = new(chestView.GetChestUnlockedTitle, description, coinAquired,gemAquired);
                ChestService.Instance.CurrentUnlockingChestId = 0;
                ChestService.Instance.OnChestUnlocked(msg); 
                chestSlotController.FreeSlot();
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