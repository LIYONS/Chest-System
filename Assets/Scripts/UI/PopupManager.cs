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
        [SerializeField] private Button btn2;
        [SerializeField] private Button btn1;

        private ChestUnlockMsg msgObject;

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

        public void OnBtn1Clicked()
        {
            if (msgObject.btn1Action!=null)
            {
                msgObject.btn1Action();
            }

        }
        public void OnBtn2Clicked()
        {
            if (msgObject.btn2Action != null)
            {
                msgObject.btn2Action();
            }
        }
        public void ChestUnlockPopup(ChestUnlockMsg msgObject)
        {
            this.msgObject = msgObject;
            chestPopupTitle.text = msgObject.msgTitle;
            gemAmountToUnlock.text = msgObject.gemAmount.ToString();
            btn1.GetComponentInChildren<TextMeshProUGUI>().text = msgObject.btn1Txt;
            chestPopupWindow.SetActive(true);
        }

        public GameObject GetChestPopupWindow { get { return chestPopupWindow; } }
    }
}