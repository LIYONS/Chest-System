using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ChestSystem.UI
{
    public class PopupManager : MonoBehaviour
    {
        [Header("MsgPopupWindow")]
        [SerializeField] private GameObject msgPopUpWindow;
        [SerializeField] private TextMeshProUGUI msgPopUpTitle;
        [SerializeField] private TextMeshProUGUI msgPopUpDescription;

        [Header("ChestUnlockPopup")]
        [SerializeField] private GameObject chestPopupWindow;
        [SerializeField] private TextMeshProUGUI chestPopupTitle;
        [SerializeField] private TextMeshProUGUI gemAmountToUnlock;
        [SerializeField] private GameObject unlockImmediateBtn;
        [SerializeField] private GameObject startTimerButton;
        [SerializeField] private string closeButtonTxt="Close";
        [SerializeField] private string startTimerText = "Start Timer";
        private int currentChestSlotBeingUnlocked=0;
       // private bool isUnlocking=false;

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

        public void OnPopupCloseClicked()
        {
            if (msgPopUpWindow)
            {
                msgPopUpWindow.SetActive(false);
            }
        }
        public void OnChestPopupButtonClicked()
        {
            chestPopupWindow.SetActive(false);
        }
        public void ChestUnlockPopup(ChestUnlockMsg msgObject)
        {
            chestPopupTitle.text = msgObject.msgTitle;
            gemAmountToUnlock.text = msgObject.gemAmount.ToString();
            unlockImmediateBtn.GetComponent<Button>().onClick.AddListener(msgObject.UnlockImmediateAction); 
            if (currentChestSlotBeingUnlocked==msgObject.chestSlotId)
            {
                startTimerButton.GetComponentInChildren<TextMeshProUGUI>().text = closeButtonTxt;
            }
            else
            {
                currentChestSlotBeingUnlocked = msgObject.chestSlotId;
                startTimerButton.GetComponentInChildren<TextMeshProUGUI>().text = startTimerText;
                startTimerButton.GetComponent<Button>().onClick.AddListener(msgObject.startUnlockAction);
            }
            Debug.Log(msgObject.chestSlotId);
            chestPopupWindow.SetActive(true);
        }
        public GameObject GetStartTimerButton { get { return startTimerButton; } }

        public GameObject GetUnlockImmediateBtn { get { return unlockImmediateBtn; } }

        public GameObject GetChestPopupWindow { get { return chestPopupWindow; } }
    }
}
public struct ChestUnlockMsg
{
    public string msgTitle;
    public int gemAmount;
    public int chestSlotId;
    public UnityEngine.Events.UnityAction startUnlockAction;
    public UnityEngine.Events.UnityAction UnlockImmediateAction;

    public ChestUnlockMsg(string title,int gem, UnityEngine.Events.UnityAction startAction, UnityEngine.Events.UnityAction immediateAction, int slotId = 0)
    {
        msgTitle = title;
        gemAmount = gem;
        chestSlotId = slotId;
        startUnlockAction = startAction;
        UnlockImmediateAction = immediateAction;
    }
}

public struct Message
{
    public string msgTitle;
    public string msgDescription;

    public Message(string title,string description)
    {
        msgTitle = title;
        msgDescription = description;
    }
}