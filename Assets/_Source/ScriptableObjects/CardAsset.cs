using UnityEngine;

namespace _Source.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class CardAsset : ScriptableObject
    {
        [SerializeField]
        public string cardName;
        [SerializeField]
        public Color cardColor;
        [SerializeField]
        public Sprite cardImage;
    }
}