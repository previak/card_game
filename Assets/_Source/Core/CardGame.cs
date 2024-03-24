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
        [SerializeField]
        private GameObject cardPrefab;
        
        [SerializeField]
        private List<int> playerLayoutIds;
        
        private static CardGame _instance;

        [field: SerializeField]
        public List<CardAsset> InitialCards { get; set; }  = new List<CardAsset>();
        
        private Dictionary<CardInstance, CardView> _allCards = new Dictionary<CardInstance, CardView>();
        
        public static CardGame Instance
        {
            get => _instance;
            set
            {
                if (_instance == null)
                    _instance = value;
            }
        }

        private void Awake()
        {
            _instance = this;
        }
        
        private void Start()
        {
            StartGame();
        }

        public void StartGame()
        {
            foreach (var layoutId in playerLayoutIds)
            {
                foreach (var cardAsset in InitialCards)
                {
                    CreateCard(cardAsset, layoutId);
                }
            }
        }
        
        private void CreateCard(CardAsset asset, int layoutId)
        {
            CardInstance instance = new CardInstance(asset);
            CreateCardView(instance);
            instance.MoveToLayout(layoutId);
        }
        
        private void CreateCardView(CardInstance instance)
        {
            CardView view = Instantiate(cardPrefab, transform).GetComponent<CardView>();
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
        
    }
}
