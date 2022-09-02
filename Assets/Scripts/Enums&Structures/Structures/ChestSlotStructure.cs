using ChestSystem.Chest;

public struct ChestSlot
{
    public int chestSlotID;
    public ChestSlotController chestSlotController;

    public ChestSlot(int id, ChestSlotController controller)
    {
        this.chestSlotID = id;
        this.chestSlotController = controller;
    }
}