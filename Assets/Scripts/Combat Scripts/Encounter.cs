using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    public List<Character> enemiesInEncounter = new List<Character>();

    private void Start()
    {
        foreach (Character chara in GetComponentsInChildren<Character>())
        {
            if (enemiesInEncounter.Contains(chara) == false)
            {
                enemiesInEncounter.Add(chara);
                Debug.Log("whoops you forgot to add " + chara.characterName + "to this encounter");
            }
        }
    }

}
