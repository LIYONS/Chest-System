namespace ChestSystem.Chest.MVC
{
    public class ChestModel
    {
        private ChestObject chestObject;

        private ChestController chestController;
        public ChestModel(ChestObject _chestObject)
        {
            chestObject = _chestObject;
        }

        public ChestObject GetChestObject { get { return chestObject; } }

        public void SetChestController(ChestController _controller)
        {
            chestController = _controller;
        }
    }
}
