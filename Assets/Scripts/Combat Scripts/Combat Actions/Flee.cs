using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat Action/Flee", order = 3)]
public class Flee : CombatAction
{
    public string failmessage;

    public override IEnumerator Execute(Character actor)
    {
        
        int chance = Random.Range(0, 4);
        if (actor.previousAction.defensive)
        {
            chance += actor.currentDefence;
        }
        if (chance > CombatManager.Instance.enemiesInBattle.Count)
        {
            CombatManager.Instance.currentCombatLog = actionLog;
            yield return new WaitForSecondsRealtime(1);
            CombatManager.Instance.EndBattle();
        }
        else
        {
            CombatManager.Instance.currentCombatLog = failmessage;
            yield return new WaitForSecondsRealtime(1);
        }
        if (actor == CombatManager.Instance.playerChar && speed == ActionSpeed.finisher)
        {
            CombatManager.Instance.playerChar.comboing = false;
        }
        else if (actor == CombatManager.Instance.playerChar && speed == ActionSpeed.starter)
        {
            CombatManager.Instance.playerChar.comboing = true;
        }
        yield return null;
    }
}
