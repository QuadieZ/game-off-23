using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatAction : ScriptableObject
{

    public string prepLog;
    public string actionLog;
    public string actionName;
    public Character actor;
    public ActionSpeed actionSpeed;
    public string animationValue;

    public enum ActionSpeed
    {
        starter,
        combo,
        finisher,
        standalone,
    }

    public virtual IEnumerator Execute()
    {
        return null;

    }
}
