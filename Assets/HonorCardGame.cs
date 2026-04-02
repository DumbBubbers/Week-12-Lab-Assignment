using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HonorCardGame : MonoBehaviour
{
    List<string> deck;
    List<string> hand;

    string[] ranks = { "K", "Q", "J", "A" };
    string[] suits = { "♠", "♣", "♥", "♦" };

    bool gameWon;
    bool gameLost;

    void Start()
    {
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        yield return new WaitForSeconds(10f); // allow Login Server Queue script to run first

        while (true)
        {
            yield return StartCoroutine(RunGame());

            Debug.Log("----- Starting a new Honor Card Game -----");

            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator RunGame()
    {
        deck = new List<string>();
        hand = new List<string>();

        gameWon = false;
        gameLost = false;

        // Create deck
        foreach (string suit in suits)
        {
            foreach (string rank in ranks)
            {
                deck.Add(rank + suit);
            }
        }

        // Shuffle deck
        deck = deck.OrderBy(card => Random.value).ToList();

        // Deal opening hand
        for (int i = 0; i < 4; i++)
        {
            hand.Add(DrawCard());
        }

        Debug.Log("I made the initial deck and draw. My hand is: " +
                  string.Join(", ", hand) + ".");

        while (!gameWon && !gameLost)
        {
            if (IsWinningHand())
            {
                Debug.Log("My hand is: " + string.Join(", ", hand) +
                          ". The game is WON.");
                gameWon = true;
                break;
            }

            if (deck.Count == 0)
            {
                Debug.Log("The deck is empty. The game is LOST.");
                gameLost = true;
                break;
            }

            int discardIndex = Random.Range(0, hand.Count);
            string discarded = hand[discardIndex];
            hand.RemoveAt(discardIndex);

            string drawn = DrawCard();
            hand.Add(drawn);

            if (IsWinningHand())
            {
                Debug.Log("I discarded " + discarded + " and drew " + drawn +
                          ". My hand is: " + string.Join(", ", hand) +
                          ". The game is WON.");
                gameWon = true;
            }
            else
            {
                Debug.Log("I discarded " + discarded + " and drew " + drawn +
                          ". My hand is: " + string.Join(", ", hand) +
                          ". This is not a winning hand. I will attempt to play another round.");
            }

            yield return new WaitForSeconds(2f); // delay between turns
        }
    }

    string DrawCard()
    {
        string card = deck[0];
        deck.RemoveAt(0);
        return card;
    }

    bool IsWinningHand()
    {
        var suitGroups = hand
            .Select(card => card.Substring(card.Length - 1))
            .GroupBy(suit => suit)
            .Select(group => group.Count());

        return suitGroups.Any(count => count >= 3);
    }
}
