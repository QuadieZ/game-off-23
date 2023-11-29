using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat Action/Buff", order = 4)]
public class Buff : CombatAction
{
    public BuffType type;
    public int buffStrength = 3;
    public int maxBuff = 3;
    public enum BuffType
    {
        Defence,
        Attack,
        Health,
        MaxHealth,
        Accuracy,

    }
    public override IEnumerator Execute(Character actor)
    {
        int variance = Random.Range(0, maxBuff);
        switch(type){
            case BuffType.Defence:
                actor.currentDefence += buffStrength + variance;
                break;
            case BuffType.Attack:
                actor.currentStrength += buffStrength + variance;
                break;
            case BuffType.Health:
                actor.currentHealth += buffStrength + variance;
                break;
            case BuffType.MaxHealth:
                actor.defaultHealth += buffStrength + variance;
                break;
            case BuffType.Accuracy:
                actor.currentTohit += buffStrength + variance;
                break;

        }
        CombatManager.Instance.currentCombatLog = prepLog;
        yield return new WaitForSecondsRealtime(2);
        actor.AnimateNow(animationValue, actor);
        CombatManager.Instance.currentCombatLog = actionLog;
        yield return new WaitForSecondsRealtime(2);
        Debug.Log("executed" + actionName);
        CombatManager.Instance.currentCombatLog = actionLog;
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
