using UnityEngine;

namespace _Source.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class CardAsset : ScriptableObject
    {
        public string cardName;
        public Color cardColor;
        public Sprite cardFrontImage;
        public Sprite cardBackImage;
    }
}