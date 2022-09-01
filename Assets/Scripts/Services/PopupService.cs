using UnityEngine; 
using Singleton;
using ChestSystem.UI;
using System;
using System.Collections.Generic;

namespace ChestSystem.Services
{
    public class PopupService : MonoSingletonGeneric<PopupService>
    {
        [SerializeField] private PopupManager popupManager;

        private bool isShowing;
        private Queue<Action> popUpQueue=new();

        private void Update()
        {
            if(!isShowing && popUpQueue.Count!=0)
            {
                isShowing = true;
                Action action = popUpQueue.Dequeue();
                action();
            }
        }
        public void QueueNewUnlockPopup(ChestUnlockMsg msgObject)
        {
            Action action = new(() => ShowNewUnlockMsg(msgObject));
            popUpQueue.Enqueue(action);
        }

        public void QueueMessage(Message message)
        {
            Action action = new(()=>ShowMessage(message));
            popUpQueue.Enqueue(action);    
        }
        public void ShowMessage(Message message)
        {
            popupManager.ShowMessage(message);
        }
        public void ShowNewUnlockMsg(ChestUnlockMsg msgObject)
        {
            popupManager.ChestUnlockPopup(msgObject);
        }
        public void OnChestUnlocked()
        {
            popupManager.OnChestUnlocked();
        }

        public bool SetIsShowing { set { isShowing = value; } }
    }
}
