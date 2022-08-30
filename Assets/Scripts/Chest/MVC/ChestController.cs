namespace ChestSystem.Chest.MVC
{
    public class ChestController
    {
        private ChestModel chestModel;

        private ChestView chestView;
        public ChestController(ChestModel _model)
        {
            chestModel = _model;
        }

        public void SetChestView(ChestView _view)
        {
            chestView = _view;
        }
    }
}
