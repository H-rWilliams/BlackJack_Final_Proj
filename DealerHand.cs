using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack_Final_Proj
{
    internal class DealerHand
    {
        private List<string> hand = new List<string>();

        public DealerHand()
        {
            // Deal initial card to the dealer
            DealInitialCard();
        }

        private void DealInitialCard()
        {
            Deck deck = new Deck(); // Assuming you have a Deck class
            hand.Add(deck.DealCard());
        }

        public int CalculateHandValue()
        {
            int handValue = 0;
            int numAces = 0;

            foreach (string card in hand)
            {
                int cardValue;
                if (int.TryParse(card, out cardValue))
                {
                    handValue += cardValue;
                }
                else
                {
                    if (card == "A")
                    {
                        // Count Aces and add 11 temporarily
                        numAces++;
                        handValue += 11;
                    }
                    else
                    {
                        // Face cards
                        handValue += 10;
                    }
                }
            }

            // Adjust for Aces if needed
            while (numAces > 0 && handValue > 21)
            {
                handValue -= 10; // Change the value of one Ace from 11 to 1
                numAces--;
            }

            return handValue;
        }

        public void Hit()
        {
            // Dealer must draw cards until reaching 17 or busting
            Deck deck = new Deck(); // Assuming you have a Deck class
            hand.Add(deck.DealCard());
        }

        public bool IsBust()
        {
            return CalculateHandValue() > 21;
        }

        public List<string> GetHand()
        {
            return hand;
        }
    }
}

