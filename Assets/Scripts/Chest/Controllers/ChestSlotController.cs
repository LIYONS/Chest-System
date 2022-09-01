using UnityEngine;
using ChestSystem.Services;
using ChestSystem.Chest.MVC;
using ChestSystem.Chest.SO;

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
        private int chestSlotID;

        private void Start()
        {
            FreeSlot();
        }
        public bool GetIsEmpty { get { return isEmpty; } }

        public void SpawnChest(GameObject chestPrefab, ChestConfig config)
        {
            isEmpty = false;
            emptyText.SetActive(false);
            chestModel = new(config.chestObject);
            chestController = new(chestModel);
            chestView = Instantiate(chestPrefab, transform).GetComponent<ChestView>();
            SetReferences();
        }
        public void UnlockClicked(ChestUnlockMsg msgObject)
        {
            msgObject.chestSlotId = chestSlotID;
            ChestService.Instance.ShowNewUnlockPopup(msgObject);
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
        }
        public int SetChestSlotID { set => chestSlotID = value; }
    }   
}
