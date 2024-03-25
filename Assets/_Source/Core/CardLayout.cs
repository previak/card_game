using System.Collections.Generic;
using _Source.Models;
using _Source.View;
using UnityEngine;

namespace _Source.Core
{
    public class CardLayout : MonoBehaviour
    {
        [SerializeField] public int layoutId;
        [SerializeField] public Vector2 offset;
        [SerializeField] public bool faceUp;
        private RectTransform _rectTransform;
        private const float Smoothing = 5f;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            var cardsInLayout = CardGame.Instance.GetCardsInLayout(layoutId);
            foreach (var card in cardsInLayout)
            {
                var cardView = CardGame.Instance.GetCardView(card);
                var cardTransform = (RectTransform)cardView.transform;
                cardTransform.SetParent(transform, false);
                cardTransform.anchoredPosition = Vector2.Lerp(cardTransform.anchoredPosition, offset * card.CardPosition, Time.deltaTime * Smoothing);
                cardTransform.SetSiblingIndex(card.CardPosition);
                cardView.Rotate(faceUp);
            }
        }
    }
}
