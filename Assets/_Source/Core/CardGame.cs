using System.Collections.Generic;
using System.Linq;
using _Source.Models;
using _Source.ScriptableObjects;
using _Source.View;
using UnityEngine;

namespace _Source.Core
{
    public class CardGame : MonoBehaviour
    {
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private List<int> playerLayoutIds;
        [SerializeField] private int deckLayoutId;
        [field: SerializeField] private int HandCapacity { get; set; }
        [field: SerializeField] public List<CardAsset> InitialCards { get; set; }  = new List<CardAsset>();
        [field: SerializeField] private List<CardAsset> Deck { get; set; } = new List<CardAsset>();
        [field: SerializeField] public int FieldLayoutId { get; private set; }
        
        private readonly Dictionary<CardInstance, CardView> _allCards = new Dictionary<CardInstance, CardView>();
        public static CardGame Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
        
        private void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            foreach (var layoutId in playerLayoutIds)
            {
                foreach (var cardAsset in InitialCards)
                {
                    CreateCard(cardAsset, layoutId);
                }
            }
            InitDeck();
        }
        
        private void CreateCard(CardAsset asset, int layoutId)
        {
            var instance = new CardInstance(asset);
            CreateCardView(instance);
            instance.MoveToLayout(layoutId);
        }
        
        private void CreateCardView(CardInstance instance)
        {
            if (_allCards.ContainsKey(instance))
            {
                return;
            }
            
            var view = Instantiate(cardPrefab, transform).GetComponent<CardView>();
            view.Init(instance);
            _allCards.Add(instance, view);
        }
        
        public List<CardInstance> GetCardsInLayout(int layoutId)
        {
            return _allCards
                .Where(pair => pair.Key.LayoutId == layoutId)
                .Select(pair => pair.Key)
                .ToList();
        }

        public CardView GetCardView(CardInstance instance)
        {
            return _allCards[instance];
        }
        
        public void RecalculateLayout(int layoutId)
        {
            var cards = GetCardsInLayout(layoutId);
            for (var i = 0; i < cards.Count; i++)
            {
                cards[i].CardPosition = i;
            }
        }
        
        public void StartTurn()
        {
            foreach (var layoutId in playerLayoutIds)
            {
                var cardsInHand = GetCardsInLayout(layoutId);

                while (cardsInHand.Count < HandCapacity)
                {
                    var cardsInDeck = GetCardsInLayout(deckLayoutId);
                    if (cardsInDeck.Count == 0)
                    {
                        Debug.Log("Deck is empty!");
                        return;
                    }
                    var card = cardsInDeck[0];
                    card.MoveToLayout(layoutId);
                    cardsInHand = GetCardsInLayout(layoutId);
                }
            }
        }
        
        private void InitDeck()
        {
            foreach (var deckPart in Deck)
            {
                CreateCard(deckPart, deckLayoutId);
            }
        }
        
        public void ShuffleLayout(int layoutId)
        {
            var cards = GetCardsInLayout(layoutId);
            for (var i = cards.Count - 1; i > 0; i--)
            {
                var j = Random.Range(0, i + 1);
                (cards[i].CardPosition, cards[j].CardPosition) = (cards[j].CardPosition, cards[i].CardPosition);
            }
        }
    }
}
