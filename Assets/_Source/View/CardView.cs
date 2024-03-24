using System.Collections;
using System.Collections.Generic;
using _Source.Models;
using UnityEngine;

namespace _Source.View
{
    public class CardView : MonoBehaviour
    {
        private CardInstance _cardInstance;

        public void Init(CardInstance card)
        {
            _cardInstance = card;
        }
    }
}
