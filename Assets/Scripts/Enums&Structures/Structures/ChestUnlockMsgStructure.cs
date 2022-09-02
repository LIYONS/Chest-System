using System;

public struct ChestUnlockMsg
{
    public string msgTitle;
    public int gemAmount;
    public string btn1Txt;
    public int chestSlotId;
    public Action btn1Action;
    public Action btn2Action;

    public ChestUnlockMsg(string _title, int _gem,string _btn1Txt, Action _btn1Action, Action _btn2Action, int _slotId = 0)
    {
        msgTitle = _title;
        gemAmount = _gem;
        btn1Txt = _btn1Txt;
        chestSlotId = _slotId;
        btn1Action = _btn1Action;
        btn2Action = _btn2Action;
    }
}
