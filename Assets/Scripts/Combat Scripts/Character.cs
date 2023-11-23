using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Character : MonoBehaviour
{

    public string characterName;
    [Header("Health")]
    public int defaultHealth;
    public int currentHealth;
    [Header("Defence")]
    public int defaultDefence;
    public int currentDefence;
    public bool blockNext;
    [Header("Strength")]
    public int defaultStrength;
    public int currentStrength;
    public bool doubleNext;
    [Header("Accuracy")]
    public int defaultTohit;
    public int currentTohit;
    public bool missNext;
    [Header("Actions")]
    public CombatAction nextAction;
    public CombatAction currentAction;
    public CombatAction previousAction;
    //check if the character has acted this round
    public bool acted;
    [Header("UI")]
    public Transform healthBar;


    public Animator myAnim;


    private void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    public virtual void GetAction(CombatAction action)
    {
        previousAction = currentAction;
        currentAction = action;
    }

    public void Refresh(bool healthInc)
    {
        currentDefence = defaultDefence;
        currentStrength = defaultStrength;
        currentTohit = defaultTohit;

        if (healthInc)
        {
            currentHealth = defaultHealth;
        }

    }

    public void AnimateNow(string animTrigger, Character chara)
    {
        if (chara.myAnim != null)
        {
            chara.myAnim.SetTrigger(animTrigger);
        }
    }

    public void AnimateHealth()
    {
        var scale = healthBar.localScale;
        scale.y = currentHealth/defaultHealth;
        healthBar.localScale = scale;
    }
}
