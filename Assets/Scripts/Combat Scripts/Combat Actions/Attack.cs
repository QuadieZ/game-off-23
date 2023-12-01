using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat Action/Basic Attack", order = 1)]
public class Attack : CombatAction
{
    public Character target;
    public float deviation = 2;
    public int damage = 2;
    public string hitLog = "The attack hits!";
    public string missLog = "The attack misses!";
    public string blockedLog = "The attack is blocked!";
    public bool directionRandom;
    public bool multiAttack;
    public bool sameTarget;
    public bool hitEveryEnemyOnce;
    public bool parry;
    public int hitTotal;
    public int hitCount;

    public override IEnumerator Execute(Character actor)
    {
        if (actor != CombatManager.Instance.playerChar)
        {
            target = CombatManager.Instance.playerChar;
            CombatManager.Instance.playerChar.attacked = true;
        }
        //For now I am just assigning the enemy target at random
        else if (hitEveryEnemyOnce == true)
        {
            target = CombatManager.Instance.enemiesInBattle[0];
        }
        else
        {

            target = newRandomTarget();
        }

        Debug.Log("my target is " + target.name);

        if (multiAttack)
        {
            hitCount = hitTotal;
        }
        else
        {
            hitCount = 1;
        }

        while (hitCount > 0)
        {
            //pick a new target if need be
            if (hitCount < hitTotal && hitEveryEnemyOnce == true && actor == CombatManager.Instance.playerChar)
            {
                target = CombatManager.Instance.enemiesInBattle[CombatManager.Instance.enemiesInBattle.IndexOf(target) + 1];
            }
            else if (hitCount < hitTotal && sameTarget == false && actor == CombatManager.Instance.playerChar)
            {
                target = newRandomTarget();
                
            }
            else if (hitCount < hitTotal && (sameTarget == true || CombatManager.Instance.playerChar != actor) && target.currentHealth == 0)
            {
                break;
            }
            if (hitCount < hitTotal)
            {
                CombatManager.Instance.currentCombatLog = actionLog;
            }


            int hit = Random.Range(0, actor.currentTohit + 1);
            int finalDamage = damage + actor.currentStrength - target.currentDefence;
            if (parry && CombatManager.Instance.playerChar.attacked)
            {
                finalDamage += actor.currentDefence;
            }
            Debug.Log("aims for " + finalDamage + " on " + target + " accuracy is " + hit + " and deviation is " + deviation);
            if (directionRandom)
            {
                left = System.Convert.ToBoolean(Random.Range(0, 2));
                
            }

            if (hit < deviation || target.dodgeLeft && left || target.dodgeRight && !left)
            {
                target.AnimateNow("miss", actor);
                target.AnimateNow("dodge", target);
                //audio clip one is the attack sound
                actor.audioSource.pitch = 1.2f;
                actor.PlayAudio(0);
                actor.audioSource.pitch = 1f;
                Debug.Log(actor.name + " missed and left was " + left);
                CombatManager.Instance.currentCombatLog = missLog;
                yield return new WaitForSecondsRealtime(2);
                

            }
            else if (target.blockNext == false && finalDamage > 0)
            {

                CombatManager.Instance.currentCombatLog = hitLog;
                target.beforeHealth = target.currentHealth;
                target.currentHealth -= (damage + actor.currentStrength - target.currentDefence);
                target.AnimateNow(animationValue, actor);
                target.AnimateNow("damage", target);
                //audio clip one is the attack sound
                actor.PlayAudio(0);
                target.AnimateHealth();
                Debug.Log(actor.name + " hit and left was " + left);
                yield return new WaitForSecondsRealtime(0.5f);
                if (CombatManager.Instance.enemiesInBattle.Count == 0)
                {
                    CombatManager.Instance.currentCombatLog = "You are alone once more";
                }
                yield return new WaitForSecondsRealtime(1.5f);


            }
            else
            {

                target.AnimateNow("miss", actor);
                target.AnimateNow("block", target);
                actor.audioSource.pitch = 0.9f;
                actor.PlayAudio(0);
                actor.audioSource.pitch = 1f;
                target.blockNext = false;
                Debug.Log(actor.name + " was blocked ");
                CombatManager.Instance.currentCombatLog = blockedLog;
                yield return new WaitForSecondsRealtime(2);
                
            }
            Debug.Log("executed" + actionName);
            if (multiAttack == true && hitEveryEnemyOnce == false)
            {
                hitCount -= 1;
               
            }
            else if(hitEveryEnemyOnce == true && CombatManager.Instance.enemiesInBattle.IndexOf(target) < CombatManager.Instance.enemiesInBattle.Count - 1)
            {
                hitCount = 1;
            }
            else
            {
                hitCount = 0;
            }
        }
        hitCount = hitTotal;
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

    public Character newRandomTarget()
    {
        Character newTarget;
        int targetIndex = Random.Range(0, CombatManager.Instance.enemiesInBattle.Count);
        newTarget = CombatManager.Instance.enemiesInBattle[targetIndex];
        return newTarget;
    }


}
