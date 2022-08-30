using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Services
{
    public class ChestSlotsController : MonoBehaviour
    {
        [SerializeField] private int numberOfSlots;

        private List<ChestSlotController> chestSlots;
    }
}
