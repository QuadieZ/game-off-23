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
    public bool dodgeLeft;
    public bool dodgeRight;
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
    public List<CombatAction> myActions = new List<CombatAction>();
    //check if the character has acted this round
    public bool acted;
    [Header("UI")]
    public Transform healthBar;


    public Animator myAnim;


    private void Start()
    {
        myAnim = GetComponentInChildren<Animator>();
        Refresh(true);

    }

    public virtual void GetAction(CombatAction action)
    {
        previousAction = currentAction;
        //default behavior is to randomly select from the list of possible actions
        int nextup = Random.Range(0, (myActions.Count)-1);
        nextAction = myActions[nextup];
        currentAction = action;
        StartCoroutine(Act(currentAction));
    }

    public virtual IEnumerator Act(CombatAction action)
    {
        action.finished = false;
        CombatManager.Instance.currentCombatLog = action.prepLog;
        yield return new WaitForSecondsRealtime(2f);
        StartCoroutine(action.Execute(this));
        Debug.Log(name + "finished Acting" + action.actionName);
        yield return null;


    }


    public void Refresh(bool fullRefresh)
    {

        currentTohit = defaultTohit;
        dodgeLeft = false;
        dodgeRight = false;
        blockNext = false;

        if (fullRefresh)
        {
            currentHealth = defaultHealth;
            currentDefence = defaultDefence;
            currentStrength = defaultStrength;
        }

    }

    public void AnimateNow(string animTrigger, Character chara)
    {
        if (chara.myAnim != null)
        {
            chara.myAnim.SetTrigger(animTrigger);
            Debug.Log(animTrigger + "meow meow");
        }
    }

    public void AnimateHealth()
    {
        if (currentHealth <= 0)
        {
            AnimateNow("death", this);
        }
        else
        {
            float _c = currentHealth;
            float _d = defaultHealth;
            var scale = healthBar.localScale;
            scale.x = _c / _d;
            healthBar.localScale = scale;

        }

    }
}
