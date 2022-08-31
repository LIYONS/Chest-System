using UnityEngine;
using Singleton;
using ChestSystem.Services;

namespace ChestSystem.UI
{
    public class UiService : MonoSingletonGeneric<UiService>
    {
        [SerializeField] private UiManager uiManager;
        [SerializeField] private ChestSlotsController chestSlotsController;

        public UiManager GetUiManager { get { return uiManager; } }

        public ChestSlotsController GetSlotsController { get { return chestSlotsController; } }
    }
}
