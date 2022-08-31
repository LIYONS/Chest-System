using ChestSystem.UI;
using System;

namespace ChestSystem.Chest.MVC
{
    public class ChestController
    {
        private ChestModel chestModel;
        private ChestView chestView;

        private float unlockDuration;
        private int gemToUnlock;
        
        public void Start()
        {
            unlockDuration = chestModel.GetChestObject.unlockDuration;
            TimeSpan time = TimeSpan.FromSeconds(unlockDuration);
            chestView.SetTimerText(time.ToString(@"hh\:mm\:ss"));
            gemToUnlock = (int) (unlockDuration / UiService.Instance.GetSlotsController.GetTimeToSkipFor1Gem);
        }
        public void Update()
        {

        }
        public void OnUnlockClicked()
        {
            UiService.Instance.GetUiManager.ChestUnlockPopup(chestView.GetUnlockTitle,  gemToUnlock.ToString());
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
