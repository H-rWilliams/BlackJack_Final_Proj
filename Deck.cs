using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack_Final_Proj
{

    internal class Deck
    {
        string jack = "J";
        string queen = "Q";
        string king = "K";
        string ace = "A";

        List<string> deck = new List<string>();

        public Deck()
        {
            InitializeDeck();
        }

        private void InitializeDeck()
        {
            const int numDecks = 6;

            for (int d = 0; d < numDecks; d++)
            {
                for (int i = 2; i <= 10; i++)
                {
                    deck.Add(i.ToString());
                }

                deck.Add(jack);
                deck.Add(queen);
                deck.Add(king);
                deck.Add(ace);
            }

            ShuffleDeck();
        }

        private void ShuffleDeck()
        {
            Random random = new Random();
            int n = deck.Count;

            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);

                string temp = deck[i];
                deck[i] = deck[j];
                deck[j] = temp;
            }
        }

        public string DealCard()
        {
            if (deck.Count == 0)
            {
                Console.WriteLine("The deck is empty. Reshuffling the deck.");
                InitializeDeck();
            }

            string card = deck[0];
            deck.RemoveAt(0);
            return card;
        }
    }
}


