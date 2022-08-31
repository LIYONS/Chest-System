using UnityEngine;
using UnityEngine.UI;
using ChestSystem.Chest.MVC;
using ChestSystem.UI;

namespace ChestSystem.Chest
{
    public class ChestSlotController : MonoBehaviour
    {

        [SerializeField] private GameObject emptyText;
        [SerializeField] private GameObject unlockButton;
        [SerializeField] private string newChestPopupTitle;
        private bool isEmpty;
        private ChestModel chestModel;
        private ChestController chestController;
        private ChestView chestView;
        private int chestSlotID;

        private void Start()
        {
            FreeSlot();
            Button unlockBtn = unlockButton.GetComponent<Button>();
            unlockBtn.onClick.AddListener(OnUnlockClicked);
        }
        public bool GetIsEmpty { get { return isEmpty; } }

        public void SpawnChest(GameObject chestPrefab, ChestConfig config)
        {
            isEmpty = false;
            emptyText.SetActive(false);
            unlockButton.SetActive(true);
            chestModel = new(config.chestObject);
            chestController = new(chestModel);
            chestView = Instantiate(chestPrefab, transform).GetComponent<ChestView>();
            ShowNewChestPopup(config);
            SetReferences();
        }

        public void ShowNewChestPopup(ChestConfig config)
        {
            UiManager uiManager = UiService.Instance.GetUiManager;
            if (uiManager)
            {
                uiManager.PopUp(newChestPopupTitle, $"You have acquired a new{config.chestObject.name} Chest\n GemRange\t {config.chestObject.minGems} - {config.chestObject.maxGems} \n CoinRange {config.chestObject.minCoins} - {config.chestObject.maxCoins} ");
            }
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
            unlockButton.SetActive(false);
        }
        public int SetChestSlotID { set => chestSlotID = value; }
        public void OnUnlockClicked()
        {
            if(chestController!=null)
            {
                chestController.OnUnlockClicked();
            }
        }
    }   
}
