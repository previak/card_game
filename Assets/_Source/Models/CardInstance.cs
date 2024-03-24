using _Source.Core;
using _Source.ScriptableObjects;

namespace _Source.Models
{
    public class CardInstance
    {
        public CardAsset CardAsset { get; private set; }
        public int LayoutId;
        public int CardPosition;

        public CardInstance(CardAsset asset)
        {
            CardAsset = asset;
        }

        public void MoveToLayout(int newLayoutId)
        {
            CardPosition = CardGame.Instance.GetCardsInLayout(newLayoutId).Count;
            LayoutId = newLayoutId;
        }
    }
}