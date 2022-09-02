using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using ChestSystem.Services;

namespace ChestSystem.UI
{
    public class PopupManager : MonoBehaviour
    {
        [Header("MsgPopupWindow")]
        [SerializeField] private GameObject msgPopUpWindow;
        [SerializeField] private TextMeshProUGUI msgPopUpTitle;
        [SerializeField] private TextMeshProUGUI msgPopUpDescription;
        [SerializeField] private List<MsgPopup> msgPopups;

        [Header("ChestUnlockPopup")]
        [SerializeField] private GameObject chestPopupWindow;
        [SerializeField] private TextMeshProUGUI chestPopupTitle;
        [SerializeField] private TextMeshProUGUI gemAmountToUnlock;
        [SerializeField] private GameObject unlockImmediateBtn;
        [SerializeField] private GameObject startTimerButton;
        [SerializeField] private string closeButtonTxt="Close";
        [SerializeField] private string startTimerText = "Start Timer";

        private void Start()
        {
            msgPopUpWindow.SetActive(false);
            chestPopupWindow.SetActive(false);
        }

        public void ShowMessage(Message message)
        {
            if (msgPopUpWindow)
            {
                msgPopUpTitle.text = message.msgTitle;
                msgPopUpDescription.text = message.msgDescription;
                msgPopUpWindow.SetActive(true);
            }
        }
        public void ShowMessage(MsgPopupType msgType)
        {
            MsgPopup message = msgPopups.Find(i => i.popupType == msgType);
            if(message.title!=null && msgPopUpWindow)
            {
                msgPopUpTitle.text = message.title;
                msgPopUpDescription.text = message.description;
                msgPopUpWindow.SetActive(true);
            }
        }

        public void OnMsgPopupCloseClicked()
        {
            if (msgPopUpWindow)
            {
                msgPopUpWindow.SetActive(false);
            }
            PopupService.Instance.SetIsShowing = false;
        }
        public void OnCloseclicked()
        {
            chestPopupWindow.SetActive(false);
            PopupService.Instance.SetIsShowing = false;
        }
        public void ChestUnlockPopup(ChestUnlockMsg msgObject)
        {
            chestPopupTitle.text = msgObject.msgTitle;
            gemAmountToUnlock.text = msgObject.gemAmount.ToString();
            unlockImmediateBtn.GetComponent<Button>().onClick.AddListener(msgObject.UnlockImmediateAction); 
            if (PopupService.Instance.CurrentUnlockingChestID==msgObject.chestSlotId)
            {
                startTimerButton.GetComponentInChildren<TextMeshProUGUI>().text = closeButtonTxt;
            }
            else
            {
                startTimerButton.GetComponentInChildren<TextMeshProUGUI>().text = startTimerText;
                startTimerButton.GetComponent<Button>().onClick.AddListener(msgObject.startUnlockAction);
            }
            chestPopupWindow.SetActive(true);
        }
        public GameObject GetStartTimerButton { get { return startTimerButton; } }

        public GameObject GetUnlockImmediateBtn { get { return unlockImmediateBtn; } }

        public GameObject GetChestPopupWindow { get { return chestPopupWindow; } }
    }
}