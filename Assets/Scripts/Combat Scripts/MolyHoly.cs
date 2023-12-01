using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolyHoly : Character
{
    bool windup = false;
    int spat = 0;
    public override void GetAction(CombatAction action, bool acting)
    {
        previousAction = currentAction;
        int nextup = 0;
        if (windup && acting)
        {
            
            nextup = Random.Range(0, 2);
            windup = false;
        }
        else
        {
            nextup = Random.Range(2, myActions.Count - spat);
        }
        if(nextup == 2)
        {
            
            windup = true;
            spat = 0;
        }
        else if (nextup == 3)
        {
            spat += 1;
        }
        nextAction = myActions[nextup];
        currentAction = action;
        if (acting)
        {
            StartCoroutine(Act(currentAction));
        }
       
    }

    public override IEnumerator Act(CombatAction action)
    {
        action.finished = false;
        CombatManager.Instance.currentCombatLog = action.prepLog;
        yield return new WaitForSecondsRealtime(2f);
        StartCoroutine(action.Execute(this));
        //Debug.Log(name + "finished Acting" + action.actionName);
        yield return null;


    }
}
