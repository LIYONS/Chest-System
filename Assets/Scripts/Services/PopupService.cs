using UnityEngine; 
using Singleton;
using ChestSystem.UI;

namespace ChestSystem.Services
{
    public class PopupService : MonoSingletonGeneric<PopupService>
    {
        [SerializeField] private PopupManager popupManager;

        public void ShowNewUnlockPopup(ChestUnlockMsg msgObject)
        {
            popupManager.ChestUnlockPopup(msgObject);
        }

        public void ShowMessage(Message message)
        {
            popupManager.ShowMessage(message);
        }

    }
}
