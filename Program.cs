using BlackJack_Final_Proj;
using System;


class Program
{
    static void DisplayHands(PlayerHand playerHand, DealerHand dealerHand, bool showDealerFaceDown = false)
    {
        Console.Write("Player's hand: " + FormatCards(playerHand.GetHand(), false));
        Console.WriteLine($"   Total: {playerHand.CalculateHandValue()}");

        Console.Write("Dealer's hand: " + FormatCards(dealerHand.GetHand(), showDealerFaceDown));
        Console.WriteLine($"   Total: {dealerHand.CalculateHandValue()}");
        Console.WriteLine();
    }



    static string FormatCards(List<string> cards, bool showDealerFaceDown)
    {
        return string.Join(", ", cards.Select(card => FormatCardValue(card, false)));
    }

    static string FormatCardValue(string card, bool showDealerFaceDown)
    {
        if (showDealerFaceDown && card != "?" && card.Length > 1)
        {
            return "?";
        }

        // Handle face cards (J, Q, K)
        if (card == "J")
        {
            return "J";
        }
        else if (card == "Q")
        {
            return "Q";
        }
        else if (card == "K")
        {
            return "K";
        }
        else if (card == "A")
        {
            return "A"; // Display Ace as "A"
        }
        else
        {
            return card;
        }
    }



    static int GetBet(int availableChips)
    {
        int bet = 0;
        bool validInput = false;

        while (!validInput)
        {
            Console.WriteLine($"You have ${availableChips} in chips. How much would you like to bet?");
            string input = Console.ReadLine()!;

            if (int.TryParse(input, out bet) && bet >= 5 && bet <= availableChips)
            {
                validInput = true;
            }
            else
            {
                Console.WriteLine($"Invalid input. Please enter a whole number between 5 and {availableChips}");
            }
        }

        return bet;
    }


    static void Main(string[] args)
    {
        int chips = 250;
        bool game = true;

        Console.WriteLine("You sit down to play blackjack. The buy-in is $5, and you start with $250 worth of chips");

        while (game)
        {
            Deck deck = new Deck();
            PlayerHand playerHand = new PlayerHand();
            DealerHand dealerHand = new DealerHand();

            bool vInput = false;
            int bet = 0;

            while (!vInput)
            {
                Console.WriteLine($"You have ${chips} in chips. How much would you like to bet?");
                string input = Console.ReadLine()!;

                if (int.TryParse(input, out bet) && bet >= 5 && bet <= chips)
                {
                    vInput = true;
                }
                else
                {
                    Console.WriteLine($"Invalid input. Please enter a whole number between 5 and {chips}");
                }
            }

            bool pTurn = true;
            bool dTurn = true;

            while (pTurn)
            {
                DisplayHands(playerHand, dealerHand, false);
                Console.WriteLine($"Your hand total: {playerHand.CalculateHandValue()}");

                if (playerHand.CalculateHandValue() == 21)
                {
                    Console.WriteLine("BlackJack! You Win!");
                    chips += bet * 3;
                    dTurn = false;
                    break;
                }

                Console.WriteLine("Would you like to hit? (y/n)");
                string choice = Console.ReadLine()!.ToLower();

                if (choice == "y")
                {
                    playerHand.Hit();
                    if (playerHand.CalculateHandValue() > 21)
                    {
                        Console.WriteLine("Bust! You lose!");
                        chips -= bet;
                        dTurn = false;
                        break;
                    }
                }
                else if (choice == "n")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please choose y/n");
                }
            }

            while (dTurn)
            {
                Console.WriteLine("The dealer will now play.");
                // Dealer's turn
                while (dealerHand.CalculateHandValue() < 17)
                {
                    dealerHand.Hit();
                    DisplayHands(playerHand, dealerHand, true);

                    if (dealerHand.IsBust() || dealerHand.CalculateHandValue() >= 17)
                    {
                        break;
                    }
                }

                DisplayHands(playerHand, dealerHand, true);

                if (dealerHand.IsBust() || dealerHand.CalculateHandValue() < playerHand.CalculateHandValue())
                {
                    Console.WriteLine("You Win!");
                    chips += bet * 2;
                    break;
                }
                else if (dealerHand.CalculateHandValue() > playerHand.CalculateHandValue())
                {
                    Console.WriteLine("You lose!");
                    chips -= bet;
                    break;
                }
                else
                {
                    Console.WriteLine("It's a tie!");
                    break;
                }
            }


            Console.WriteLine($"Your chip count: {chips}");
            Console.WriteLine("Play again? (y/n)");
            string playAgain2 = Console.ReadLine()!.ToLower();

            if (playAgain2 == "y")
            {
                game = true;
            }
            else if (playAgain2 == "n")
            {
                Console.WriteLine("Thanks for playing!");
                game = false;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please choose y/n");
            }
        }
    }

}

