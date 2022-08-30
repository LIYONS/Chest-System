using UnityEngine;

namespace ChestSystem.Chest.MVC
{
    public class ChestView : MonoBehaviour
    {
        private ChestController chestController;
        public void SetChestController(ChestController _controller)
        {
            chestController = _controller;
        }
    }
}
