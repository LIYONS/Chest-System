using UnityEngine;
using ChestSystem.Chest;
using Singleton;

namespace ChestSystem.Services
{
    public class ChestService : MonoSingletonGeneric<ChestService>
    {
        [SerializeField] private ChestConfiguration chestConfiguration;

        private void SpawnChest(ChestType chestType)
        {

        }
    }
}
