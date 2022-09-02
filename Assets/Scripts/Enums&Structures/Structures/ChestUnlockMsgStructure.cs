public struct ChestUnlockMsg
{
    public string msgTitle;
    public int gemAmount;
    public int chestSlotId;
    public UnityEngine.Events.UnityAction startUnlockAction;
    public UnityEngine.Events.UnityAction UnlockImmediateAction;

    public ChestUnlockMsg(string title, int gem, UnityEngine.Events.UnityAction startAction, UnityEngine.Events.UnityAction immediateAction, int slotId = 0)
    {
        msgTitle = title;
        gemAmount = gem;
        chestSlotId = slotId;
        startUnlockAction = startAction;
        UnlockImmediateAction = immediateAction;
    }
}
