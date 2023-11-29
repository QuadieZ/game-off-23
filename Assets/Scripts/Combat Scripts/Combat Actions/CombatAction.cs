using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatAction : ScriptableObject
{

    public string prepLog;
    public string actionLog;
    public string actionName;
    public string animationValue;
    public string animationPrepValue;
    public bool finished;
    public bool left;
    public bool defensive;

    public float sizeCost;

    public enum ActionSpeed
    {
        starter,
        combo,
        finisher,
        standalone,
        flexible,
    }
    public ActionSpeed speed;

    public virtual IEnumerator Execute(Character actor)
    {
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
