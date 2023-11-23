using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatEvent : UnityEvent<string, int>
{
}

public class CombatManager : MonoBehaviour
{
    //create instance of combatmanager
    private static CombatManager instance;
    public static CombatManager Instance
    {
        get
        {
             return instance;
        }

    }

    public Character playerChar;
    public List<Character> enemiesInBattle = new List<Character>();
    public Encounter currentEncounter;
    public bool playerTurn;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else

        {
            instance = this;

        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (playerChar == null)
        {
            Debug.Log("No Player Character - Combat Broken");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
      
        
    }

    public void CombatBegin()
    {
        foreach (Character chara in currentEncounter.enemiesInEncounter)
        {
            enemiesInBattle.Add(chara);
            chara.AnimateNow("spawn", chara);
        }

    }

    public void StartTurn(Character currentChar)
    {
        if (currentChar = playerChar)
        {
            //make menus interactive for player and wait for input then execute input
        }
        {
            //do the enemies action probably as a coroutine
        }

    }

    public void EndTurn(Character currentChar)
    {
        currentChar.acted = true;

        //check if all enemies have acted
        bool allEnemiesActed = true;
        Character nextChar = null;
        foreach (Character chara in enemiesInBattle)
        {
            if (chara.acted == false)
            {
                allEnemiesActed = false;
                nextChar = chara;
                break;
            }

        }
        if (allEnemiesActed)
        {
            EndRound();
        }
        else
        {
            StartTurn(nextChar);
        }

    }

    public void EndRound()
    {
        //check if all enemies are dead
        if (enemiesInBattle.Count == 0)
        {
            EndBattle();
        }
        else
        {
            foreach(Character chara in enemiesInBattle)
            {
                chara.acted = false;
            }
            playerChar.acted = false;
            playerTurn = true;
            StartTurn(playerChar);
        }

    }

    public void EndBattle()
    {
        //switch camera back to overworld camera
        //
        //end the combat


    }
}
