using UnityEngine;
using UnityEngine.UI;
using ChestSystem.Chest.MVC;
using ChestSystem.Chest;

namespace ChestSystem.Services
{
    public class ChestSlotController : MonoBehaviour
    {

        [SerializeField] private GameObject emptyText;
        [SerializeField] private GameObject unlockButton;
        private bool isEmpty;
        private ChestModel chestModel;
        private ChestController chestController;
        private ChestView chestView;

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
            SetReferences();
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

        public void OnUnlockClicked()
        {
            if(chestController!=null)
            {
                chestController.OnUnlockClicked();
            }
        }
    }   
}
