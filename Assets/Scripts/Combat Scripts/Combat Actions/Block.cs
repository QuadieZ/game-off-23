using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat Action/Basic Block", order = 2)]
public class Block : CombatAction
{
    public bool fullBlock = true;
    public bool dodge = false;
    public int addedDefence;

    public override IEnumerator Execute(Character actor)
    {
        if (fullBlock)
        {
            actor.blockNext = true;
            
        }
        else if (dodge)
        {
            if (left)
            {
                actor.dodgeLeft = true;

            }
            else
            {
                actor.dodgeRight = true;
            }
        }
        else
        {
            actor.currentDefence += addedDefence;
        }
        actor.AnimateNow("block", actor);
        CombatManager.Instance.currentCombatLog = actionLog;
        yield return new WaitForSecondsRealtime(2);
        Debug.Log("executed" + actionName);
        finished = true;
        if (actor == CombatManager.Instance.playerChar && speed == ActionSpeed.finisher)
        {
            CombatManager.Instance.playerChar.comboing = false;
        }
        else if (actor == CombatManager.Instance.playerChar && speed == ActionSpeed.starter)
        {
            CombatManager.Instance.playerChar.comboing = true;
        }
        CombatManager.Instance.EndTurn(actor);
        yield return null;
    }
}
