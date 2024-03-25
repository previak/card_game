using System.Collections;
using System.Collections.Generic;
using _Source.Core;
using _Source.Models;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Source.View
{
    public class CardView : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameObject cardBack;
        [SerializeField] private GameObject cardFront;
        private CardInstance _cardInstance;

        public void Init(CardInstance card)
        {
            _cardInstance = card;
            var cardRenderer = cardFront.GetComponentInChildren<Image>();

            cardRenderer.color = _cardInstance.CardAsset.cardColor;
            cardRenderer.sprite = _cardInstance.CardAsset.cardFrontImage;
            cardRenderer.name = _cardInstance.CardAsset.cardName;
        }
        
        public void Rotate(bool faceUp)
        {
            cardFront.SetActive(faceUp);
            cardBack.SetActive(!faceUp);
        }

        private void PlayCard()
        {
            _cardInstance.MoveToLayout(CardGame.Instance.FieldLayoutId);
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            PlayCard();
        }
    }
}
