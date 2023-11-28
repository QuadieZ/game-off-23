using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    [Header("UI")]
    public string currentCombatLog;
    public TMP_Text combatLog;
    public GameObject combatMenu;
    public List<PlayerOption> menuButtons = new List<PlayerOption>();

    [Header("References")]
    public PlayerCharacter playerChar;
    public List<CombatAction> playerActions = new List<CombatAction>();
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

        //Test Combat - Combat should usually begin because the player triggered it somehow in the overworld
        CombatBegin(currentEncounter);
        
    }

    // Update is called once per frame
    void Update()
    {
        combatLog.text = currentCombatLog;

        if (combatMenu.activeSelf == true && Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            ScrollOptions(true);

        }
        else if (combatMenu.activeSelf == true && Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            ScrollOptions(false);
        }


    }


    public void CombatBegin(Encounter encounter)
    {
        playerChar.Refresh(true);
        currentEncounter = encounter;
        currentCombatLog = "A Battle has Begun";
        foreach (Character chara in currentEncounter.enemiesInEncounter)
        {
            enemiesInBattle.Add(chara);
            chara.Refresh(true);
            chara.AnimateNow("spawn", chara);
        }
        foreach (PlayerOption button in menuButtons)
        {
            if (playerActions.Count - 1 >= menuButtons.IndexOf(button))
            {
                button.PlaceOption(playerActions[menuButtons.IndexOf(button)]);
            }
            else
            {
                break;
            }
            
        }
        StartTurn(playerChar);

    }

    public void StartTurn(Character currentChar)
    {
        if (currentChar == playerChar)
        {
            foreach (PlayerOption button in menuButtons)
            {
                button.PlaceOption(button.option);
            }
            combatMenu.SetActive(true);
            Debug.Log("Starting Player Turn");
            //make menus interactive for player and wait for input then execute input
        }
        else
        {
            Debug.Log("Starting " + currentChar.name + " Turn");
            //Make the enemy start their next queued action
            if (currentChar.nextAction == null)
            {
                currentChar.nextAction = currentChar.myActions[0];
            }
            currentChar.GetAction(currentChar.nextAction);
            
        }

    }

    //Moving the Player Options
    public void ScrollOptions(bool up)
    {
        if (playerActions.Count > 4)
        {
            int bottomIndex = playerActions.IndexOf(menuButtons[0].option);
            int topIndex = playerActions.IndexOf(menuButtons[3].option);
            //Debug.Log(bottomIndex);
            if (bottomIndex <= 0 && up == false || topIndex >= playerActions.Count -1 && up == true)
            {
                //Debug.Log("Max Scroll Reached");
                return;
            }
            foreach (PlayerOption button in menuButtons)
            {
                int currentIndex = playerActions.IndexOf(button.option);
                if (up)
                {
                    //Debug.Log("Scroll Up");
                    button.PlaceOption(playerActions[currentIndex + 1]);
                }
                else
                {
                    //Debug.Log("Scroll Down");
                    button.PlaceOption(playerActions[currentIndex - 1]);
                }

            }
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
                //Debug.Log(nextChar);
                break;
            }

        }
        if (allEnemiesActed)
        {
            foreach(Character chara in enemiesInBattle)
            {
                chara.Refresh(false);
            }
            playerChar.Refresh(false);
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
        //refresh stats for player and for the encounter so it can be reused
        playerChar.Refresh(true);
        foreach (Character chara in enemiesInBattle)
        {
            chara.Refresh(true);
        }
        foreach (PlayerOption button in menuButtons)
        {
            button.option = null;
            button.mytext.text = " ";
            button.interactable = false;

        }

        //end the combat


    }
}
