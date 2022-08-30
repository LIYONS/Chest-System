using ChestSystem.Chest;
using UnityEngine;
using ChestSystem.Chest.MVC;

namespace ChestSystem.Services
{
    public class ChestSlotController : MonoBehaviour
    {
        private bool isEmpty;
        private ChestModel chestModel;
        private ChestController chestController;
        private ChestView chestView;

        private void Start()
        {
            isEmpty = true;
        }
        public bool GetIsEmpty { get { return isEmpty; } }

        public void SpawnChest(GameObject chestPrefab, ChestConfig config)
        {
            isEmpty = false;
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
                isEmpty = true;
            }
        }
    }   
}
