using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public bool comboing;

    public override void GetAction(CombatAction action, bool acting)
    {
        previousAction = currentAction;
        currentAction = action;
        StartCoroutine(Act(currentAction));
    }

    public override IEnumerator Act(CombatAction action)
    {
        CombatManager.Instance.combatMenu.SetActive(false);
        action.finished = false;
        CombatManager.Instance.currentCombatLog = action.prepLog;
        yield return new WaitForSecondsRealtime(2f);
        StartCoroutine(action.Execute(this));
        Debug.Log("player started" + action.actionName);
        yield return null;


    }
}
