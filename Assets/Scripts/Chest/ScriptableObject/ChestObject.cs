using UnityEngine;

namespace ChestSystem.Chest
{
    [CreateAssetMenu(fileName ="ChestObject",menuName ="ScriptableObjects/ChestSystem/ChestObject")] 
    public class ChestObject : ScriptableObject
    {
        public float unlockDuration;
        public string chestName;
        public int minGems;
        public int maxGems;
        public int minCoins;
        public int maxCoins;
    }
}
