using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DuplicateNameDetector : MonoBehaviour
{
    // Reuse first-name list from Task 1
    static string[] firstNames =
    {
        "Carol","Adam","Maria","John","Leila","Chris","Taylor","Jordan","Alex",
        "Sam","Jamie","Casey","Morgan","Riley","Avery","Quinn","Drew","Parker",
        "Skyler","Rowan","Blake","Elliot","Reese","Cameron","Dakota","Logan"
    };

    void Start()
    {
        // 1. Build the Name Array
        List<string> nameArray = new List<string>();

        for (int i = 0; i < 15; i++)
        {
            string randomName = firstNames[Random.Range(0, firstNames.Length)];
            nameArray.Add(randomName);
        }

        Debug.Log("Created the name array: " + string.Join(", ", nameArray));

        // 2. Detect Duplicates with HashSet
        HashSet<string> seen = new HashSet<string>();
        HashSet<string> duplicates = new HashSet<string>();

        foreach (string name in nameArray)
        {
            if (!seen.Add(name))
            {
                duplicates.Add(name);
            }
        }

        // 3. Report Results
        if (duplicates.Count > 0)
        {
            Debug.Log("The array has duplicate names: " +
                      string.Join(", ", duplicates));
        }
        else
        {
            Debug.Log("The array has no duplicate names.");
        }
    }
}
