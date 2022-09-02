using UnityEngine;
using ChestSystem.Services;
using ChestSystem.Chest.MVC;

namespace ChestSystem.Chest
{
    public class ChestSlotController : MonoBehaviour
    {

        [SerializeField] private GameObject emptyText;
        [SerializeField] private string newChestPopupTitle;
        private bool isEmpty;
        private ChestModel chestModel;
        private ChestController chestController;
        private ChestView chestView;

        private void Start()
        {
            FreeSlot();
        }
        public bool GetIsEmpty { get { return isEmpty; } }

        public void SpawnChest(GameObject chestPrefab, ChestConfig config)
        {
            isEmpty = false;
            IsQueued = false;
            emptyText.SetActive(false);
            chestModel = new(config.chestObject);
            chestController = new(chestModel);
            chestView = Instantiate(chestPrefab, transform).GetComponent<ChestView>();
            SetReferences();
        }

        public void OnUnlockClicked(ChestUnlockMsg msgObject)
        {
            msgObject.chestSlotId = ChestSlotID;
            ChestService.Instance.GetChestSlotsController.ShowUnlock(msgObject);
        }

        private void SetReferences()
        {
            chestModel.SetChestController(chestController);
            chestController.SetChestView(chestView);
            chestView.SetChestController(chestController);
        }

        public void FreeSlot()
        {
            
            if (chestView)
            {
                Destroy(chestView.gameObject);
            }
            isEmpty = true;
            emptyText.SetActive(true);
            UnlockingStatus = false;
        }

        public int ChestSlotID { get; set; }

        public bool UnlockingStatus { get; set; }

        public bool IsQueued { get; set; }
    }   
}
