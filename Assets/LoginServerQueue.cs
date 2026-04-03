using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoginServerQueue : MonoBehaviour
{
    // Name Library
    static string[] firstNames =
    {
        "Carol","Adam","Maria","John","Leila","Chris","Taylor","Jordan","Alex",
        "Sam","Jamie","Casey","Morgan","Riley","Avery","Quinn","Drew","Parker",
        "Skyler","Rowan","Blake","Elliot","Reese","Cameron","Dakota","Logan"
    };

    static string[] lastInitials =
    {
        "A","B","C","D","E","F","G","H","I","J","K","L","M",
        "N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
    };

    Queue<string> loginQueue = new Queue<string>();

    void Start()
    {
        // Create Initial Login Queue
        int initialPlayers = Random.Range(4, 7);

        for (int i = 0; i < initialPlayers; i++)
        {
            loginQueue.Enqueue(GetRandomPlayerName());
        }

        List<string> readableList = loginQueue.ToList();
        Debug.Log("Initial login queue created. There are " +
                  readableList.Count + " players in the queue: " +
                  string.Join(", ", readableList));

        // Start automated routines
        StartCoroutine(AddPlayerRoutine());
        StartCoroutine(LoginPlayerRoutine());
    }

    // Helper Method
    string GetRandomPlayerName()
    {
        string first = firstNames[Random.Range(0, firstNames.Length)];
        string last = lastInitials[Random.Range(0, lastInitials.Length)];

        return first + " " + last + ".";
    }

    // Add Player Routine
    IEnumerator AddPlayerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f));

            string newPlayer = GetRandomPlayerName();
            loginQueue.Enqueue(newPlayer);

            Debug.Log(newPlayer + " is trying to login and added to the login queue.");
        }
    }

    // Login Player Routine
    IEnumerator LoginPlayerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);

            if (loginQueue.Count > 0)
            {
                string player = loginQueue.Dequeue();
                Debug.Log(player + " is now inside the game.");
            }
            else
            {
                Debug.Log("Login server is idle. No players are waiting.");
            }
        }
    }
}
