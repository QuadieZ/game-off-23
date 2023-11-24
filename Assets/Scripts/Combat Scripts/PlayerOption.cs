using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerOption : Button
{
    public CombatAction option;
    public TMP_Text mytext;

    public void PlaceOption(CombatAction newOption)
    {
        RemoveOption();
        option = newOption;
        mytext = GetComponentInChildren<TMP_Text>();
        mytext.text = option.actionName;
        //we check whether the player is in a combo and if the current action is allowed
        switch ((CombatManager.Instance.playerChar.comboing, option.speed))
        {
            case (true, CombatAction.ActionSpeed.starter):
                SwitchOff(true);
                break;
            case (false, CombatAction.ActionSpeed.starter):
                SwitchOff(false);
                onClick.AddListener(delegate { OptionSelected(); });
                break;
            case (true, CombatAction.ActionSpeed.combo):
                SwitchOff(false);
                onClick.AddListener(delegate { OptionSelected(); });
                break;
            case (false, CombatAction.ActionSpeed.combo):
                SwitchOff(true);
                break;
            case (true, CombatAction.ActionSpeed.finisher):
                SwitchOff(false);
                onClick.AddListener(delegate { OptionSelected(); });
                break;
            case (false, CombatAction.ActionSpeed.finisher):
                SwitchOff(true);
                break;
            case (true, CombatAction.ActionSpeed.standalone):
                SwitchOff(true);
                break;
            case (false, CombatAction.ActionSpeed.standalone):
                SwitchOff(false);
                onClick.AddListener(delegate { OptionSelected(); });
                break;
            case (true, CombatAction.ActionSpeed.flexible):
                SwitchOff(false);
                onClick.AddListener(delegate { OptionSelected(); });
                break;
            case (false, CombatAction.ActionSpeed.flexible):
                SwitchOff(false);
                onClick.AddListener(delegate { OptionSelected(); });
                break;
        }


    }

    public void RemoveOption()
    {
        onClick.RemoveAllListeners();
        mytext = null;

    }

    public void SwitchOff(bool off)
    {
        if (off)
        {
            interactable = false;
            mytext.color = Color.grey;
        }
        else
        {
            interactable = true;
            mytext.color = Color.white;
        }
    }

    public void OptionSelected()
    {
        Debug.Log("Selected" + option.actionName);
        CombatManager.Instance.playerChar.GetAction(option);
    }


}
