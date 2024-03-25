using _Source.Core;
using _Source.ScriptableObjects;

namespace _Source.Models
{
    public class CardInstance
    {
        public CardAsset CardAsset { get; private set; }
        public int LayoutId { get; private set; } 
        public int CardPosition { get; set; }

        public CardInstance(CardAsset asset)
        {
            CardAsset = asset;
        }

        public void MoveToLayout(int newLayoutId)
        {
            CardGame.Instance.RecalculateLayout(LayoutId);
            LayoutId = newLayoutId;
            CardGame.Instance.RecalculateLayout(LayoutId);
        }
    }
}