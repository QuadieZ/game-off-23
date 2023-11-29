using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat Action/Memory", order = 5)]
public class Reminisce : CombatAction
{
    public string fullText;
    public string secretWeakness;
    public int memStrength;
    public override IEnumerator Execute(Character actor)
    {
        actor.AnimateNow(animationValue, actor);
        CombatManager.Instance.currentCombatLog = actionLog;
        foreach (Character chara in CombatManager.Instance.enemiesInBattle)
        {
            if (chara.secretWeakness == secretWeakness)
            {
                chara.currentHealth -= memStrength;
            }
        }
        yield return new WaitForSecondsRealtime(2);
        Debug.Log("executed" + actionName);
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
