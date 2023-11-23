using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat Action/Basic Attack", order = 1)]
public class Attack : CombatAction
{
    public Character target;
    public float deviation = 5;
    public int damage = 2;
    public string hitLog = "The attack hits!";
    public string missLog = "The attack misses!";
    public string blockedLog = "The attack is blocked!";

    public override IEnumerator Execute(Character actor)
    {
        if (actor != CombatManager.Instance.playerChar)
        {
            target = CombatManager.Instance.playerChar;
        }
        //else if (target == null)
        //{
        //    yield return null;
        //}
        //For now I am just assigning the enemy target at random
        else
        {
            int targetIndex = Random.Range(0, CombatManager.Instance.enemiesInBattle.Count - 1);
            target = CombatManager.Instance.enemiesInBattle[targetIndex];
        
        }
        float hit = Random.Range(0f, actor.currentTohit);
        int finalDamage = damage + actor.currentStrength - target.currentDefence;
        if (hit >= deviation && target.blockNext == false && finalDamage > 0)
        {
            CombatManager.Instance.currentCombatLog = hitLog;
            target.currentHealth -= (damage + actor.currentStrength - target.currentDefence);
            target.AnimateNow(animationValue, actor);
            target.AnimateNow("damage", target);
            target.AnimateHealth();
            yield return new WaitForSecondsRealtime(2);
        }
        else if (hit < deviation || target.dodgeLeft && left || target.dodgeRight && !left)
        {
            target.AnimateNow("miss", actor);
            target.AnimateNow("dodge", target);
            CombatManager.Instance.currentCombatLog = missLog;
            yield return new WaitForSecondsRealtime(2);
            
        }
        else
        {
            target.AnimateNow("miss", actor);
            target.AnimateNow("block", target);
            target.blockNext = false;
            CombatManager.Instance.currentCombatLog = blockedLog;
            yield return new WaitForSecondsRealtime(2);
        }
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
