using UnityEngine;
using Singleton;

namespace ChestSystem.UI
{
    public class UiService : MonoSingletonGeneric<UiService>
    {
        [SerializeField] private UiManager uiManager;

        public UiManager GetUiManager { get { return uiManager; } }

        public bool AddCoinCount(int amount)
        {
            return uiManager.AddCoinCount(amount);
        }

        public bool AddGemCount(int amount)
        {
            return uiManager.AddGemCount(amount);
        }
        public bool ReduceGemCount(int amount)
        {
            return uiManager.ReduceGemCount(amount);
        }

        public bool ReduceCoinCount(int amount)
        {
            return uiManager.ReduceCoinCount(amount);
        }
    }
}
