using UnityEngine;
using Singleton;

namespace ChestSystem.UI
{
    public class UiService : MonoSingletonGeneric<UiService>
    {
        [SerializeField] private UiManager uiManager;

        public UiManager GetUiManager { get { return uiManager; } }

        public void AddGemCount(int amount)
        {
            uiManager.AddGemCount(amount);
        }

        public void AddCoinCount(int amount)
        {
            uiManager.AddCoinCount(amount);
        }
    }
}
