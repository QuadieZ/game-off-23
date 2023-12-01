using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Character : MonoBehaviour
{

    public string characterName;
    [Header("Health")]
    public int defaultMaxHealth;
    public int defaultHealth;
    public int currentHealth;
    public int beforeHealth;
    public string secretWeakness;
    [Header("Defence")]
    public int defaultDefence;
    public int currentDefence;
    public bool blockNext;
    public bool dodgeLeft;
    public bool dodgeRight;
    [Header("Strength")]
    public int defaultStrength;
    public int currentStrength;
    public bool doubleNext;
    [Header("Accuracy")]
    public int defaultTohit;
    public int currentTohit;
    public bool missNext;
    [Header("Actions")]
    public CombatAction nextAction;
    public CombatAction currentAction;
    public CombatAction previousAction;
    public List<CombatAction> myActions = new List<CombatAction>();
    //check if the character has acted this round
    public bool acted;
    int lastBuff = 0;
    int lastAttack = 0;
    [Header("UI")]
    public Transform healthBar;

    [Header("Sound")]
    public AudioSource audioSource;
    public List<AudioClip> audioClips = new List<AudioClip>();


    public Animator myAnim;


    private void Start()
    {
        myAnim = GetComponentInChildren<Animator>();
        audioSource = GetComponentInChildren<AudioSource>();
        Refresh(true);

    }

    public virtual void GetAction(CombatAction action)
    {
        previousAction = currentAction;
        //default behavior is to randomly select from the list of possible actions
        //make sure their buff action are closer to index 0 and this code will make sure they don't do the same buff twice in a row
        if (previousAction != null && previousAction.GetType().ToString() == "Buff")
        {
            Debug.Log("removing buff chance");
            lastBuff += 1;
            lastAttack = 0;
        }
        else if(previousAction != null && previousAction.GetType().ToString() == "Attack")
        {
            Debug.Log("lowering attack chance");
            lastAttack += 1;
            lastBuff = 0;
        }
        //Debug.Log(previousAction.GetType().ToString() + lastBuff);
        int nextup = Random.Range(0 + lastBuff, (myActions.Count) - lastAttack);
        nextAction = myActions[nextup];
        currentAction = action;
        StartCoroutine(Act(currentAction));
    }

    public virtual IEnumerator Act(CombatAction action)
    {
        action.finished = false;
        CombatManager.Instance.currentCombatLog = action.prepLog;
        yield return new WaitForSecondsRealtime(2f);
        StartCoroutine(action.Execute(this));
        //Debug.Log(name + "finished Acting" + action.actionName);
        yield return null;


    }


    public void Refresh(bool fullRefresh)
    {

        currentTohit = defaultTohit;
        dodgeLeft = false;
        dodgeRight = false;
        blockNext = false;

        if (fullRefresh)
        {
            defaultHealth = defaultMaxHealth;
            currentHealth = defaultHealth;
            currentDefence = defaultDefence;
            currentStrength = defaultStrength;
            healthBar.gameObject.SetActive(true);
        }

    }

    public void AnimateNow(string animTrigger, Character chara)
    {
        if (chara.myAnim != null)
        {
            chara.myAnim.SetTrigger(animTrigger);
            //Debug.Log(animTrigger + "meow meow");
        }
    }

    public void PlayAudio(int indexOfClip)
    {
        audioSource.Stop();
        audioSource.clip = audioClips[indexOfClip];
        audioSource.Play();
        Debug.Log( audioSource.clip.name +" played");
    }

    public void AnimateHealth()
    {
        if (currentHealth <= 0)
        {
            Debug.Log(characterName + " died");
            //the death sound should always be last in the index
            PlayAudio(audioClips.Count - 1);
            myAnim.SetBool("death", true);
            healthBar.gameObject.SetActive(false);
            Death();
        }
        else
        {
            float _c = currentHealth;
            float _d = defaultHealth;
            if(beforeHealth > currentHealth)
            {
                //damage sound effect is 2
                PlayAudio(2);
            }
            else if (beforeHealth < currentHealth)
            {
                //heal sound effect is 3
                PlayAudio(3);
            }
            var scale = healthBar.localScale;
            scale.x = _c / _d;
            healthBar.localScale = scale;

        }

    }

    public void Death()
    {
        if (CombatManager.Instance.playerChar == this)
        {
            //figure this out
        }
        else
        {
            CombatManager.Instance.enemiesInBattle.Remove(this);
        }
    }
}
