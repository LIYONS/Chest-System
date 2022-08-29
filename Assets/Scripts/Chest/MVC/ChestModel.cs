namespace ChestSystem.Chest.MVC
{
    public class ChestModel
    {
        private ChestObject chestObject;
        public ChestModel(ChestObject _chestObject)
        {
            chestObject = _chestObject;
        }

        public ChestObject GetChestObject { get { return chestObject; } }
    }
}
