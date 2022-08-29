using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chest
{
    [CreateAssetMenu(fileName = "ChestList", menuName = "ScriptableObjects/ChestSystem/ChestList")]
    public class ChestConfiguration : ScriptableObject
    {
        public List<ChestConfig> ChestList;
    }
    [System.Serializable]
    public struct ChestConfig
    {
        public ChestType chestType;
        public ChestObject chestObject;
    }

    public enum ChestType
    {
        Common,
        Epic,
        Legendary,
        Rare
    }

}
